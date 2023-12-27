using System.ComponentModel.DataAnnotations;

namespace AirLineWeb.Dtos
{
    public class WebhookSubscriptionReadDto
    {
        public int Id { get; set; }

        public string WebHookURi { get; set; }

        public string Secrect { get; set; }

        public string WebHookType { get; set; }

        public string WebHookPublisher { get; set; }
    }
}

