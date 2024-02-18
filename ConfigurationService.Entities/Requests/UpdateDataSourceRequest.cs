using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Requests
{
    public class UpdateDataSourceRequest
    {
        
        public Guid ProviderId { get; set; }
        public string DataSource { get; set; }
    }
}
