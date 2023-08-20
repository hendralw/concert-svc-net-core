using concert_svc.Model.Request;
using concert_svc.Model.Response;
using NPOI.HSSF.Record;

namespace concert_svc.Services
{
    public interface ITicketService
    {
        Task<TicketResponse?> bookTicket(BookRequest request);

        Task<TicketResponse> insertTicket(TicketRequest request);

        Task<TicketResponse> updateTicket(Guid id, TicketRequest request);
    }
}
