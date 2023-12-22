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
	public class GetDeviceByIdQueryHandler:IRequestHandler<GetDeviceByIdQuery, Device>
	{
		private readonly EntityDbContext entityDbContext;
		public GetDeviceByIdQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<Device> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.Id == request.id && device.IsDeleted == false)
					.FirstOrDefault();
			});
		}
	}
}
