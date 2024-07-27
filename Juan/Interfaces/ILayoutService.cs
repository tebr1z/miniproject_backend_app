using Juan.Models;

namespace Juan.Interfaces
{
    public interface ILayoutService
    {
        IDictionary<string, string> GetSettings();
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}