using System;
using System.Collections.Generic;

namespace FrodX.OrderProcessing.EFCore.Data;

public partial class Order
{
    public Guid OrderId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;
}
