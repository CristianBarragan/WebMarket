using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace WebMarket.Model.Api
{
    public class ApiResponse
    {
        [DataMember]
        public HttpStatusCode StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message = "", object result = null)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }
    }
}
