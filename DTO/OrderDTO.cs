using System.ComponentModel.DataAnnotations.Schema;
using WebAPIExample2.Models;

namespace WebAPIExample2.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public bool Complaint { get; set; } = false;
        public ICollection<int> ServicesIds { get; set; } = [];
        public int UserId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public string Status { get; set; } = "Złożony";
    }
}
