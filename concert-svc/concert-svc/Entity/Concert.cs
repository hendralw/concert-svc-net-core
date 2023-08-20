namespace concert_svc.Entity
{
    public class Concert
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string venue { get; set; } = string.Empty;
        public DateTime date { get; set; }
    }
}
