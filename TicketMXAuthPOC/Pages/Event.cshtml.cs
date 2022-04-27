using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketMXAuthPOC.Models;
using TicketMXAuthPOC.Services;

namespace TicketMXAuthPOC.Pages
{
    public class EventModel : PageModel
    {
        #region CTOR, Fields.
        private readonly ILogger<EventModel> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly ITicketMXService _ticketMXService;

        public EventModel(ILogger<EventModel> logger, ITicketMXService ticketMXService, SignInManager<User> signInManager)
        {
            _logger = logger;
            _ticketMXService = ticketMXService;
            _signInManager = signInManager;
        }

        public string HtmlPage { get; set; }
        #endregion
        public async Task<IActionResult> OnGet()
        {
            var returnUrl = "/event";
            var accessToken = Request.Cookies[constants.AccessToken];
            if (string.IsNullOrEmpty(accessToken))
            {
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = returnUrl });
            }

            HtmlPage = await _ticketMXService.GetEvent(accessToken!);
            return Page();
        }
    }
}