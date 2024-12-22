using System;
using System.Collections.Generic;

namespace Examen_Test.Models;

public partial class ProductSale
{
    public int ProductSaleId { get; set; }

    public DateOnly SaleDate { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int? ClientServiceId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
