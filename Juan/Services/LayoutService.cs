using Juan.Data;
using Juan.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Juan.Services
{
    public class LayoutService
    {
        private readonly JuanDbContext _context;


        public LayoutService (JuanDbContext context)
        {
            _context = context;
        }

        public IDictionary<string, string> GetSettings() => _context.Settings
            .Where(s => !s.IsDelete)
            .ToDictionary(s => s.Key, s =>s.Value);
            
    }
}
