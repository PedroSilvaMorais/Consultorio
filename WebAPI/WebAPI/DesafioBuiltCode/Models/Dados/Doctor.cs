using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DesafioBuiltCode.Models.Dados
{
    public class Doctor
    {
        private string strConexao;
        public Doctor()
        {
            strConexao = ConfigurationManager.ConnectionStrings["ConsultorioDB"].ConnectionString;
        }

        public void Incluir(Models.Entidade.Doctor pDoctor)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            DBConnection.Open();

            var insert = "INSERT INTO tbDoctor(Nome,Crm,CrmUf) VALUES (@Nome,@Crm,@CrmUf)";

            DBConnection.Execute(insert, pDoctor);
        }

        public IEnumerable<dynamic> Listar(Models.Entidade.Doctor pDoctor)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var p = new DynamicParameters();

            //p.Add("@Id", (pDoctor.Id == int.MinValue ? (int?)null : pDoctor.Id));
            // p.Add("@Nome", pDoctor.Nome == string.Empty ? (string)null : pDoctor.Nome);

            DBConnection.Open();

            var select = "spSel_Doctor";

            var Result = DBConnection.Query(select, p, commandType: CommandType.StoredProcedure);

            return Result;
        }

        public void Atualizar(Models.Entidade.Doctor pDoctor)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var update = "UPDATE tbDoctor SET Nome = @Nome, Crm = @Crm, CrmUf = @CrmUf WHERE Id = @Id";

            DBConnection.Open();

            DBConnection.Execute(update, pDoctor);
        }

        public void Excluir(Models.Entidade.Doctor pDoctor)
        {

            SqlConnection DBConnection = new SqlConnection(strConexao);

            var delete = "DELETE FROM tbDoctor WHERE Id = @Id";

            DBConnection.Open();

            DBConnection.Execute(delete, pDoctor);
        }

        public IEnumerable<dynamic> VerificaCrm(Models.Entidade.Doctor pDoctor)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var p = new DynamicParameters();

            p.Add("@Id", (pDoctor.Id <= 0 ? (int?)null : pDoctor.Id));
            p.Add("@Crm", (pDoctor.Crm == string.Empty ? (string)null : pDoctor.Crm));
            p.Add("@CrmUf", pDoctor.CrmUf == string.Empty ? (string)null : pDoctor.CrmUf);

            DBConnection.Open();

            var select = "spSel_VerificaCrm";

            var Result = DBConnection.Query(select, p, commandType: CommandType.StoredProcedure);

            return Result;
        }

        public IEnumerable<dynamic> VerificaVinculo(int pId)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var p = new DynamicParameters();

            p.Add("@IdDoctor", (pId <= 0 ? (int?)null : pId));

            DBConnection.Open();

            var select = "spSel_VerificaVinculo";

            var Result = DBConnection.Query(select, p, commandType: CommandType.StoredProcedure);

            return Result;
        }
    }
}