using Application.Core.Common;
using Application.Core.Interface;
using Application.Core.ProjectAggregate;
using LinqKit;
using MediatR;

namespace Application.Core.CQRS.Queries
{
    public class GetAllRequest : IRequest<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
    }

    public class GetAllRequestQueryHandler : IRequestHandler<GetAllRequest, string>
    {
        private readonly IPersonRepository _repository;

        public GetAllRequestQueryHandler(IPersonRepository repository)
        {
            _repository = repository;
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

            var persons = _repository.ListWithDependentObjectsAsync();

            persons = persons.Where(predicate).ToList();

            return JsonSerializer.Serialize(persons);
        }
    }
}
