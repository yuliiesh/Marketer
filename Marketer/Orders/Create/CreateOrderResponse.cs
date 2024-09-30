﻿using System;
using System.Collections.Generic;
using Marketer.Data.Models;

namespace Marketer.Orders.Create;

public class CreateOrderResponse
{
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalPrice;
    public ICollection<ProductModel> Products { get; set; }
}