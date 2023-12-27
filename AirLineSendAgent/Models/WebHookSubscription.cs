using System.ComponentModel.DataAnnotations;

namespace AirLineSendAgent.Models
{
    public class WebHookSubscription
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string WebHookURi { get; set; }

        [Required]
        public string Secrect { get; set; }

        [Required]
        public string WebHookType { get; set; }

        [Required]
        public string WebHookPublisher { get; set;}
    }
}
