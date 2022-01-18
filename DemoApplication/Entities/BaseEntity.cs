namespace DemoApplication.Entities;

public abstract class BaseEntity: BaseDateEntity
{
    public string  EnteredBy { get; set; }
    public string  UpdatedBy { get; set; }
}