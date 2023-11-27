using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Controle
{
    public class ctlCandidato
    {
        private string strconexao = @"Data Source=DESKTOP-TDKLPB3\SQLSERVER_2022;Initial Catalog=BD_PIM;Integrated Security=True";
        SqlConnection conexaodb = null;
        private string query;


        public List<string> buscarCandidato(mdlCandidatoo _mdlCandidato)
        {
            conexaodb = new SqlConnection(strconexao);

            string query = "SELECT nome, segundo_nome, nascimento, idade, escolaridade," +
                           "cargo, setor, estado_civil, rua, bairro, cidade, uf, cep, " +
                           "pretensao_salarial, email, celular, senha_real " +
                           "FROM Candidatos WHERE id_candidato = @id_candidato";

            SqlCommand cmd = new SqlCommand(query, conexaodb);

            cmd.Parameters.Add("@id_candidato", SqlDbType.Int).Value = int.Parse(_mdlCandidato.id_candidato);

            try
            {
                conexaodb.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<string> listacandidato = new List<string>();

                if (dr.Read())
                {

                    listacandidato.Add(Convert.ToString(dr["nome"]));
                    listacandidato.Add(Convert.ToString(dr["segundo_nome"]));
                    listacandidato.Add(Convert.ToString(dr["nascimento"]));
                    listacandidato.Add(Convert.ToString(dr["idade"]));
                    listacandidato.Add(Convert.ToString(dr["escolaridade"]));
                    listacandidato.Add(Convert.ToString(dr["cargo"]));
                    listacandidato.Add(Convert.ToString(dr["setor"]));
                    listacandidato.Add(Convert.ToString(dr["estado_civil"]));
                    listacandidato.Add(Convert.ToString(dr["rua"]));
                    listacandidato.Add(Convert.ToString(dr["bairro"]));
                    listacandidato.Add(Convert.ToString(dr["cidade"]));
                    listacandidato.Add(Convert.ToString(dr["uf"]));
                    listacandidato.Add(Convert.ToString(dr["cep"]));
                    listacandidato.Add(Convert.ToString(dr["pretensao_salarial"]));
                    listacandidato.Add(Convert.ToString(dr["email"]));
                    listacandidato.Add(Convert.ToString(dr["celular"]));
                    listacandidato.Add(Convert.ToString(dr["senha_real"]));

                }
                else
                {
                    conexaodb.Close();
                    listacandidato = null;
                    _mdlCandidato.msg = "Candidato não encontrado.";
                    return listacandidato;
                }

                conexaodb.Close();
                return listacandidato;
            }
            catch (Exception)
            {
                conexaodb.Close();
                throw;
            }
        }

        public bool alterarCandidato(mdlCandidatoo _mdlCandidato)
        {
            conexaodb = new SqlConnection(strconexao);

            try
            {
                conexaodb.Open();
                query = "UPDATE Candidatos SET " +
                     " escolaridade = @escolaridade, cargo = @cargo, setor = @setor, estado_civil = @estado_civil," +
                     "rua = @rua, bairro = @bairro, cidade = @cidade, uf = @uf, cep = @cep, pretensao_salarial = @pretensao_salarial," +
                     "email = @email, celular=@celular, senha_real = @senha_real WHERE id_candidato = @id_candidato";

                SqlCommand cmd = new SqlCommand(query, conexaodb);

                cmd.Parameters.Add("@id_candidato", SqlDbType.Int).Value = Convert.ToInt32(_mdlCandidato.id_candidato);

                cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = _mdlCandidato.nome;
                cmd.Parameters.Add("@segundo_nome", SqlDbType.NVarChar).Value = _mdlCandidato.segundo_nome;
                cmd.Parameters.Add("@escolaridade", SqlDbType.NVarChar).Value = _mdlCandidato.escolaridade;
                cmd.Parameters.Add("@cargo", SqlDbType.NVarChar).Value = _mdlCandidato.cargo;
                cmd.Parameters.Add("@setor", SqlDbType.NVarChar).Value = _mdlCandidato.setor;
                cmd.Parameters.Add("@estado_civil", SqlDbType.NVarChar).Value = _mdlCandidato.estado_civil;
                cmd.Parameters.Add("@rua", SqlDbType.NVarChar).Value = _mdlCandidato.rua;
                cmd.Parameters.Add("@bairro", SqlDbType.NVarChar).Value = _mdlCandidato.bairro;
                cmd.Parameters.Add("@cidade", SqlDbType.NVarChar).Value = _mdlCandidato.cidade;
                cmd.Parameters.Add("@uf", SqlDbType.NVarChar).Value = _mdlCandidato.uf;
                cmd.Parameters.Add("@cep", SqlDbType.NVarChar).Value = _mdlCandidato.cep;
                cmd.Parameters.Add("@pretensao_salarial", SqlDbType.Float).Value = _mdlCandidato.pretensao_salarial;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = _mdlCandidato.email;
                cmd.Parameters.Add("@celular", SqlDbType.NVarChar).Value = _mdlCandidato.celular;
                cmd.Parameters.Add("@senha_real", SqlDbType.Int).Value = Convert.ToInt32(_mdlCandidato.senha_real);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    conexaodb.Close();
                    return true;
                }
                else
                {
                    conexaodb.Close();
                    _mdlCandidato.msg = "Candidato não encontrado.";
                    return false;
                }
            }
            catch (Exception)
            {
                conexaodb.Close();
                throw;
            }
        }
    }
}
