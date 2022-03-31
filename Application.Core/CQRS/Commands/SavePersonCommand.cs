using Application.Core.Common;
using Application.Core.Interface;
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
        private readonly IPersonRepository _personRepository;

        public SavePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<long> Handle(SavePersonCommand request, CancellationToken cancellationToken)
        {
            var deserializedObject = JsonSerializer.Deserialize<Person>(request.JsonData);

            var result = await _personRepository.AddAsync(deserializedObject, cancellationToken);

            return result.Id;
        }
    }
}
