using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Examen_Test.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public string? MainImagePath { get; set; }

    public int IsActive { get; set; }

    public int? ManufacturerId { get; set; }

    public virtual Active IsActiveNavigation { get; set; } = null!;

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();

    public Bitmap Image => MainImagePath != null ? new Bitmap($"Assets//{MainImagePath}") : null!;

    public string Actives => IsActive == 1 ? "Активен" : IsActive == 2 ? "Не Активен" : null!;

    SolidColorBrush Colors => IsActive == 1 ? new SolidColorBrush(Color.Parse("White")) : new SolidColorBrush(Color.Parse("Gray"));
}