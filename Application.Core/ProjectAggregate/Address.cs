using EntityBase = Application.SharedKernel.BaseEntity.BaseEntity;


namespace Application.Core.ProjectAggregate
{
    public class Address : EntityBase
    {
        public string City { get; set; }
        public string AddressLine { get; set; }
    }
}
