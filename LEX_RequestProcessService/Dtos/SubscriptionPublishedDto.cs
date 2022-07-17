namespace LEX_RequestProcessService.Dtos;
public class SubscriptionPublishedDto
    {
        public int Id { get; set; }
         public string Key { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public int ServiceId { get; set; }
    }