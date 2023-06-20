using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvrSystem.Application.IvrFeatures.CallManagerFeatures.Models
{
    public class Call_CallManagerCommandRequest
    {
        public string CallerPhone { get; set; }
        public string TwilioPhone { get; set; }
    }
}
