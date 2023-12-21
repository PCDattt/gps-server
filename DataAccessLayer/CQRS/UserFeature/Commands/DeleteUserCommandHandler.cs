using DataTransferObject.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.UserFeature.Commands
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
	{
		private readonly EntityDbContext entityDbContext;
		public DeleteUserCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var record = entityDbContext.Users
				.Where(user => user.Id == request.id && user.IsDeleted == false)
				.FirstOrDefault();
			if (record == null)
			{
				return false;
			}
			record.IsDeleted = true;
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
	}
}
