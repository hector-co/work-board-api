using Newtonsoft.Json;
using System.Collections.Generic;

namespace WorkBoard.Application.Queries
{
    public class ResultModel<TData>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Meta { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalCount { get; set; }
    }
}
