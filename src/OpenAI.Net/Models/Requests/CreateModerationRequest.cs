using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Models.Requests
{
    public class CreateModerationRequest
    {
        public CreateModerationRequest(string input)
        {
            Input = input;
        }

        /// <summary>
        /// The input text to classify <br />
        /// <see href="https://beta.openai.com/docs/api-reference/moderations/create#moderations/create-input" />
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Two content moderations models are available: text-moderation-stable and text-moderation-latest. <br />
        /// The default is text-moderation-latest which will be automatically upgraded over time. <br />
        /// This ensures you are always using our most accurate model. <br />
        /// If you use text-moderation-stable, we will provide advanced notice before updating the model. <br />
        /// Accuracy of text-moderation-stable may be slightly lower than for text-moderation-latest. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/moderations/create#moderations/create-model" />
        /// </summary>
        public string Model { get; set; }
    }
}
