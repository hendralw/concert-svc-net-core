using concert_svc.Helpers;
using concert_svc.Model.Response;
using concert_svc.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static concert_svc.Helpers.ResponseHelper;
using static concert_svc.Helpers.CommonHelper;
using concert_svc.Model.Request;

namespace concert_svc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService _concertService;
        private readonly ILogger<ConcertService> _logger;


        public ConcertController(IConcertService concertService, ILogger<ConcertService> logger) 
        {
            _concertService = concertService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<ConcertResponse>>> getById(Guid id)
        {
            string endpointInfo = string.Format("/concert/%s [GET]", id);

            _logger.LogInformation("Endpoint {} - Fetching concert by ID: {}", endpointInfo, id);

            var concert = await _concertService.getById(id);
           

            _logger.LogInformation("Endpoint {} - Successfully fetched concert with ID: {}", endpointInfo, id);
            return ResponseHelper.Ok(concert, "success get data")!;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseApi<Paging<ConcertResponse>>>> getAll (string? name, string? venue, int page = 1, int pageSize = 10)
        {
            string endpointInfo = "/concert [GET]";
            _logger.LogInformation("Endpoint {} - Fetching concerts with filters - Name: {}, Venue: {}, Page: {}, Size: {}", endpointInfo, name, venue, page, pageSize);

            Paging<ConcertResponse> concerts;
            if (AreAnyParamsNotNull(name!, venue!))
            {
                concerts = await _concertService.searchConcert(name!, venue!, page, pageSize);
                _logger.LogInformation("Endpoint {} - Found {} concerts matching filters.", endpointInfo, concerts.totalElements);
            }
            else 
            {
                concerts = await _concertService.getAllConcert(page, pageSize);
                _logger.LogInformation("Endpoint {} - Fetched {} concerts.", endpointInfo, concerts.totalElements);
            }

            return ResponseHelper.Ok(concerts, "success get data");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseApi<ConcertResponse>>> deleteConcert(Guid id)
        {
            string endpointInfo = string.Format("/concert/%s [DELETE]", id);

            _logger.LogInformation("Endpoint {} - Delete concert by ID: {}", endpointInfo, id);

            var concert = await _concertService.deleteConcert(id);

            _logger.LogInformation("Endpoint {} - Successfully delete concert with ID: {}", endpointInfo, id);
            return ResponseHelper.Ok(concert, "success delete data")!;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseApi<ConcertResponse>>> updateConcert(Guid id, ConcertRequest request)
        {
            string endpointInfo = "/concert [PUT]";

            _logger.LogInformation("Endpoint {} - Update concert by ID: {}", endpointInfo, ToJson(request));

            var concert = await _concertService.updateConcert(id, request);

            _logger.LogInformation("Endpoint {} - Successfully update concert with ID: {}", endpointInfo, ToJson(request));
            return ResponseHelper.Ok(concert, "success update data")!;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApi<ConcertResponse>>> insertConcert(ConcertRequest request)
        {
            string endpointInfo = "/concert [PUT]";

            _logger.LogInformation("Endpoint {} - Insert concert by ID: {}", ToJson(request));

            var concert = await _concertService.insertConcert(request);

            _logger.LogInformation("Endpoint {} - Successfully insert concert : {}", endpointInfo, ToJson(request));
            return ResponseHelper.Ok(concert, "success update data")!;
        }
    }
}
