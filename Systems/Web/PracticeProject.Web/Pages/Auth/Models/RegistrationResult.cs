using System.Text.Json.Serialization;

namespace PracticeProject.Web.Pages.Auth.Models
{
    public class RegistrationResult
    {
        public bool Successful { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}
