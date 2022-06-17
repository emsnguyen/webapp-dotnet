using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly BugTrackingContext _context;

        public CreateModel(BugTrackingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Project = new Project
            {
                Name = "Project name",
                Description = "Describe the project",
            };

            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            #region snippet_TryUpdateModelAsync

            var emptyProject = new Project();

            if (await TryUpdateModelAsync<Project>(
                emptyProject,
                "project",   // Prefix for form value.
                s => s.Name, s => s.Description))
            {
                #endregion
                _context.Projects.Add(emptyProject);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return null;
        }
        #endregion
    }
}