using RequestHelpers.ConfigsHelpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Repositories.Entities
{
    public class ProviderConfigEntity
    {
        public Guid Id { get; set; }
        public object JsonConfig { get; set; }
        public ProviderType Type { get; set; }
        public string DataSource { get; set; }
        public string Description { get;set; }  
        public Guid ProgramId { get;set; }
        public bool IsStoped { get; set; }
    }
}
