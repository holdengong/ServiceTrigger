using System.ComponentModel.DataAnnotations;

namespace ServiceTrigger.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}