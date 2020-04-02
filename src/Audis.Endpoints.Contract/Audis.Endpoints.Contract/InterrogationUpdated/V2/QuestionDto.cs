using System;
using System.Collections.Generic;
using System.Text;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V2
{
    public class QuestionDto
    {
        public QuestionId Id { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
