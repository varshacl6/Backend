using System;

namespace DemoApplication.Entities;

public abstract class BaseDateEntity
{
    public DateTime  EnteredOn { get; set; }
    public DateTime  UpdatedOn { get; set; }
}