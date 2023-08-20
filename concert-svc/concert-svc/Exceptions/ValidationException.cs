using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations;
using static concert_svc.Helpers.ResponseHelper;

namespace concert_svc.Exceptions
{
    public static class ValidationException
    {
        public static ResponseApi<object> ValidateNullOrEmpty(object value, string errorMessage)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) || value.Equals(Guid.Empty))
            {
                var response = new ResponseApi<object>
                {
                    code = 400,
                    message = $"Validation errors occurred : {errorMessage}",
                    status = "error",
                    data = null
                };

                return response;
            }

            return null;
        }
    }
}
