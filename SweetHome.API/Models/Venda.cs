using System;
using System.Collections.Generic;

namespace SweetHome.API.Models
{
    public partial class Venda
    {
        public long Id { get; set; }
        public long VendedorId { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime DataVenda { get; set; }

        public virtual Vendedor Vendedor { get; set; }
    }
}
