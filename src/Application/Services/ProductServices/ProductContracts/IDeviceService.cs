using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Services.ProductServices.ProductContracts;

public interface IDeviceService
{
    Task<DeviceViewModel?> CreateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken);
    Task<DeviceViewModel?> UpdateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken);
    Task<bool> DeleteDeviceByIdAsync(Guid DeviceId, CancellationToken cancellationToken);

    Task<DeviceViewModel?> FindDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken);
    Task<DeviceViewModel?> FindDeviceByIdAsyncAsNoTracking(Guid deviceId, CancellationToken cancellationToken);
}
