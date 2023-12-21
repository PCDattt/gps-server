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
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
	{
		private readonly EntityDbContext entityDbContext;
		public GetUserByIdQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.Id == request.id && user.IsDeleted == false)
					.FirstOrDefault();
			});
		}
	}
}
