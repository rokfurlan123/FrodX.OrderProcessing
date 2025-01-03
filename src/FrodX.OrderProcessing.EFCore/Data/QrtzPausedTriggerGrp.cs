using System;
using System.Collections.Generic;

namespace FrodX.OrderProcessing.EFCore.Data;

public partial class QrtzPausedTriggerGrp
{
    public string SchedName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;
}
