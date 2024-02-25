using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Configs.Dto
{
    public class ProgramDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ProgramType { get; set; }
        public int DateCteate { get; set; }
         
    }
}
