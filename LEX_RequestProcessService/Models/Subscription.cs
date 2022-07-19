using System.ComponentModel.DataAnnotations;
// vezano uz pretplatu
public class Subscription
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Purpose { get; set; }
    public string Description { get; set; }
    public int ServiceId { get; set; }
}