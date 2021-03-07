using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Authentication.Response
{
    public class BaseResponse<T>
    {
        public BaseResponse(T data, bool status)
        {
            this.data = data;
            this.status = status;
        }

        public BaseResponse(T data, bool status, string errors)
        {
            this.data = data;
            this.status = status;
            this.errors = errors;
        }

        public T data;

        public bool status;

        public string errors;
       
    }
}
