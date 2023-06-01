using System;
using System.Collections.Generic;

namespace MCProducts.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public string? Image { get; set; }

    public int? Quantity { get; set; }
}
