using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DesafioBuiltCode.Controllers
{
    public class PatientController : ApiController
    {
        public HttpResponseMessage Get()
        {
            Models.Entidade.Patient pPatient = new Models.Entidade.Patient();
            Models.Dados.Patient DBPatient = new Models.Dados.Patient();

            var teste = DBPatient.Listar(pPatient);

            return Request.CreateResponse(HttpStatusCode.OK, teste);
        }

        public string Post(Models.Entidade.Patient pPatient)
        {
            try
            {
                if (!VerificaCpfExiste(pPatient))
                {
                    if (!CpfIsInvalido(pPatient.CPF))
                    {
                        Models.Dados.Patient DBPatient = new Models.Dados.Patient();

                        DBPatient.Incluir(pPatient);

                        return "Inserido com Sucesso!";
                    }
                    else
                    {
                        return "CPF inválido!";
                    }
                }
                else
                {
                    return "CPF já existente!";
                }
            }
            catch (Exception)
            {

                return "Falha ao Inserir!";
            }
        }

        private bool CpfIsInvalido(string pCPF)
        {
            bool retorno = false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string temCpf;
            string digito;
            int soma;
            int resto;

            pCPF = pCPF.Trim();
            pCPF = pCPF.Replace(".", "").Replace("-", "");

            if (pCPF.Length != 11)
            {
                retorno = true;

                return retorno;
            }

            temCpf = pCPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(temCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            temCpf = temCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(temCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            if (!pCPF.EndsWith(digito) || pCPF == "00000000000")
            {
                retorno = true;

                return retorno;
            }

            return retorno;
        }

        private bool VerificaCpfExiste(Models.Entidade.Patient pPatient)
        {
            bool Existe = false;

            Models.Dados.Patient DBPatient = new Models.Dados.Patient();

            var CpfExistente = DBPatient.VerificaCpfExiste(pPatient);

            if (CpfExistente.Count() > 0)
            {
                Existe = true;
            }

            return Existe;
        }

        public string Put(Models.Entidade.Patient pPatient)
        {
            try
            {
                if (!VerificaCpfExiste(pPatient))
                {
                    if (!CpfIsInvalido(pPatient.CPF))
                    {
                        Models.Dados.Patient DBPatient = new Models.Dados.Patient();

                        DBPatient.Atualizar(pPatient);

                        return "Atualizado com Sucesso!";
                    }
                    else
                    {
                        return "CPF inválido!";
                    }
                }
                else
                {
                    return "CPF já existente!";
                }
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
                Models.Entidade.Patient pPatient = new Models.Entidade.Patient();
                Models.Dados.Patient DBPatient = new Models.Dados.Patient();

                pPatient.Id = Id;

                DBPatient.Excluir(pPatient);

                return "Excluido com Sucesso!";
            }
            catch (Exception)
            {

                return "Falha ao Excluir!";
            }
        }
    }
}
