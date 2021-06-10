using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;

namespace OAJ.WebService.Internal.Helpers
{
    internal static class MvcOptionsExtensions
    {
        public static void UseGeneralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));
        }

        public static void UseGeneralRoutePrefix(this MvcOptions opts, string
        prefix)
        {
            opts.UseGeneralRoutePrefix(new RouteAttribute(prefix));
        }
    }

    internal sealed class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel RoutePrefix;

        public RoutePrefixConvention(IRouteTemplateProvider route)
        {
            RoutePrefix = new AttributeRouteModel(route);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(RoutePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    selector.AttributeRouteModel = RoutePrefix;
                }
            }
        }
    }
}
