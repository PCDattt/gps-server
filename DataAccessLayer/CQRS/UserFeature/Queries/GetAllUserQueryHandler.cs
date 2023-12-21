using DataTransferObject.Contexts;
using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.UserFeature.Queries
{
	public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<User>>
	{
		private readonly EntityDbContext entityDbContext;
		public GetAllUserQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<List<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.IsDeleted == false)
					.ToList();
			});
		}
	}
}
