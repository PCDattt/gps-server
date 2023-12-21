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
	public class SearchUserByNameQueryHandler :IRequestHandler<SearchUserByNameQuery, List<User>>
	{
		private readonly EntityDbContext entityDbContext;
		public SearchUserByNameQueryHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<List<User>> Handle(SearchUserByNameQuery request, CancellationToken cancellationToken)
		{
			return await Task.Run(() =>
			{
				return entityDbContext.Users
					.Where(user => user.Name.Contains(request.name) && user.IsDeleted == false)
					.ToList();
			});
		}
	}
}
