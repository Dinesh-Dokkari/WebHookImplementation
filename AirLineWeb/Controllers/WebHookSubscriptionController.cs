using AirLineWeb.Data;
using AirLineWeb.Dtos;
using AirLineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirLineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _db;
        private readonly IMapper _mapper;

        public WebHookSubscriptionController(AirlineDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            var subscriber = _db.WebHookSubscriptions.FirstOrDefault(s => s.WebHookURi == webhookSubscriptionCreateDto.WebHookURi);
            if (subscriber == null)
            {

                subscriber = _mapper.Map<WebHookSubscription>(webhookSubscriptionCreateDto);
                subscriber.WebHookPublisher = "AustralianAirLines";
                subscriber.Secrect = Guid.NewGuid().ToString();
                try
                {
                    _db.WebHookSubscriptions.Add(subscriber);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscriber);
                return Ok(webhookSubscriptionReadDto);
                //return CreatedAtRoute(nameof(GetSubscriptionBySecrect),new { secretId=webhookSubscriptionReadDto.Secrect}, webhookSubscriptionReadDto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
