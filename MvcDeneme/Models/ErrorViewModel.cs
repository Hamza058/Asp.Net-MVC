using System;
using System.Collections.Generic;

namespace MvcDeneme.Models
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
