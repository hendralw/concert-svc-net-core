using concert_svc.Enums;
using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using NPOI.SS.Formula.Functions;
using static concert_svc.Helpers.ResponseHelper;
using static concert_svc.Exceptions.ValidationException;
namespace concert_svc.Model.Request
{
    public class BookRequest
    {
        public Guid concert_id { get; set; }

        [Required(ErrorMessage = "type is required")]
        [EnumDataType(typeof(TicketType), ErrorMessage = "invalid ticket type")]
        public string type { get; set; }

        public ResponseApi<object> Validate()
        {
            var response = ValidateNullOrEmpty(concert_id, "concert_id is required");

            if (response != null)
            {
                return response;
            }

            return null;
        }
    }
}
