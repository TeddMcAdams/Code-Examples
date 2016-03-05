namespace HRPortal.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}