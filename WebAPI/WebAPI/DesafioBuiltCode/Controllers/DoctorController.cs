using DesafioBuiltCode.Models.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DesafioBuiltCode.Controllers
{
    public class DoctorController : ApiController
    {
        public HttpResponseMessage Get()
        {
            Models.Entidade.Doctor pDoctor = new Models.Entidade.Doctor();
            Models.Dados.Doctor DBDoctor = new Models.Dados.Doctor();

            var Lista = DBDoctor.Listar(pDoctor);

            return Request.CreateResponse(HttpStatusCode.OK, Lista);
        }

        public string Post(Models.Entidade.Doctor pDoctor)
        {
            try
            {
                if (!VerificaCrm(pDoctor))
                {
                    Models.Dados.Doctor DBDoctor = new Models.Dados.Doctor();

                    DBDoctor.Incluir(pDoctor);

                    return "Inserido com Sucesso!";
                }
                else
                {
                    return "CRM já existente!";
                }
            }
            catch (Exception)
            {

                return "Falha ao Inserir!";
            }
        }

        private bool VerificaCrm(Doctor pDoctor)
        {
            bool Crm = false;

            Models.Dados.Doctor DBDoctor = new Models.Dados.Doctor();

            var CrmExistente = DBDoctor.VerificaCrm(pDoctor);

            if (CrmExistente.Count() > 0)
            {
                Crm = true;
            }

            return Crm;
        }

        public string Put(Models.Entidade.Doctor pDoctor)
        {
            try
            {
                Models.Dados.Doctor DBDoctor = new Models.Dados.Doctor();

                DBDoctor.Atualizar(pDoctor);

                return "Atualizado com Sucesso!";
            }
            catch (Exception)
            {

                return "Falha ao Atualizar!";
            }
        }

        public string Delete(int Id)
        {
            try
            {
                if (!PossuiVinculo(Id))
                {
                
                    Models.Entidade.Doctor pDoctor = new Models.Entidade.Doctor();
                    Models.Dados.Doctor DBDoctor = new Models.Dados.Doctor();

                    pDoctor.Id = Id;

                    DBDoctor.Excluir(pDoctor);

                    return "Excluido com Sucesso!";

                }
                else
                {
                    return "Não foi possivel excluir doctor está vinculado a um paciente!";
                }
            }
            catch (Exception)
            {

                return "Falha ao Excluir!";
            }
        }

        private bool PossuiVinculo(int pId)
        {
            bool PossuiVinculo = false;

            Models.Dados.Doctor DBDoctor = new Models.Dados.Doctor();

            var Existe = DBDoctor.VerificaVinculo(pId);

            if (Existe.Count() > 0)
            {
                PossuiVinculo = true;
            }

            return PossuiVinculo;
        }
    }
}
