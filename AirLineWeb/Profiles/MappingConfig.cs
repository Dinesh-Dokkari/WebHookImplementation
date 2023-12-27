using AirLineWeb.Dtos;
using AirLineWeb.Models;
using AutoMapper;

namespace AirLineWeb.Profiles
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<WebHookSubscription,WebhookSubscriptionCreateDto>().ReverseMap();
            CreateMap<WebHookSubscription,WebhookSubscriptionReadDto>().ReverseMap();


        }
    }

}
