namespace WebAPIExample2.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int UserId {  get; set; }
        public IEnumerable<int> ServicesIds { get; set;} = new List<int>();
        public string ServiceName { get; set; }
        public string Status { get; set; } = "Oczekujący na przyjęcie";
    }
}
