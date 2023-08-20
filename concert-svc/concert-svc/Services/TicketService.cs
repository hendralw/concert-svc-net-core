using AutoMapper;
using concert_svc.Entity;
using concert_svc.Enums;
using concert_svc.Model.Request;
using concert_svc.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace concert_svc.Services
{
    public class TicketService : ITicketService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ConcertService> _logger;

        public TicketService(DataContext db, ILogger<ConcertService> logger, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TicketResponse?> bookTicket(BookRequest request)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var validate = request.Validate();

                if (validate != null) throw new Exception(validate.message);

                var concert = await _db.Concert.FirstOrDefaultAsync(x => x.id.Equals(request.concert_id)) ?? 
                                                                    throw new Exception("concert not found");
                             
                var ticket = await _db.Ticket.FirstOrDefaultAsync(x => x.concert_id.Equals(request.concert_id.ToString()) &&
                                                                  x.type.Equals(request.type) &&
                                                                  x.available_qty > 0) ?? 
                                                                  throw new Exception("no available ticket found");

                await updateTicketAvailability(ticket);

                var ticketDto = _mapper.Map<TicketResponse>(ticket);

                await transaction.CommitAsync();

                return ticketDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while book ticket.");
                throw new Exception(ex.Message);
            }
        }

        private async Task updateTicketAvailability(Ticket ticket)
        {
            try
            {
                int updatedQty = ticket.available_qty - 1;
                if (updatedQty < 0) throw new Exception("no more available ticket");

                ticket.available_qty = updatedQty;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while update ticket.");
                throw new Exception(ex.Message);
            }         
        }

        public async Task<TicketResponse> updateTicket(Guid id, TicketRequest request)
        {
            try
            {
                var ticket = await _db.Ticket.FirstOrDefaultAsync(x => x.id.Equals(id)) ?? throw new Exception("ticket not found");
                var concert = await _db.Concert.FirstOrDefaultAsync(x => x.id.Equals(new Guid(request.concert_id))) ?? throw new Exception("concert not found, please check your concert_id");

                ticket.concert_id = request.concert_id;
                ticket.price = request.price;
                ticket.type = request.type;
                ticket.available_qty = request.available_qty;
                await _db.SaveChangesAsync();

                return _mapper.Map<TicketResponse>(ticket);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while update ticket.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<TicketResponse> insertTicket(TicketRequest request)
        {
            try
            {
                var checkTicketType = await _db.Ticket.FirstOrDefaultAsync(x => x.concert_id.Equals(request.concert_id) && x.type.Equals(request.type)) ?? throw new Exception("ticket for this concert type is exist");
                if (checkTicketType != null)
                    throw new Exception("ticket for this concert type is exist");

                var checkTicketExist = await _db.Ticket.FirstOrDefaultAsync(x => x.concert_id.Equals(request.concert_id)) ?? throw new Exception("concert not found, please check your concert_id");

                var ticket = new Ticket
                {
                    id = Guid.NewGuid(),
                    concert_id = request.concert_id,
                    price = request.price,
                    type = request.type,
                    available_qty = request.available_qty,
                };

                await _db.Ticket.AddAsync(ticket);
                await _db.SaveChangesAsync();

                return _mapper.Map<TicketResponse>(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while insert ticket.");
                throw new Exception(ex.Message);
            }
        }
    }
}
