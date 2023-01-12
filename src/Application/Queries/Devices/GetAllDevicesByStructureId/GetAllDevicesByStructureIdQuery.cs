using TechOnIt.Application.Reports.Devices;

namespace TechOnIt.Application.Queries.Devices.GetAllDevicesByStructureId;

public class GetAllDevicesByStructureIdQuery : IRequest<object>
{
    public Guid StructureId { get; set; }
}
public class GetAllDevicesByStructureIdQueryHandler : IRequestHandler<GetAllDevicesByStructureIdQuery, object>
{
    #region Ctor
    private readonly IDeviceReport _deviceReport;
    public GetAllDevicesByStructureIdQueryHandler(IDeviceReport deviceReport)
    {
        _deviceReport = deviceReport;
    }
    #endregion

    public async Task<object> Handle(GetAllDevicesByStructureIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var placeDevices = await _deviceReport.GetDevicesByStructureIdNoTrackAsync<GetAllDevicesByStructureIdQueryResponse>(request.StructureId);

            if (placeDevices is null)
                return ResultExtention.NotFound($"cant find device with structure id : {request.StructureId}");

            return placeDevices;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}

public class GetAllDevicesByStructureIdQueryResponse
{
    public string Id { get; set; }
    public int Pin { get; set; }
    public bool IsHigh { get; set; }
}