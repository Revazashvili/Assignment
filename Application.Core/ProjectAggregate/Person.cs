﻿using Application.Core.Base;

namespace Application.Core.ProjectAggregate;

public class Person : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public long? AddressId { get; set; }
    public virtual Address Address { get; set; }
}