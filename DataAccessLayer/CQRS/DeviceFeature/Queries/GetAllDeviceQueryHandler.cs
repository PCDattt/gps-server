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
	public class GetAllDeviceQueryHandler : IRequestHandler<GetAllDeviceQuery, List<Device>>
	{
		private readonly EntityDbContext entityDbContext;
		public GetAllDeviceQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<List<Device>> Handle(GetAllDeviceQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Devices
					.Where(device => device.IsDeleted == false)
					.ToList();
			});
		}
	}
}
