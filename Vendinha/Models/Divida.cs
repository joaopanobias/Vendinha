using System;
using System.Collections.Generic;
using System.Text;

namespace Vendinha.Models
{
    public class Divida
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Valor { get; set; }
        public bool Paga { get; set; } = false;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataPagamento { get; set; }

        public void ComoPaga()
        {
            Paga = true;
            DataPagamento = DateTime.Now;
        }
    }
}