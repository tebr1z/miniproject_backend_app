using Juan.Models;
using Juan.ViewModels;

namespace Juan.Interfaces
{
    public interface ILayoutService
    {
        IDictionary<string, string> GetSettings();
        Task<IEnumerable<Category>> GetCategoriesAsync();
        IEnumerable<BasketVM> GetBasket();

    }
}