namespace concert_svc.Model.Response
{
    public class ConcertResponse
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public DateTime date { get; set; }
        public string? venue { get; set; }
    }
}
