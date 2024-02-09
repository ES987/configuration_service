using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Configs.Requests
{
    public class ProviderRequest
    {
        public Guid ProgramId { get; set; }
        public ProviderRequestType RequestType { get; set; }
        public object Request { get; set; }

    }
}
