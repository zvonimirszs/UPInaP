using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace LEX_SubscriptionService.Models;

public class Subscription
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }        
    [Required]
    public string Key { get; set; }
    [Required]
    public string Purpose { get; set; }
    public string Description { get; set; }
    [Required]
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    // kategorija osobnih podataka
    public string Category { get; set; }
    // pravnu osnovu za obradu
    public string LawfulnessofProcessing { get; set; }
    //legitimne interese voditelja obrade - ako je obrada po članku 6, stavki 1
    public string LegitimateInterestsDesc { get; set; }
    //primatelje ili kategorije primatelja osobnih podataka
    public string Recipients { get; set; }
    //prenijeti trećoj zemlji  DA ili NE
    public bool TransferThirdCountry { get; set; }
    // izvoru od kuda su podaci
    public string Sources { get; set; }
    // aktivacija pretplate
    public DateTime? StartDate { get; set; }
    // deaktivacija pretplate - 
    public DateTime? EndDate { get; set; }
}

public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }