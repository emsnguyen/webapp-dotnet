// #define  SortFilterPage // SortFilter //SortOnly // first 

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly BugTrackingContext _context;
        public List<Project>? Projects { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public IndexModel(BugTrackingContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects.ToListAsync();
        }
    }
}