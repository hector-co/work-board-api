using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Dtos;
using WorkBoard.Queries.Users;
using WorkBoard.Queries;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

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
                Data = await connection.QueryFirstOrDefaultAsync<UserDto>(query, new { request.Id })
            };

            return result;
        }

    }
}
