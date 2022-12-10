using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Dto;
using asp_net_core.Models;
using AutoMapper;

namespace asp_net_core.Helper {
	public class MappingProfiles : Profile {
		public MappingProfiles() {
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
			CreateMap<Todo, TodoDto>();
			CreateMap<TodoDto, Todo>();
		}
	}
}