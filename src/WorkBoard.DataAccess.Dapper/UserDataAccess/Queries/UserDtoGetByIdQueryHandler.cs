using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Dtos;
using WorkBoard.Queries.Users;
using WorkBoard.Queries;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace WorkBoard.DataAccess.Dapper.UserDataAccess.Queries
{
    public class UserDtoGetByIdQueryHandler : IUserDtoGetByIdQueryHandler
    {
        private readonly string _connectionString;

        public UserDtoGetByIdQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WorkBoard");
        }

        public async Task<ResultModel<UserDto>> Handle(UserDtoGetByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "select * from [user] where id = @Id";
            
            var result = new ResultModel<UserDto>
            {
                Data = await connection.QueryFirstAsync<UserDto>(query, new { request.Id })
            };

            return result;
        }

    }
}
