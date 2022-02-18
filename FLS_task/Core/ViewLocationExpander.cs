using Microsoft.AspNetCore.Mvc.Razor;

namespace FLS_task.Core
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string featuresViewLocation = "~/Features/{1}/Views/{0}.cshtml";
            return viewLocations.Union(new[] { featuresViewLocation });
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
