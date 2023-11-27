using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Controle.Funcionario
{
    public class ctlFuncionario
    {
        private string strconexao = @"Data Source=DESKTOP-TDKLPB3\SQLSERVER_2022;Initial Catalog=BD_PIM;Integrated Security=True";
        SqlConnection conexaodb = null;
        private string query;
        
        public bool alterarFuncionario(mdlFuncionario _mdlFuncionario)
        {
            conexaodb = new SqlConnection(strconexao);

            try
            {
                conexaodb.Open();
                query = "UPDATE Funcionarios SET " +
                     "nome = @nome, segundo_nome = @segundo_nome, nascimento = @nascimento, idade = @idade, escolaridade = @escolaridade," +
                     "cargo = @cargo, setor = @setor, estado_civil = @estado_civil, rua = @rua, bairro = @bairro, cidade = @cidade, uf = @uf," +
                     "cep = @cep, numero = @numero, email = @email, celular = @celular, senha_real = @senha_real, acesso = @acesso," +
                     "periodo = @periodo, insalubridade = @insalubridade, periculosidade = @periculosidade," +
                     "rg = @rg, cpf = @cpf, n_ctps = @n_ctps, data_contratacao = @data_contratacao, dependentes = @dependentes," +
                     "conducoes = @conducoes, salario_bruto = @salario_bruto, v_alimentacao = @v_alimentacao," +
                     "v_refeicao = @v_refeicao WHERE cpf = @cpf";


                SqlCommand cmd = new SqlCommand(query, conexaodb);

                cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = _mdlFuncionario.nome;
                cmd.Parameters.Add("@segundo_nome", SqlDbType.NVarChar).Value = _mdlFuncionario.segundo_nome;
                cmd.Parameters.Add("@nascimento", SqlDbType.NVarChar).Value = _mdlFuncionario.nascimento;
                cmd.Parameters.Add("@idade", SqlDbType.Int).Value = _mdlFuncionario.idade;
                cmd.Parameters.Add("@escolaridade", SqlDbType.NVarChar).Value = _mdlFuncionario.escolaridade;
                cmd.Parameters.Add("@cargo", SqlDbType.NVarChar).Value = _mdlFuncionario.cargo;
                cmd.Parameters.Add("@setor", SqlDbType.NVarChar).Value = _mdlFuncionario.setor;
                cmd.Parameters.Add("@estado_civil", SqlDbType.NVarChar).Value = _mdlFuncionario.estado_civil;
                cmd.Parameters.Add("@rua", SqlDbType.NVarChar).Value = _mdlFuncionario.rua;
                cmd.Parameters.Add("@bairro", SqlDbType.NVarChar).Value = _mdlFuncionario.bairro;
                cmd.Parameters.Add("@cidade", SqlDbType.NVarChar).Value = _mdlFuncionario.cidade;
                cmd.Parameters.Add("@uf", SqlDbType.NVarChar).Value = _mdlFuncionario.uf;
                cmd.Parameters.Add("@cep", SqlDbType.NVarChar).Value = _mdlFuncionario.cep;
                cmd.Parameters.Add("@numero", SqlDbType.Int).Value = _mdlFuncionario.numero;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = _mdlFuncionario.email;
                cmd.Parameters.Add("@celular", SqlDbType.NVarChar).Value = _mdlFuncionario.celular;
                cmd.Parameters.Add("@senha_real", SqlDbType.Int).Value = _mdlFuncionario.senha_real;

                cmd.Parameters.Add("@acesso", SqlDbType.NVarChar).Value = _mdlFuncionario.acesso;
                cmd.Parameters.Add("@periodo", SqlDbType.NVarChar).Value = _mdlFuncionario.periodo;
                cmd.Parameters.Add("@insalubridade", SqlDbType.NVarChar).Value = _mdlFuncionario.insalubridade;
                cmd.Parameters.Add("@periculosidade", SqlDbType.NVarChar).Value = _mdlFuncionario.periculosidade;
                cmd.Parameters.Add("@rg", SqlDbType.NVarChar).Value = _mdlFuncionario.rg;
                cmd.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = _mdlFuncionario.cpf;
                cmd.Parameters.Add("@n_ctps", SqlDbType.NVarChar).Value = _mdlFuncionario.n_ctps;
                cmd.Parameters.Add("@data_contratacao", SqlDbType.NVarChar).Value = _mdlFuncionario.contratacao;
                cmd.Parameters.Add("@dependentes", SqlDbType.Int).Value = _mdlFuncionario.dependentes;
                cmd.Parameters.Add("@conducoes", SqlDbType.Int).Value = _mdlFuncionario.conducoes;
                cmd.Parameters.Add("@salario_bruto", SqlDbType.Float).Value = _mdlFuncionario.salario_bruto;

                cmd.Parameters.Add("@v_alimentacao", SqlDbType.Float).Value = float.Parse(_mdlFuncionario.vale_alimentacao);

                cmd.Parameters.Add("@v_refeicao", SqlDbType.Float).Value = float.Parse(_mdlFuncionario.vale_refeicao);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    conexaodb.Close();
                    return true;
                }
                else
                {
                    conexaodb.Close();
                    _mdlFuncionario.msg = "Candidato não encontrado.";
                    return false;
                }
            }
            catch (Exception)
            {
                conexaodb.Close();
                throw;
            }
        }

        public List<string> buscarFuncionario(mdlFuncionario _mdlFuncionario)
        {
            conexaodb = new SqlConnection(strconexao);

            string query = "SELECT nome, segundo_nome, nascimento, idade, escolaridade, cargo," +
                            "setor, estado_civil, rua, bairro, cidade, uf, cep, numero, email," +
                            "celular, senha_real, acesso, periodo, insalubridade," +
                            "periculosidade, rg, cpf, n_ctps, data_contratacao, dependentes," +
                            "conducoes, salario_bruto, dias_ferias, v_alimentacao, v_refeicao " +
                           "FROM Funcionarios WHERE cpf = @cpf";

            SqlCommand cmd = new SqlCommand(query, conexaodb);

            cmd.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = _mdlFuncionario.cpf;

            try
            {
                conexaodb.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<string> listafuncionario = new List<string>();

                if (dr.Read())
                {
                    listafuncionario.Add(Convert.ToString(dr["nome"]));
                    listafuncionario.Add(Convert.ToString(dr["segundo_nome"]));
                    listafuncionario.Add(Convert.ToString(dr["nascimento"]));
                    listafuncionario.Add(Convert.ToString(dr["idade"]));
                    listafuncionario.Add(Convert.ToString(dr["escolaridade"]));
                    listafuncionario.Add(Convert.ToString(dr["cargo"]));
                    listafuncionario.Add(Convert.ToString(dr["setor"]));
                    listafuncionario.Add(Convert.ToString(dr["estado_civil"]));
                    listafuncionario.Add(Convert.ToString(dr["rua"]));
                    listafuncionario.Add(Convert.ToString(dr["bairro"]));
                    listafuncionario.Add(Convert.ToString(dr["cidade"]));
                    listafuncionario.Add(Convert.ToString(dr["uf"]));
                    listafuncionario.Add(Convert.ToString(dr["cep"]));
                    listafuncionario.Add(Convert.ToString(dr["numero"]));
                    listafuncionario.Add(Convert.ToString(dr["email"]));
                    listafuncionario.Add(Convert.ToString(dr["celular"]));
                    listafuncionario.Add(Convert.ToString(dr["senha_real"]));

                    listafuncionario.Add(Convert.ToString(dr["acesso"]));
                    listafuncionario.Add(Convert.ToString(dr["periodo"]));
                    listafuncionario.Add(Convert.ToString(dr["insalubridade"]));
                    listafuncionario.Add(Convert.ToString(dr["periculosidade"]));
                    listafuncionario.Add(Convert.ToString(dr["rg"]));
                    listafuncionario.Add(Convert.ToString(dr["cpf"]));
                    listafuncionario.Add(Convert.ToString(dr["n_ctps"]));
                    listafuncionario.Add(Convert.ToString(dr["data_contratacao"]));
                    listafuncionario.Add(Convert.ToString(dr["dependentes"]));
                    listafuncionario.Add(Convert.ToString(dr["conducoes"]));
                    listafuncionario.Add(Convert.ToString(dr["salario_bruto"]));
                    listafuncionario.Add(Convert.ToString(dr["dias_ferias"]));
                    listafuncionario.Add(Convert.ToString(dr["v_alimentacao"]));
                    listafuncionario.Add(Convert.ToString(dr["v_refeicao"]));
                }
                else
                {
                    conexaodb.Close();
                    listafuncionario = null;
                    _mdlFuncionario.msg = "Funcionario não encontrado.";
                    return listafuncionario;
                }

                conexaodb.Close();
                return listafuncionario;
            }
            catch (Exception)
            {
                conexaodb.Close();
                throw;
            }
        }

        
    }
}
