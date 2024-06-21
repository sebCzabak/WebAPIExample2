namespace WebAPIExample2.Models
{
    public class PartRequest
    {
        public int PartRequestId { get; set; }
        public string MechanicName { get; set; }
        public string Status { get; set; }
        public ICollection<RequestedPart> RequestedParts { get; set; }
        public string Parts { get; set; }
    }
}
