namespace concert_svc.Model.Response
{
    public class TicketResponse
    {
        public Guid id { get; set; }
        public string concert_id { get; set; }
        public string type { get; set; }
        public float price { get; set; }
        public int available_qty { get; set; }
    }
}
