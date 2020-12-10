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
    public class Patient
    {
        private string strConexao;
        public Patient()
        {
            strConexao = ConfigurationManager.ConnectionStrings["ConsultorioDB"].ConnectionString;
        }

        public void Incluir(Models.Entidade.Patient pPatient)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            DBConnection.Open();

            var insert = "INSERT INTO tbPatient(Nome,BirthDate,CPF,NomeDoctor) VALUES (@Nome,@BirthDate,@CPF,@NomeDoctor)";

            DBConnection.Execute(insert, pPatient);
        }

        public IEnumerable<dynamic> Listar(Models.Entidade.Patient pPatient)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var p = new DynamicParameters();

            //p.Add("@Id", (pPatient.Id == int.MinValue ? (int?)null : pPatient.Id));
            // p.Add("@Nome", pPatient.Nome == string.Empty ? (string)null : pPatient.Nome);

            DBConnection.Open();

            var select = "spSel_Patient";

            var Result = DBConnection.Query(select, p, commandType: CommandType.StoredProcedure);

            return Result;
        }

        public void Atualizar(Models.Entidade.Patient pPatient)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var update = "UPDATE tbPatient SET Nome = @Nome, BirthDate = @BirthDate, CPF = @CPF, NomeDoctor = @NomeDoctor WHERE Id = @Id";

            DBConnection.Open();

            DBConnection.Execute(update, pPatient);
        }

        public void Excluir(Models.Entidade.Patient pPatient)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var delete = "DELETE FROM tbPatient WHERE Id = @Id";

            DBConnection.Open();

            DBConnection.Execute(delete, pPatient);
        }

        public IEnumerable<dynamic> VerificaCpfExiste(Models.Entidade.Patient pPatient)
        {
            SqlConnection DBConnection = new SqlConnection(strConexao);

            var p = new DynamicParameters();

            p.Add("@CPF", (pPatient.CPF == string.Empty ? (string)null : pPatient.CPF));
            p.Add("@Id", (pPatient.Id <=0 ? (int?)null : pPatient.Id));

            DBConnection.Open();

            var select = "spSel_VerificaCpfExistente";

            var Result = DBConnection.Query(select, p, commandType: CommandType.StoredProcedure);

            return Result;
        }
    }
}