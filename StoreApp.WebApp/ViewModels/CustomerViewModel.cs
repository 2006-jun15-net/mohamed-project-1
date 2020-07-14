using System.ComponentModel.DataAnnotations;


namespace StoreApp.WebApp.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
    }
}
