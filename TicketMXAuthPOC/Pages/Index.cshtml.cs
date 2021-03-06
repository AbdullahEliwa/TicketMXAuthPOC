using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketMXAuthPOC.Models;

namespace TicketMXAuthPOC.Pages
{
    public class IndexModel : PageModel
    {
        #region 
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(ILogger<IndexModel> logger, SignInManager<User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public string EventUrl { get; set; }
        #endregion

        public async Task<IActionResult> OnGet()
        {
            var returnUrl = "/";
            var accessToken = Request.Cookies[constants.AccessToken];
            if (string.IsNullOrEmpty(accessToken))
            {
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = returnUrl });
            }
            EventUrl = $"https://dev.ticketmx.com/ar/dt/687?authorization={accessToken}";
            return Page();
        }
    }
}