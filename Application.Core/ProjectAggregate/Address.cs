using Application.Core.Base;

namespace Application.Core.ProjectAggregate;

public class Address : BaseEntity
{
    public string City { get; set; }
    public string AddressLine { get; set; }
}