using System;
using System.Collections.Generic;

namespace SweetHome.API.Models
{
    public partial class Produto
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public long TamanhoId { get; set; }
        public long CorId { get; set; }

        public virtual Cor Cor { get; set; }
        public virtual Tamanho Tamanho { get; set; }
    }
}
