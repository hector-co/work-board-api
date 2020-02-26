using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Queries.Users;
using WorkBoard.Dtos;
using WorkBoard.Queries;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;
using Microsoft.Data.SqlClient;
using Qurl.Dapper;

namespace WorkBoard.DataAccess.Dapper.UserDataAccess.Queries
{
    public class UserDtoPagedQueryHandler : IUserDtoPagedQueryHandler
    {
        private readonly string _connectionString;

        public UserDtoPagedQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WorkBoard");
        }

        public async Task<ResultModel<IEnumerable<UserDto>>> Handle(UserDtoPagedQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);

            var queryParts = request.GetQueryParts("User");
            var parameters = new DynamicParameters(queryParts.Parameters);
            var query = queryParts.GetSqlQuery();
            query += queryParts.GetSqlCountQuery(false);

            var resultSets = await connection.QueryMultipleAsync(query, parameters);
            var userDtos = (await resultSets.ReadAsync<UserDto>()).ToList();
            var totalCount = (await resultSets.ReadAsync<int>()).First();

            return new ResultModel<IEnumerable<UserDto>>
            {
                Data = userDtos,
                TotalCount = totalCount
            };
        }
    }
}
