using Microsoft.AspNetCore.Mvc;

namespace BOS.Webclient.Views.Helpers
{
    public static class UrlHelperExtensions
    {
        public static dynamic ApplicationUrls(this IUrlHelper urlHelper)
        {
            return new 
            {
                pages = new {
                  change_password = "/Identity/Account/Manage/ChangePassword",
                  account_index = urlHelper.Action("Index", "Account"),
                  account_login = urlHelper.Action("Login", "Account"),
                },
                api = new {
                  account_login = urlHelper.Action("Login", "Account"),
                }
            };
        }
    }
}