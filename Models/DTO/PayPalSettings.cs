using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBL3.Models.DTO;

public class PayPalSettings
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Mode { get; set; }
}