using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Showcase.Wpf.Base.Presentation
{
    public interface IDataTemplateStore
    {
        Task AddDataTemplateAsyncFor<TView>()
            where TView : IView;

        Task AddDataTemplateAsyncFor(Type viewType);

        Task AddDataTemplateForAllViewsAsync(Assembly assembly);

        Task<DataTemplate> GetDataTemplateAsyncFor<TDataContext>();

        Task<DataTemplate> GetDataTemplateAsyncFor(Type dataContextType);
    }
}
