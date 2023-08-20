using concert_svc.Enums;
using System.ComponentModel.DataAnnotations;
using static concert_svc.Helpers.ResponseHelper;
using static concert_svc.Exceptions.ValidationException;

namespace concert_svc.Model.Request
{
    public class TicketRequest
    {
        [Required(ErrorMessage = "concert_id is required")]
        public string concert_id { get; set; }

        [Required(ErrorMessage = "price is required")]
        public float price { get; set; }

        [Required(ErrorMessage = "type is required")]
        [EnumDataType(typeof(TicketType), ErrorMessage = "invalid ticket type")]
        public string type { get; set; }

        [Required(ErrorMessage = "available_qty is required")]
        public int available_qty { get; set; }

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
