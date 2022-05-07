using Application.Core.Common;
using Application.Core.Interfaces;
using Application.Core.ProjectAggregate;
using MediatR;

namespace Application.Core.CQRS.Commands
{
    public class SavePersonCommand : IRequest<long>
    {
        public string JsonData { get; set; }
    }

    public class SavePersonCommandHandler : IRequestHandler<SavePersonCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        public SavePersonCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> Handle(SavePersonCommand request, CancellationToken cancellationToken)
        {
            var deserializedObject = JsonSerializer.Deserialize<Person>(request.JsonData);
            var result = await _dbContext.Persons.AddAsync(deserializedObject!, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }
    }
}
