// #define  SortFilterPage // SortFilter //SortOnly // first 

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly BugTrackingContext _context;
        [BindProperty]
        public Project Project { get; set; }
        public string ErrorMessage { get; set; }
        
        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(BugTrackingContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(long? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FindAsync(id);

            _logger.LogDebug("Project details: {}", Project);

            if (Project == null)
            {
                return NotFound();
            }

            if (saveChangesError == true)
            {
                ErrorMessage = "Delete failed. Try again!";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            _logger.LogDebug("Delete Project OnPostAsync. id: {}", id);
            if (id == null)
            {
                return NotFound();
            }

            var projectToDelete = await _context.Projects.FindAsync(id);
            _logger.LogDebug("OnPostAsync. projectToDelete: {}", projectToDelete);

            if (projectToDelete == null)
            {
                return NotFound();
            }
            try
            {
                _context.Projects.Remove(projectToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("./Delete", new { id = id, saveChangesError = true });
            }
        }
    }
}