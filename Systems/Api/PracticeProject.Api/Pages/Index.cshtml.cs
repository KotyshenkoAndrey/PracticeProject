using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeProject.Api.Settings;
using PracticeProject.Common;
using PracticeProject.Services.Settings;
using System.Reflection;

namespace PracticeProject.Api.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool OpenApiEnabled => settings.Enabled;

        [BindProperty]
        public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();


        [BindProperty]
        public string HelloMessage => apiSettings.HelloMessage;


        private readonly SwaggerSettings settings;
        private readonly ApiSpecialSettings apiSettings;

        public IndexModel(SwaggerSettings settings, ApiSpecialSettings apiSettings)
        {
            this.settings = settings;
            this.apiSettings = apiSettings;
        }

        public void OnGet()
        {

        }
    }
}
