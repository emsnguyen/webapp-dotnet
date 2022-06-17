#define  SortFilterPage // SortFilter //SortOnly // first 

using WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Projects
{
    #region snippet1
    public class IndexModel : PageModel
    {
        private readonly BugTrackingContext _context;

        public IndexModel(BugTrackingContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        #endregion

#if SortFilterPage
        #region snippet_SortFilterPageType
        public PaginatedList<Project> Projects { get; set; }
        #endregion
#else
        public IList<Project> Projects { get; set; }
#endif

#if first
        #region snippet_ScaffoldedIndex
        public async Task OnGetAsync()
        {
            Projects = await _context.Projects.ToListAsync();
        }
        #endregion
#endif

#if SortOnly
        #region snippet_SortOnly
        public async Task OnGetAsync(string sortOrder)
        {
        #region snippet_Ternary
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        #endregion

            IQueryable<Project> ProjectIQ = from s in _context.Projects
                                            select s;

            switch (sortOrder)
            {
                case "name_desc":
                    ProjectIQ = ProjectIQ.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    ProjectIQ = ProjectIQ.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    ProjectIQ = ProjectIQ.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    ProjectIQ = ProjectIQ.OrderBy(s => s.Name);
                    break;
            }

        #region snippet_SortOnlyRtn
            Projects = await ProjectIQ.AsNoTracking().ToListAsync();
        #endregion
        }
        #endregion
#endif
#if SortFilter
        #region snippet_SortFilter
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;

            IQueryable<Project> ProjectIQ = from s in _context.Projects
                                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ProjectIQ = ProjectIQ.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ProjectIQ = ProjectIQ.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    ProjectIQ = ProjectIQ.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    ProjectIQ = ProjectIQ.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    ProjectIQ = ProjectIQ.OrderBy(s => s.Name);
                    break;
            }

            Projects = await ProjectIQ.AsNoTracking().ToListAsync();
        }
        #endregion
#endif
#if SortFilterPage
        #region snippet_SortFilterPage
        #region snippet_SortFilterPage2
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        #endregion
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            #region snippet_SortFilterPage3
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            #endregion

            CurrentFilter = searchString;

            IQueryable<Project> ProjectIQ = from s in _context.Projects
                                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ProjectIQ = ProjectIQ.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ProjectIQ = ProjectIQ.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    ProjectIQ = ProjectIQ.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    ProjectIQ = ProjectIQ.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    ProjectIQ = ProjectIQ.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 20;
            #region snippet_SortFilterPage4
            Projects = await PaginatedList<Project>.CreateAsync(
                ProjectIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            #endregion
        }
        #endregion
#endif
    }
}