namespace WebAPIExample2.Models
{
    public class RequestedPart
    {
        public int RequestedPartId { get; set; }
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int Quantity { get; set; }
        public int PartRequestId { get; set; }
        public PartRequest PartRequest { get; set; }
    }
}
