namespace LEX_RequestRecordsService.Dtos
{
    public class RequestReadDto
    {
        public int Id { get; set; }
        public string IdentificationString { get; set; }
        public string IdentificationKey { get; set; }
        public string DeliveryKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostNo { get; set; }
        public string RequestText { get; set; }
        public string RequestNote { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RequestTypeName { get; set; }  
    }
}