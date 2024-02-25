using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Repositories.Entities
{
    public class ParametersBingingEntity
    {
        public int Id { get; set; }
        public Guid ProviderId { get; set; }
        public int Channel { get; set; }
        public int ParameterId { get; set; }

    }
}
