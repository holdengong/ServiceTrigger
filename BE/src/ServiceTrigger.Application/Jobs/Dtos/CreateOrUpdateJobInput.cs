using System.ComponentModel.DataAnnotations;

namespace ServiceTrigger.Jobs.Dtos
{
    public class CreateOrUpdateJobInput
{
        [Required]
        public JobEditDto Job { get; set; }

}
}