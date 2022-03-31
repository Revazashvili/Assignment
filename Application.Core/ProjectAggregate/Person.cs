using EntityBase = Application.SharedKernel.BaseEntity.BaseEntity;


namespace Application.Core.ProjectAggregate
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
