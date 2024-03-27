using WEB_UI_MVC.Models;

namespace WEB_UI_MVC.Helpers
{
    public static class RazorHelpers
    {
        public static string ChoiceLayout(Utilisateurs user)
        {
            if (user.Admin == "YES")
            {
                return "~/Views/Shared/_LayoutAdmin.cshtml";
            }
            else if (user.Admin == "CDP")
            {
                return "~/Views/Shared/_LayoutCDP.cshtml";
            }
            return "~/Views/Shared/_LayoutUser.cshtml";
        }
    }
}
