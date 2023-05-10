//using SkillTracker.Search.Application.Services.Search.Commands;

//namespace SkillTracker.Search.Application.Services.Search.CommandHandlers;

//public class SearchProfileCommandHandler : IRequestHandler<SearchProfileCommand, bool>
//{
//    private readonly IEventBus _bus;

//    public SearchProfileCommandHandler(IEventBus bus)
//    {
//        _bus = bus;
//    }
//    public async Task<bool> Handle(SearchProfileCommand request, CancellationToken cancellationToken)
//    {

//        _bus.Publish(new SearchProfileEvent()
//        {
//            AssociateId = request.AssociateId,
//            Name = request.Name,
//            Skill = request.Skill
//        });
//        return await Task.FromResult(true);
//    }
//}