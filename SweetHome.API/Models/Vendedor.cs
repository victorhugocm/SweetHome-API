using System;
using System.Collections.Generic;

namespace SweetHome.API.Models
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            Venda = new HashSet<Venda>();
        }

        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Comissao { get; set; }

        public virtual ICollection<Venda> Venda { get; set; }
    }
}
