using Newtonsoft.Json;
using System.Net;

namespace WorkBoard.Api.Exceptions
{
    public class ErrorResultModel
    {
        public ErrorResultModel()
        {
        }

        public ErrorResultModel(string message, HttpStatusCode status, string code, object payload = null)
        {
            Message = message;
            Status = status;
            Code = code;
            Payload = payload;
        }

        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Code { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Payload { get; set; }
    }
}
