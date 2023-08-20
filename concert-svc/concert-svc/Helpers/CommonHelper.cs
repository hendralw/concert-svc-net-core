using concert_svc.Model.Response;
using System.Text.Json;
using X.PagedList;

namespace concert_svc.Helpers
{
    public static class CommonHelper
    {
        public static Paging<T> CreatePagingModel<T>(IPagedList<T> pagedData)
        {
            var pagingModel = new Paging<T>
            {
                content = pagedData.ToList(),
                pageNumber = pagedData.PageNumber,
                pageSize = pagedData.PageSize,
                totalElements = pagedData.TotalItemCount
            };

            return pagingModel;
        }

        public static bool AreAnyParamsNotNull(params object[] parameters)
        {
            return parameters.Any(param => param != null);
        }

        public static string ToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true // Make the JSON output indented for better readability
            });
        }
    }
}
