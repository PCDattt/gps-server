using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject.Entities;

namespace DataAccessLayer.CQRS.UserFeature.Queries
{
	public class GetUserByIdQuery:IRequest<User>
	{
		public int id { get; set; }
	}
}
