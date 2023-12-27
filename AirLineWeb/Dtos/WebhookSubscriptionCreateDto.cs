using System.ComponentModel.DataAnnotations;

namespace AirLineWeb.Dtos
{
    public class WebhookSubscriptionCreateDto
    {


        [Required]
        public string WebHookURi { get; set; }

        [Required]
        public string WebHookType { get; set; }


    }
}

