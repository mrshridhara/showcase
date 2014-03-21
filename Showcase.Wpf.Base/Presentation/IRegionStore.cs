using System.Threading.Tasks;

namespace Showcase.Wpf.Base.Presentation
{
    public interface IRegionStore
    {
        Task ClearRegionAsnc(string regionName);

        Task ShowInRegionAsync<TDataContext>(string regionName, TDataContext dataContext);
    }
}