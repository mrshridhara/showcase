using System.Threading.Tasks;
using System.Windows;

namespace Showcase.Wpf.Base.Presentation
{
    public interface IDataTemplateStoreFactory
    {
        Task<IDataTemplateStore> CreateDataTemplateStoreAsync(IDependencyResolver resolver, ResourceDictionary resources);
    }
}