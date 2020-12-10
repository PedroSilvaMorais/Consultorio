using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioBuiltCode.Models.Entidade
{
    public class Patient
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime BirthDate { get; set; }

        public string CPF { get; set; }

        public string NomeDoctor { get; set; }

    }
}