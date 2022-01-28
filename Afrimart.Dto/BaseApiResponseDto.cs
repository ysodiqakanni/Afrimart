using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto
{
    public class BaseApiResponseDto<T> where T: class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
