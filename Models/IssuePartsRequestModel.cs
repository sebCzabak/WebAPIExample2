namespace WebAPIExample2.Models
{
    public class IssuePartsRequestModel
    {
        public int RequestId { get; set; }
        public List<PartRequestDetail> Parts { get; set; }
    }
    public class PartRequestDetail
    {
        public int PartId { get; set; }
        public int Quantity { get; set; }
    }
}
