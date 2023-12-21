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
	public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
	{
		private readonly EntityDbContext entityDbContext;
		public GetUserByEmailQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.Email == request.email && user.IsDeleted == false)
					.FirstOrDefault();
			});
		}
	}
}
