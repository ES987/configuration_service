﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Configs
{
    public enum ServiceError { 
        Error,
         BadRequest
    }

    public enum ProviderRequestType
    {
        Unknow,
        TurnOn,
        AddProvider,
        UpdateProviderDataSource,
        UpdateProviderConfig,

    }
}
