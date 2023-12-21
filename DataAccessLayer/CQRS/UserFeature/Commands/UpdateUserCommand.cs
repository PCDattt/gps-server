using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.UserFeature.Commands
{
	public class UpdateUserCommand : IRequest<bool>
	{
		public User user { get; set; }
		public string hashPassword { get; set; } = string.Empty;
	}
}
