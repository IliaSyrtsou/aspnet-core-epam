using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Web.Components
{
    public class BreadcrumbsComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() {
            var breadcrumbs = new BreadcrumbsModel($"Home{Request.Path.ToString()}");
            return await Task.FromResult(View(breadcrumbs));
        }
    }

    public class BreadcrumbsModel {
        public string Breadcrumbs {get;set;}
        public BreadcrumbsModel(string breadcrumbs) {
            Breadcrumbs = breadcrumbs;
        }
    }
}