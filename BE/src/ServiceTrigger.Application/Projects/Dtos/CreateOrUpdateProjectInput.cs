using System.ComponentModel.DataAnnotations;

namespace ServiceTrigger.Projects.Dtos
{
    public class CreateOrUpdateProjectInput
{
        [Required]
        public ProjectEditDto Project { get; set; }

}
}