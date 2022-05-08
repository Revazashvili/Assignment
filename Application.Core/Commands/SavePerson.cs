using Application.Core.Common;
using Application.Core.Interfaces;
using Application.Core.ProjectAggregate;
using MediatR;

namespace Application.Core.Commands;

public class SavePerson
{
    public record Command(string JsonData) : IRequest<long>;
    
    public class Handler : IRequestHandler<Command, long>
    {
        private readonly IAppDbContext _dbContext;
        
        public Handler(IAppDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            var deserializedObject = JsonSerializer.Deserialize<Person>(request.JsonData);
            var result = await _dbContext.Persons.AddAsync(deserializedObject!, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }
    }
}