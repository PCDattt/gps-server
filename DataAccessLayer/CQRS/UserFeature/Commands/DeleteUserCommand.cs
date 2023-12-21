using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.UserFeature.Commands
{
	public class DeleteUserCommand : IRequest<bool>
	{
		public int id { get; set; }
	}
}
