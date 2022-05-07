using Application.Core.Common;
using Application.Core.ProjectAggregate;
using LinqKit;
using MediatR;
using System.Text;
using Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.CQRS.Queries;

public class GetAllRequest : IRequest<string>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
}

public class GetAllRequestQueryHandler : IRequestHandler<GetAllRequest, string>
{
    private readonly IAppDbContext _dbContext;

    public GetAllRequestQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        #region Predicate Builder

        var predicate = PredicateBuilder.New<Person>(true);

        if (!string.IsNullOrEmpty(request.FirstName))
            predicate = predicate.And(i => i.FirstName.Equals(request.FirstName));

        if (!string.IsNullOrEmpty(request.LastName))
            predicate = predicate.And(i => i.LastName.Equals(request.LastName));

        if (!string.IsNullOrEmpty(request.City))
            predicate = predicate.And(i => i.Address.City.Equals(request.City));

        #endregion

        var persons = await _dbContext.Persons.Include(person => person.Address).ToListAsync(cancellationToken);

        persons = persons.Where(predicate).ToList();

        StringBuilder serializedJson = new();

        serializedJson.Append('[');

        foreach (var person in persons)
        {
            var serializedObject = JsonSerializer.Serialize(person);
            serializedJson.Append(serializedObject);
            serializedJson.Append(',');
        }

        serializedJson.Append(']');

        return serializedJson.ToString();
    }
}