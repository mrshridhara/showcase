using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Showcase.Wpf.Base.Presentation.Defaults
{
    public sealed class DataTemplateStore : IDataTemplateStore
    {
        private IDependencyResolver _resolver;
        private ResourceDictionary _resources;

        public DataTemplateStore(IDependencyResolver resolver, ResourceDictionary resources)
        {
            _resolver = resolver;
            _resources = resources;
        }

        public async Task AddDataTemplateAsyncFor<TView>()
            where TView : IView
        {
            await AddDataTemplateAsyncFor(typeof(TView));
        }

        public async Task AddDataTemplateAsyncFor(Type viewType)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var typePair = (from eachInterfaceType in viewType.GetInterfaces()
                                where eachInterfaceType == typeof(IView) && eachInterfaceType.IsGenericType
                                let dataContextInterfaceType = eachInterfaceType.GetGenericArguments()[0]
                                from registeredTypePair in _resolver.RegisteredTypes
                                where registeredTypePair.Key == dataContextInterfaceType
                                select new { ViewType = viewType, DataContextType = registeredTypePair.Value }).FirstOrDefault();

                if (typePair != null)
                {
                    var dataTemplate = new DataTemplate
                    {
                        DataType = typePair.DataContextType,
                        VisualTree = new FrameworkElementFactory(typePair.ViewType)
                    };

                    _resources.Add(dataTemplate.DataTemplateKey, dataTemplate);
                }
            }, DispatcherPriority.DataBind);
        }

        public async Task AddDataTemplateForAllViewsAsync(Assembly assembly)
        {
            foreach (var viewType in assembly.GetTypes().AsParallel())
            {
                await AddDataTemplateAsyncFor(viewType);
            }
        }

        public async Task<DataTemplate> GetDataTemplateAsyncFor<TDataContext>()
        {
            return await GetDataTemplateAsyncFor(typeof(TDataContext));
        }

        public async Task<DataTemplate> GetDataTemplateAsyncFor(Type dataContextType)
        {
            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return (from entry in _resources.Cast<DictionaryEntry>()
                        where entry.Key is DataTemplateKey
                        let dataTemplateKey = (DataTemplateKey)entry.Key
                        where ((Type)dataTemplateKey.DataType) == dataContextType
                        select entry.Value as DataTemplate).FirstOrDefault();
            }, DispatcherPriority.DataBind);
        }
    }
}