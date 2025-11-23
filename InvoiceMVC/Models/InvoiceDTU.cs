

using System.ComponentModel.DataAnnotations;

namespace InvoiceMVC.Models
{
    public class InvoiceDTU
    {
        [Required]
        public string Number { get; set; } = "";
        [Required]
        public DateTime? IssueDate { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public string Service { get; set; } = "";
        [Required, Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, Display(Name = "Client Name")]
        public string ClientName { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required, Phone]
        public string phone { get; set; } = "";

        public string Address { get; set; } = "";


    }
}
