using IT.DigitalCompany.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IT.DigitalCompany.Pages
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class TestModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public TestModel(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var appUser = await _userManager.GetUserAsync(this.User)
                .ConfigureAwait(false);
            var roles = await _userManager.GetRolesAsync(appUser!);

            var c = roles.Count();
        }
    }
}
