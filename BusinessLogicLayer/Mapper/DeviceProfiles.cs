using AutoMapper;
using DataTransferObject.Entities;
using DataTransferObject.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mapper
{
	public class DeviceProfiles : Profile
	{
		public DeviceProfiles()
		{
			CreateMap<Device, DeviceResponse>();
		}
	}
}
