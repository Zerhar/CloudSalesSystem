namespace CloudSalesSystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public ICollection<Software> Softwares { get; set; } = new List<Software>();
    }
}
