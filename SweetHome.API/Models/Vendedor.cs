using System;
using System.Collections.Generic;

namespace SweetHome.API.Models
{
    public partial class Vendedor
    {
        public Vendedor() { }

        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Comissao { get; set; }
    }
}
