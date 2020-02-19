using Qurl;
using System.Collections.Generic;
using System.Linq;
using WorkBoard.Queries.Users;

namespace WorkBoard.DataAccess.Dapper
{
    public static class QurlSqlExtensions
    {
        public static string AddFilter(this string filters, string newFilter)
        {
            if (string.IsNullOrEmpty(filters))
                return newFilter;
            return filters + " and " + newFilter;
        }

        public static (string queryFilters, string sortAndPaging, Dictionary<string, object> parameters) GetQueryParts(this UserDtoPagedQuery query)
        {
            var queryFilters = "";
            var parameters = new Dictionary<string, object>();
            (string queryFilter, Dictionary<string, object> parameters) result = ("", null);
            if (query.Filter.Id.TryGetSqlFilter("Id", out result))
            {
                queryFilters = queryFilters.AddFilter(result.queryFilter);
                parameters = parameters.Concat(result.parameters).ToDictionary(p => p.Key, p => p.Value);
            }
            if (query.Filter.Name.TryGetSqlFilter("Name", out result))
            {
                queryFilters = queryFilters.AddFilter(result.queryFilter);
                parameters = parameters.Concat(result.parameters).ToDictionary(p => p.Key, p => p.Value);
            }
            if (query.Filter.LastName.TryGetSqlFilter("LastName", out result))
            {
                queryFilters = queryFilters.AddFilter(result.queryFilter);
                parameters = parameters.Concat(result.parameters).ToDictionary(p => p.Key, p => p.Value);
            }
            if (query.Filter.Email.TryGetSqlFilter("Email", out result))
            {
                queryFilters = queryFilters.AddFilter(result.queryFilter);
                parameters = parameters.Concat(result.parameters).ToDictionary(p => p.Key, p => p.Value);
            }
            if (query.Filter.Username.TryGetSqlFilter("Username", out result))
            {
                queryFilters = queryFilters.AddFilter(result.queryFilter);
                parameters = parameters.Concat(result.parameters).ToDictionary(p => p.Key, p => p.Value);
            }
            if (query.Filter.Veryfied.TryGetSqlFilter("Veryfied", out result))
            {
                queryFilters = queryFilters.AddFilter(result.queryFilter);
                parameters = parameters.Concat(result.parameters).ToDictionary(p => p.Key, p => p.Value);
            }

            return (queryFilters, query.GetSortAndPaging(), parameters);
        }

        public static string GetSortAndPaging<TFilter>(this Query<TFilter> query)
            where TFilter : new()
        {
            var orderBy = "";
            foreach (var (property, direction) in query.Sorts)
            {
                if (!string.IsNullOrEmpty(orderBy)) orderBy += ", ";
                var sortProp = property.Replace(" ", "").Replace(";", "");
                orderBy += $"t0.[{sortProp}] {(direction == SortDirection.Descending ? "desc" : "")}";
            }
            if (!string.IsNullOrEmpty(orderBy)) orderBy = "order by " + orderBy;

            var paging = "";
            if (query.Offset > 0 || query.Limit > 0)
                paging += $"offset {query.Offset} rows";
            if (query.Limit > 0)
                paging += $" fetch next {query.Limit} rows only";
            return $"{orderBy} {paging}";
        }

        public static bool TryGetSqlFilter<T>(
            this FilterProperty<T> filter, string columnName, out (string queryFilter, Dictionary<string, object> parameters) result, string prefix = null, string filterName = null)
        {
            result = ("", null);
            if (filter == null) return false;

            if (string.IsNullOrEmpty(prefix)) prefix = "t0";

            filterName = filterName ?? columnName;
            var originalFilterName = prefix + "_" + filterName;
            filterName = "@" + originalFilterName;

            columnName = $"{prefix}.{columnName}";
            var queryResult = GetSqlFilter(filter as dynamic, columnName, filterName);
            if (string.IsNullOrEmpty(queryResult.Item1)) return false;
            result = (queryResult.Item1, queryResult.Item2);
            return true;
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this EqualsFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} = {filterName}", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this NotEqualsFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} <> {filterName}", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this LessThanFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} < {filterName}", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this LessThanOrEqualFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} <= {filterName}", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this GreaterThanFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} > {filterName}", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this GreaterThanOrEqualFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} >= {filterName}", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this ContainsFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} like concat('%', {filterName}, '%')", new Dictionary<string, object> { { filterName, filter.Value } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this InFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} in {filterName}", new Dictionary<string, object> { { filterName, filter.Values } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this NotInFilterProperty<T> filter, string columnName, string filterName)
        {
            return ($"{columnName} not in {filterName}", new Dictionary<string, object> { { filterName, filter.Values } });
        }

        private static (string queryFilter, Dictionary<string, object> parameters) GetSqlFilter<T>(this RangeFilterProperty<T> filter, string columnName, string filterName)
        {
            var queryFilter = "";
            var parameters = new Dictionary<string, object>();
            if (filter.From.IsSet)
            {
                queryFilter = queryFilter.AddFilter($"{columnName} >= {filterName}From");
                parameters.Add(filterName + "From", filter.From.Value);
            }
            if (filter.To.IsSet)
            {
                queryFilter = queryFilter.AddFilter($"{columnName} <= {filterName}To");
                parameters.Add(filterName + "To", filter.To.Value);
            }

            return (queryFilter, parameters);
        }
    }
}
