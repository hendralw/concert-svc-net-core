namespace concert_svc.Model.Response
{
    public class Paging <T>
    {
        public List<T>? content { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public long totalElements { get; set; }
    }
}
