namespace LEX_RequestRecordsService.Dtos;
    public class RequestPublishedDto
    {
        public int Id { get; set; }
        public string IdentificationString { get; set; }
        public string IdentificationKey { get; set; }
        public string DeliveryKey { get; set; }
        public DateTime StartDate { get; set; }
        public string RequestTypeName { get; set; }  
        public string Event { get; set; }
        public string SourceKey { get; set; }
    }
