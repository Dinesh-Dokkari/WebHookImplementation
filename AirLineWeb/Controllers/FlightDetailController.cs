using AirLineWeb.Data;
using AirLineWeb.Dtos;
using AirLineWeb.MessageBus;
using AirLineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirLineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailController : ControllerBase
    {

        private readonly AirlineDbContext _db;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBus;

        public FlightDetailController(AirlineDbContext db, IMapper mapper, IMessageBusClient messageBus)
        {
            _db = db;
            _mapper = mapper;
            _messageBus = messageBus;
        }


        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlight(FlightDetailCreateDto flightDetailCreateDto)
        {
            var flight = _db.FlightDetails.FirstOrDefault(f => f.FlightCode == flightDetailCreateDto.FlightCode);

            if (flight == null)
            {
                var flightDetailModel = _mapper.Map<FlightDetail>(flightDetailCreateDto);

                try
                {
                    _db.FlightDetails.Add(flightDetailModel);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                var flightDetailReadDto = _mapper.Map<FlightDetailReadDto>(flightDetailModel);

                //return CreatedAtRoute(nameof(GetFlightDetailsByCode), new { flightCode = flightDetailReadDto.FlightCode }, flightDetailReadDto);
                return Ok(flightDetailReadDto);

            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFlightDetail(int id, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            var flight = _db.FlightDetails.FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            decimal oldPrice = flight.Price;

            _mapper.Map(flightDetailUpdateDto, flight);

            try
            {
                _db.SaveChanges();
                if (oldPrice != flight.Price)
                {
                    Console.WriteLine("Price Changed - Place message on bus");

                    var message = new NotificationMessageDto
                    {
                        WebhookType = "pricechange",
                        OldPrice = oldPrice,
                        NewPrice = flight.Price,
                        FlightCode = flight.FlightCode
                    };
                    _messageBus.SendMessage(message);
                }
                else
                {
                    Console.WriteLine("No Price change");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

    }

}