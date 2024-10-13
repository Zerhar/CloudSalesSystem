using System.ComponentModel.DataAnnotations;

namespace CloudSalesSystem.Models
{
    public class Software
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        [Required(ErrorMessage = "Service Name is required.")]
        public string ServiceName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsActive { get; set; }
    }
}
