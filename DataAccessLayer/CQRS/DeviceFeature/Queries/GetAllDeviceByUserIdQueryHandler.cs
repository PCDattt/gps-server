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
	public class GetAllDeviceByUserIdQueryHandler : IRequestHandler<GetAllDeviceByUserIdQuery, List<Device>>
	{
		private readonly EntityDbContext entityDbContext;
		public GetAllDeviceByUserIdQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<List<Device>> Handle(GetAllDeviceByUserIdQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.UserId == request.id && device.IsDeleted == false)
					.ToList();
			});
		}
	}
}
