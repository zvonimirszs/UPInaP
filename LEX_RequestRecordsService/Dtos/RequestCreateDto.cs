

namespace LEX_RequestRecordsService.Dtos;

    public class RequestCreateDto
    {
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
    }

