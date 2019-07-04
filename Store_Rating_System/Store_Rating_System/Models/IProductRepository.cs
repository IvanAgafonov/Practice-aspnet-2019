using System.Linq;

namespace Store_Rating_System.Models
{

    public interface IProductRepository
    {

        IQueryable<Store> Stores { get; }
    }
}
