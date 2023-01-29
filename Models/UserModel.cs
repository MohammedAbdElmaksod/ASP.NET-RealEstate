using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class UserModel
    {
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "invalid email format")]
        public string email { get; set; }
        public string ReturnUrl { get; set; }
        [Required(ErrorMessage = "invalid password")]
        public string password { get; set; }
    }
}
