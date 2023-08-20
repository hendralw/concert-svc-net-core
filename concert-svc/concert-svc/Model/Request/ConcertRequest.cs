using System.ComponentModel.DataAnnotations;
using static concert_svc.Helpers.ResponseHelper;
using static concert_svc.Exceptions.ValidationException;

namespace concert_svc.Model.Request
{
    public class ConcertRequest
    {
        //public Guid id { get; set; }

        [Required(ErrorMessage = "name is required")]
        public string? name { get; set; }

        [Required(ErrorMessage = "date is required")]
        public string? date { get; set; }

        [Required(ErrorMessage = "venue is required")]
        public string? venue { get; set; }

        //public ResponseApi<object> Validate()
        //{
        //    var response = ValidateNullOrEmpty(id, "concert_id is required");

        //    if (response != null)
        //    {
        //        return response;
        //    }

        //    return null;
        //}
    }
}
