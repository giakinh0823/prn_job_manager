﻿using System;
using System.Collections.Generic;

namespace prn_job_manager.Models
{
    public partial class QrtzLock
    {
        public string SchedName { get; set; } = null!;
        public string LockName { get; set; } = null!;
    }
}
