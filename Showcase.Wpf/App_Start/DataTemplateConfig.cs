using Showcase.Wpf.Base;
using Showcase.Wpf.Base.Presentation;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Showcase.Wpf
{
    class DataTemplateConfig
    {
        public static async Task RegisterAllDataTemplatesAsync(IDependencyResolver resolver, ResourceDictionary resources)
        {
            var factory = resolver.GetInstance<IDataTemplateStoreFactory>();
            var dataTemplateStore = await factory.CreateDataTemplateStoreAsync(resolver, resources);

            await dataTemplateStore.AddDataTemplateForAllViewsAsync(Assembly.GetExecutingAssembly());
        }
    }
}