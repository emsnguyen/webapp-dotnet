using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly BugTrackingContext _context;

        public EditModel(BugTrackingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Project = await _context.Projects.FindAsync(id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            #region snippet_TryUpdateModelAsync

            var projectToUpdate = await _context.Projects.FindAsync(id);

            if (await TryUpdateModelAsync<Project>(
                projectToUpdate,
                "project",   // Prefix for form value.
                s => s.Name, s => s.Description))
            {
                #endregion
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return null;
        }
        #endregion
    }
}