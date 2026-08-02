using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementAPI.Responses
{
    class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        public string TraceId { get; set; } = string.Empty;

        public DateTime TimeStamp { get; set; }
    }
}
