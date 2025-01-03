using System;
using System.Collections.Generic;

namespace FrodX.OrderProcessing.EFCore.Data;

public partial class QrtzLock
{
    public string SchedName { get; set; } = null!;

    public string LockName { get; set; } = null!;
}
