using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Queries.Users;
using WorkBoard.Dtos;
using WorkBoard.Queries;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

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

            var queryParts = request.GetQueryParts();
            var parameters = new DynamicParameters(queryParts.parameters);
            var queryFilters = string.IsNullOrEmpty(queryParts.queryFilters) ? "1 = 1" : queryParts.queryFilters;
            var query = $"select * from [user] t0 where {queryFilters} {queryParts.sortAndPaging};";
            query += $"select count(*) from [user] t0 where {queryFilters};";

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
