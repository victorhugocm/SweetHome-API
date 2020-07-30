using System;
using System.Collections.Generic;

namespace SweetHome.API.Models
{
    public partial class VendaProduto
    {
        public long Id { get; set; }
        public long VendaId { get; set; }
        public long ProdutoId { get; set; }
        public long Quantidade { get; set; }
        public decimal PrecoProduto { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Venda Venda { get; set; }
    }
}
