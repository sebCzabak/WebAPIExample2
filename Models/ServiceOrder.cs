using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIExample2.Models
{
    public class ServiceOrder
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        public string ServiceName { get; set; }
    }
}