using System.ComponentModel.DataAnnotations;

namespace DogusBlog.Models.Dto
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
