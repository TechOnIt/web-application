using TechOnIt.Application.Reports.Relays;

namespace TechOnIt.Application.Queries.Relays.GetAllRelaysByStructureId;

public class GetAllRelaysByStructureIdQuery : IRequest<object>
{
    public Guid StructureId { get; set; }
}
public class GetAllRelaysByStructureIdQueryHandler : IRequestHandler<GetAllRelaysByStructureIdQuery, object>
{
    #region Ctor
    private readonly RelayReport _relayReport;
    public GetAllRelaysByStructureIdQueryHandler(RelayReport relayReport)
    {
        _relayReport = relayReport;
    }
    #endregion

    public async Task<object> Handle(GetAllRelaysByStructureIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var groupRelays = await _relayReport.GetRelaysByStructureIdNoTrackAsync<GetAllRelaysByStructureIdQueryResponse>(request.StructureId);

            if (groupRelays is null)
                return ResultExtention.NotFound($"cant find relay with structure id : {request.StructureId}");

            return groupRelays;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}

public class GetAllRelaysByStructureIdQueryResponse
{
    public string Id { get; set; }
    public int Pin { get; set; }
    public bool IsHigh { get; set; }
}