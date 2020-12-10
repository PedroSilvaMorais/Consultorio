using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioBuiltCode.Models.Entidade
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Crm { get; set; }

        public string CrmUf { get; set; }
    }
}