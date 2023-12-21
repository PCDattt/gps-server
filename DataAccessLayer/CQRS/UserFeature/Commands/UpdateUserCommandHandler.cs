using DataTransferObject.Contexts;
using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.UserFeature.Commands
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
	{
		private readonly EntityDbContext entityDbContext;
		public UpdateUserCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var record = entityDbContext.Users
				.Where(user => user.Email == request.user.Email && user.IsDeleted == false)
				.FirstOrDefault();
			if (record == null)
			{
				return false;
			}
			record.Name = request.user.Name;
			record.Username = request.user.Username;
			record.PasswordHash = request.hashPassword;
			record.ModifiedDate = DateTime.Now;
			_ = await entityDbContext.SaveChangesAsync();
			return true;
		}
	}
}
