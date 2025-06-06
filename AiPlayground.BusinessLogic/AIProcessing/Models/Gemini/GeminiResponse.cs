using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiPlayground.BusinessLogic.AIProcessing.Models.Gemini
{
    public class GeminiResponse
    {
        public List<Candidate> Candidates { get; set; } = [];
    }
}
