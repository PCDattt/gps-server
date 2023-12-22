using DataTransferObject.Contexts;
using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.DeviceFeature.Queries
{
	public class GetDeviceBySerialNumberQueryHandler : IRequestHandler<GetDeviceBySerialNumberQuery, Device>
	{
		private readonly EntityDbContext entityDbContext;
		public GetDeviceBySerialNumberQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<Device> Handle(GetDeviceBySerialNumberQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.SerialNumber == request.serialNumber && device.IsDeleted == false)
					.FirstOrDefault();
			});
		}
	}
}
