using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Repositories.Entities
{
    public class ProgramEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ProgramType { get; set; }
        public int DateCteate { get; set; }
    }
}
