using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Infrastructure
{
    public class CommonResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public CommonResponse() { }

        public CommonResponse(int errorCode, string message = "", object data = null)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Data = data;
        }
    }
}
