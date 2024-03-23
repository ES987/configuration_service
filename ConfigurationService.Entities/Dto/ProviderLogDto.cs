using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Dto
{
    public class ProviderLogDto
    {
        public string Message { get; set; }
        public string LogLevel { get; set; }

        public long NanoSeconds { get; set; }

        public string DateTime { get; set; }
    }
}
