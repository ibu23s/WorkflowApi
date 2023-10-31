namespace Workflow.Api.Models
{
    public class Workflow
{
    public Workflow()
    {
        Name = string.Empty; 
        Description = string.Empty; 
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

}
