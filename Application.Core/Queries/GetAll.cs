using System.Text;
using Application.Core.Common;
using Application.Core.Interfaces;
using Application.Core.ProjectAggregate;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Queries;

public class GetAll
{
    public record Query(string? FirstName, string? LastName, string? City) : IRequest<string>;
    
    public class Handler : IRequestHandler<Query, string>
    {
        private readonly IAppDbContext _dbContext;

        public Handler(IAppDbContext dbContext) => _dbContext = dbContext;

        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            var predicate = BuildPredicate(request);
            var persons = await _dbContext.Persons.Include(person => person.Address).Where(predicate)
                .ToListAsync(cancellationToken);
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

        private static ExpressionStarter<Person> BuildPredicate(Query request)
        {
            var predicate = PredicateBuilder.New<Person>(true);

            if (!string.IsNullOrEmpty(request.FirstName))
                predicate = predicate.And(i => i.FirstName.Equals(request.FirstName));

            if (!string.IsNullOrEmpty(request.LastName))
                predicate = predicate.And(i => i.LastName.Equals(request.LastName));

            if (!string.IsNullOrEmpty(request.City))
                predicate = predicate.And(i => i.Address.City.Equals(request.City));
            return predicate;
        }
    }
}