using concert_svc.Model.Request;
using concert_svc.Model.Response;
using X.PagedList;

namespace concert_svc.Services
{
    public interface IConcertService
    {
        Task<ConcertResponse?> getById(Guid id);

        Task<Paging<ConcertResponse>> getAllConcert(int page, int pageSize);

        Task<Paging<ConcertResponse>> searchConcert(string name, string venue, int page, int pageSize);

        Task<ConcertResponse> deleteConcert(Guid id);

        Task<ConcertResponse> updateConcert(Guid id, ConcertRequest concert);

        Task<ConcertResponse> insertConcert(ConcertRequest concert);
    }
}
