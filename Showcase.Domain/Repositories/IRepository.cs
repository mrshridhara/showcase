using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.Domain.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Entities { get; set; }

        Task SaveAsync();

        Task LoadAsync();
    }
}