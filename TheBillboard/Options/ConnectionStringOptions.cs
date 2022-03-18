﻿using System.ComponentModel.DataAnnotations;

namespace TheBillboard.Options;

public class ConnectionStringOptions
{
    [Required]
    public string DefaultDatabase { get; set; } = null!;    
    public string PostgreDatabase { get; set; } = null!;
}