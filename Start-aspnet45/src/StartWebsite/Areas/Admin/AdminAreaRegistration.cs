using System.Web.Mvc;

namespace Start.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Start.Areas.Admin.Controllers" }
            );
        }

        public override string AreaName
        {
            get { return AdminConstants.Area; }
        }
    }
}