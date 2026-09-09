using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Project.Common.DTO
{
    
        public class ResultDTO
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        public class ResultDTO<T>
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        }
    

}

