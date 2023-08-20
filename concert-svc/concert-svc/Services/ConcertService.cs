using AutoMapper;
using concert_svc.Entity;
using concert_svc.Helpers;
using concert_svc.Model.Request;
using concert_svc.Model.Response;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using X.PagedList;
using static concert_svc.Helpers.CommonHelper;

namespace concert_svc.Services
{
    public class ConcertService : IConcertService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ConcertService> _logger;

        public ConcertService(DataContext db, ILogger<ConcertService> logger, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ConcertResponse?> getById(Guid id)
        {
            try
            {
                var concerts = await _db.Concert.FirstOrDefaultAsync(x => x.id.Equals(id)) ?? throw new Exception("concert not found");
                return _mapper.Map<ConcertResponse>(concerts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching concerts.");
                throw new Exception(ex.Message);
            }          
        }

        public async Task<Paging<ConcertResponse>> getAllConcert(int page, int pageSize)
        {
            try
            {
                var concerts = await _db.Concert.ToListAsync();
                var concertsDto = _mapper.Map<List<ConcertResponse>>(concerts).ToPagedList(page, pageSize);
                return CreatePagingModel(concertsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching concerts.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Paging<ConcertResponse>> searchConcert(string name, string venue, int page, int pageSize)
        {
            try
            {
                var query = _db.Concert.AsQueryable();

                query = query.Where(x => string.IsNullOrEmpty(name) || x.name.ToLower().Contains(name.ToLower()));
                query = query.Where(x => string.IsNullOrEmpty(venue) || x.venue.ToLower().Contains(venue.ToLower()));

                var concerts = await query.ToListAsync();
                var concertsDto = _mapper.Map<List<ConcertResponse>>(concerts).ToPagedList(page, pageSize);
                return CreatePagingModel(concertsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching concerts.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<ConcertResponse> deleteConcert(Guid id)
        {
            try
            {
                var concert = await _db.Concert.FirstOrDefaultAsync(x => x.id.Equals(id)) ?? throw new Exception("concert not found");
                var ticket = await _db.Ticket.FirstOrDefaultAsync(x => x.concert_id.Equals(id.ToString()));

                _db.Concert.Remove(concert);
                _db.Ticket.Remove(ticket!);
                await _db.SaveChangesAsync();

                return _mapper.Map<ConcertResponse>(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while delete concert.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<ConcertResponse> updateConcert(Guid id, ConcertRequest request)
        {
            try
            {
                var concert = await _db.Concert.FirstOrDefaultAsync(x => x.id.Equals(id)) ?? throw new Exception("concert not found");

                DateTime parsedDateTime;
                DateTime.TryParseExact(request.date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None , out parsedDateTime);
                parsedDateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);

                concert.name = request.name!;
                concert.date = parsedDateTime;
                concert.venue = request.venue!;
                await _db.SaveChangesAsync();

                return _mapper.Map<ConcertResponse>(concert);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while update concert.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<ConcertResponse> insertConcert(ConcertRequest request)
        {
            try
            {
                DateTime parsedDateTime;
                DateTime.TryParseExact(request.date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime);
                parsedDateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);

                var concert = new Concert
                {
                    id = Guid.NewGuid(),
                    name = request.name!,
                    date = parsedDateTime,
                    venue = request.venue!,                 
                };

                await _db.Concert.AddAsync(concert);
                await _db.SaveChangesAsync();

                return _mapper.Map<ConcertResponse>(concert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while insert concert.");
                throw new Exception(ex.Message);
            }
        }
    }
}
