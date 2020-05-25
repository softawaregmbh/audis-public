using System;
using System.Collections.Generic;
using System.Text;

namespace Audis.Location.AutocompleteLocation.V1
{
    public class AutocompleteLocationRequestDto
    {
        public string Address { get; set; } = default!;
        public int? CursorPosition { get; set; }
        public string SessionToken { get; set; } = default!;
    }
}
