﻿using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollyWithIoC
{
    public interface IBenPolicy
    {
        void ExecuteWithPolicy(Action action, IDictionary<string, object> customContext);

        ISyncPolicy GetPolicy();
    }
}
