using System;
using System.Collections.Generic;

namespace Examen_Test.Models;

public partial class Active
{
    public int ActiveId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
