using System.Threading.Tasks;
using System.Windows;

namespace Showcase.Wpf.Base.Presentation.Defaults
{
    public class DataTemplateStoreFactory : IDataTemplateStoreFactory
    {
        public async Task<IDataTemplateStore> CreateDataTemplateStoreAsync(IDependencyResolver resolver, ResourceDictionary resources)
        {
            return await Task.Run(() => new DataTemplateStore(resolver, resources));
        }
    }
}