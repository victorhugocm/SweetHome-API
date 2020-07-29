using System;
using System.Collections.Generic;

namespace SweetHome.API.Models
{
    public partial class Tamanho
    {
        public Tamanho()
        {
            Produto = new HashSet<Produto>();
        }

        public long Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Produto> Produto { get; set; }
    }
}
