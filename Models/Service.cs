namespace WebAPIExample2.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    }
}
