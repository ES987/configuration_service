using RequestHelpers.ConfigsHelpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Configs.Requests
{
    public class ProviderTurnOnRequest
    {
        public Guid ProgramId { get; set; }
        public ProviderRequestType RequestType { get; set; } = ProviderRequestType.Unknow;
        public TurnOnRequest Request { get; set; }
    }

    public class TurnOnRequest
    {
        public Guid ProviderId { get; set; }
        public int Pin { get; set; }
        public bool IsTurnOn { get; set; }
    }
}
