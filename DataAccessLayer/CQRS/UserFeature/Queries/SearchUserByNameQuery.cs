using DataTransferObject.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CQRS.UserFeature.Queries
{
	public class SearchUserByNameQuery :IRequest<List<User>>
	{
		public string name { get; set; } = string.Empty;
	}
}
