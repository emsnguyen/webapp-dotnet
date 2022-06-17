// #define  SortFilterPage // SortFilter //SortOnly // first 

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly BugTrackingContext _context;
        public Project? Project { get; set; }

        private readonly ILogger<DetailsModel> _logger;
        public DetailsModel(BugTrackingContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FirstOrDefaultAsync (m => m.ProjectId == id);

            _logger.LogDebug("Project details: {}", Project);
            
            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}