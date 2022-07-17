namespace LEX_RequestProcessService.Models;
// tip odovora: jednoistavan ili kompleksan 
public class ResponseType
{
    public int Id { get; set; }
    public string Name { get; set; }        
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}