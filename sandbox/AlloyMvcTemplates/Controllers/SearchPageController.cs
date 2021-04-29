using System.Linq;
using AlloyMvcTemplates.Controllers;
using AlloyMvcTemplates.Models.Pages;
using AlloyMvcTemplates.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlloyMvcTemplates.Controllers
{
    public class SearchPageController : PageControllerBase<SearchPage>
    {
        public ViewResult Index(SearchPage currentPage, string q)
        {
            var model = new SearchContentModel(currentPage)
            {
                Hits = Enumerable.Empty<SearchContentModel.SearchHit>(),
                NumberOfHits = 0,
                SearchServiceDisabled = true,
                SearchedQuery = q
            };

            return View(model);
        }
    }
}
