using System.ComponentModel.DataAnnotations;

namespace LEX_RequestRecordsService.Models;

    public class Request
    {
        [Key]
        [Required]
        public int Id { get; set; }
        // identifikacijska riječ ili riječi
        [Required]
        public string IdentificationString { get; set; }
        // može biti po email adresi, po ključu korisničkom računu usluge, po korisničkom računu usluge         
        [Required]
        public string IdentificationKey { get; set; }
        //  način se dostavlja odgovor: može biti po email adresi, poštom,     
        [Required]
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
        [Required]
        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }

    }
