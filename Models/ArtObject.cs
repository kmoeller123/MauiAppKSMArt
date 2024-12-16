using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiAppKSMArt.Models;

public partial class ArtObject
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? ArtistName { get; set; }
  
    public string? Media { get; set; }

     public string? Genre { get; set; }

    public int? Year { get; set; }

    public string? Title { get; set; }

    public decimal? Price { get; set; }

    public int? Views { get; set; }

    public DateTime? LastViewed { get; set; }

    public string? FileType { get; set; }

    public string? Location { get; set; }

    public string? FileName { get; set; }
}
