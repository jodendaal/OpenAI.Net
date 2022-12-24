using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Models.Responses
{
    public class FineTuneEventsResponse
    {
        public string Object { get; set; }
        public Event[] Data { get; set; }
    }
}
