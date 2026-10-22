using System;
using System.Collections.Generic;
using System.Text;

namespace Vendinha.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty;

        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }

        public List<Divida> Dividas { get; set; } = new();

        public decimal TotalDevido => Dividas.Where(d => !d.Paga).Sum(d => d.Valor);

        public bool DividaEmAberto => Dividas.Any(d => !d.Paga);
    }
}