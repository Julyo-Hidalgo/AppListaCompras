using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppListaCompras.Model
{
    public class Produto
    {
        public int id { get; set; }
        public string desc { get; set; }
        public int qnt { get; set; }
        public double preco { get; set; }

    }
}
