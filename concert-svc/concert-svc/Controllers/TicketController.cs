using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using concert_svc.Helpers;
using concert_svc.Model.Response;
using concert_svc.Services;
using static concert_svc.Helpers.ResponseHelper;
using static concert_svc.Helpers.CommonHelper;
using concert_svc.Model.Request;
using System.ComponentModel.DataAnnotations;

namespace concert_svc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<ConcertService> _logger;


        public TicketController(ITicketService ticketService, ILogger<ConcertService> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpPost("booking")]
        public async Task<ActionResult<ResponseApi<TicketResponse>>> bookTicket(BookRequest request)
        {
            string endpointInfo = "/ticket/booking [POST]";

            _logger.LogInformation("Endpoint {} - Booking ticket : {}", endpointInfo, ToJson(request));

            var bookTicket = await _ticketService.bookTicket(request);

            _logger.LogInformation("Endpoint {} - Successfully booking ticket : {}", endpointInfo, ToJson(request));
            return ResponseHelper.Ok(bookTicket!, "success book ticket");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseApi<TicketResponse>>> updateTicket(Guid id, TicketRequest request)
        {
            string endpointInfo = "/ticket [PUT]";

            _logger.LogInformation("Endpoint {} - Update ticket by ID: {}", endpointInfo, ToJson(request));

            var concert = await _ticketService.updateTicket(id, request);

            _logger.LogInformation("Endpoint {} - Successfully update ticket with ID: {}", endpointInfo, ToJson(request));
            return ResponseHelper.Ok(concert, "success update data")!;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApi<TicketResponse>>> insertTicket(TicketRequest request)
        {
            string endpointInfo = "/ticket [PUT]";

            _logger.LogInformation("Endpoint {} - Insert ticket by ID: {}", endpointInfo, ToJson(request));

            var concert = await _ticketService.insertTicket(request);

            _logger.LogInformation("Endpoint {} - Successfully insert ticket : {}", endpointInfo, ToJson(request));
            return ResponseHelper.Ok(concert, "success update data")!;
        }

    }
}
