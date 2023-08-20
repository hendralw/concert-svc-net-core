namespace concert_svc.Entity
{
    public class Ticket
    {
        public Guid id { get; set; }
        public string concert_id { get; set; }
        public float price { get; set; }
        public string type { get; set; }
        public int available_qty { get; set; }
    }
}
