using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PBL3.Infrastructures
{
    public static class ModelStateDictionaryExtension
    {
        public static void AddModelError(this ModelStateDictionary modelState, IdentityResult res)
        {
            foreach (var error in res.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
