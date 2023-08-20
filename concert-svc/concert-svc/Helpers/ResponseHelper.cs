using Microsoft.AspNetCore.Mvc;

namespace concert_svc.Helpers
{
    public class ResponseHelper
    {
        public class ResponseApi<T>
        {
            public int code { get; set; }
            public string? message { get; set; }
            public string? status { get; set; }
            public T? data { get; set; }
        }


        private static readonly Dictionary<int, string> StatusMessages = new Dictionary<int, string>
        {
            { 200, "Success" },
            { 201, "Created" },
            { 400, "Bad Request" },
            { 404, "Not Found" },
            // Add more status codes and messages as needed
        };

        public static ResponseApi<T> CreateResponse<T>(T data, int code = 200, string? customMessage = null)
        {
            var statusMessage = StatusMessages.ContainsKey(code) ? StatusMessages[code] : "internal server error";
            var message = customMessage ?? statusMessage;

            return new ResponseApi<T>
            {
                code = code,
                message = message,
                status = statusMessage,
                data = data
            };
        }

        public static ActionResult<ResponseApi<T>> Ok<T>(T data, string customMessage)
        {
            return CreateResponse(data, 200, customMessage);
        }


        public static ActionResult<ResponseApi<T>> InternalServerError<T>(T data, string customMessage)
        {
            return CreateResponse(data, 500, customMessage);
        }

        public static ActionResult<ResponseApi<T>> BadRequest<T>(T data, string customMessage)
        {
            return CreateResponse(data, 400, customMessage);
        }

        public static ActionResult<ResponseApi<T>> NotFound<T>(T data, string customMessage)
        {
            return CreateResponse(data, 404, customMessage);
        }
    }
}
