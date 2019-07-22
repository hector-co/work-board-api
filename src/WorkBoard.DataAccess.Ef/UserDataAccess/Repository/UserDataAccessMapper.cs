using System;
using Mapster;
using Hco.Base.DataAccess;
using WorkBoard.Domain.Model;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.UserDataAccess.Repository
{
    public class UserDataAccessMapper
    {
		static UserDataAccessMapper()
        {
            TypeAdapterConfig<User, UserDto>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.Version, src => src.AggregateVersion)
                .Map(dst => dst.Guid, src => src.AggregateGuid)
                .Map(dst => dst.Name, "_name")
                .Map(dst => dst.LastName, "_lastName")
                .Map(dst => dst.Username, "_username")
                .Map(dst => dst.Password, "_password")
                .Map(dst => dst.Email, "_email")
                .Map(dst => dst.Veryfied, "_veryfied")
                .IgnoreNonMapped(true);

			TypeAdapterConfig<UserDto, User>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.AggregateVersion, src => src.Version)
                .Map(dst => dst.AggregateGuid, src => src.Guid)
                .Map("_name", dst => dst.Name)
                .Map("_lastName", dst => dst.LastName)
                .Map("_username", dst => dst.Username)
                .Map("_password", dst => dst.Password)
                .Map("_email", dst => dst.Email)
                .Map("_veryfied", dst => dst.Veryfied)
                .IgnoreNonMapped(true)
                .ConstructUsing(dst => MapperHelper.CreateInstanceWithDefaultConstructor<User>());
        }

		public static UserDto Map(WorkBoardContext context, User user)
		{
			var userDto = new UserDto();
			Map(context, user, ref userDto);
			return userDto;
		}

		public static void Map(WorkBoardContext context, User user, ref UserDto userDto)
        {
            userDto = user.Adapt(userDto);
			// TODO map missing properties
        }

		public static User Map(WorkBoardContext context, UserDto userDto)
		{
			var user = MapperHelper.CreateInstanceWithDefaultConstructor<User>();
			Map(context, userDto, ref user);
			return user;
		}

		public static void Map(WorkBoardContext context, UserDto userDto, ref User user)
        {
            user = userDto.Adapt(user);
			// TODO map missing properties
        }
	}

}
