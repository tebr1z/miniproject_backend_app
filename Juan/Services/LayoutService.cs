using Juan.Data;
using Juan.Interfaces;

using Juan.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Juan.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly JuanDbContext _context;


        public LayoutService (JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories =await _context.Categories
                .AsNoTracking()
                .Where(c=>!c.IsDelete)
                .ToListAsync();
            return categories;
        }

        public IDictionary<string, string> GetSettings() => _context.Settings
            .Where(s => !s.IsDelete)
            .ToDictionary(s => s.Key, s =>s.Value);
            
    }
}
