using System.ComponentModel.DataAnnotations;

namespace IdentityCMS.Models
{
    public class loginuser
    {
        [Required(ErrorMessage="Username Required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        /*public string appcode { get; set; }*/

    }
}
