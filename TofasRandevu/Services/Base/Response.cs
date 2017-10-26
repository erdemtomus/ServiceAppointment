using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace TofasRandevu.Services.Base
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "code")]
        public HttpStatusCode Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        public Response()
        {
        }

        public Response(HttpStatusCode httpStatusCode, string message)
        {
            this.Code = httpStatusCode;
            this.Message = message;
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
        public Response()
        {
        }
        public Response(HttpStatusCode httpStatusCode, string message, T data)
        {
            this.Code = httpStatusCode;
            this.Message = message;
            this.Data = data;
        }
    }
}