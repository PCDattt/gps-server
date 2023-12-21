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
	public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
	{
		private readonly EntityDbContext entityDbContext;
		public AddUserCommandHandler(EntityDbContext entityDbContext)
		{
			this.entityDbContext = entityDbContext;
		}
		public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{
			var now = DateTime.Now;
			_ = await entityDbContext.Users.AddAsync(new()
			{
				Username = request.user.Username,
				PasswordHash = request.hashPassword,
				Email = request.user.Email,
				Name = request.user.Name,
				Role = request.user.Role,
				Permissions = request.user.Permissions,
				CreatedDate = now,
				ModifiedDate = now,
				IsDeleted = false
			});
			_ = entityDbContext.SaveChanges();
			return request.user;
		}
	}
}
