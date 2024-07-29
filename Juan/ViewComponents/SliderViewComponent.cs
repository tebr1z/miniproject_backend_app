using Juan.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly JuanDbContext _context;

        public SliderViewComponent(JuanDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(await Task.FromResult(sliders));
        }
    }
}
