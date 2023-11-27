using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Recursos
{
    public class ctlRecurso
    {
        public ctlRecurso()
        {
            con.ConnectionString = @"Data Source=DESKTOP-TDKLPB3\SQLSERVER_2022;Initial Catalog=BD_PIM;Integrated Security=True";
        }

        private string strconexao = @"Data Source=DESKTOP-TDKLPB3\SQLSERVER_2022;Initial Catalog=BD_PIM;Integrated Security=True";
        //SqlConnection conexaodb = null;
        private string query = string.Empty;


        //férias
        public int DiasExistentes(string id_funcionario)
        {
            SqlConnection sqlCon = new SqlConnection(strconexao);
            try
            {
                sqlCon.Open();

                query = "SELECT xferias FROM Funcionarios  WHERE id_funcionario = @id_funcionario";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                var id = cmd.CreateParameter();
                id.ParameterName = "@id_funcionario";
                id.SqlDbType = SqlDbType.Int;
                id.Value = id_funcionario;
                cmd.Parameters.Add(id);

                SqlDataReader dr = cmd.ExecuteReader();
                int dias = 0;
                while (dr.Read())
                {
                    dias = dr.GetInt32(0);
                }

                sqlCon.Close();
                return dias;
            }
            catch (Exception ex)
            {
                sqlCon.Close();
                return 0;
            }
        }
        public bool ColunasDeFerias(string id_funcionario, string d_ferias, string x_ferias)
        {
            SqlConnection sqlCon = new SqlConnection(strconexao);
            try
            {
                sqlCon.Open();

                query = "UPDATE Funcionarios SET dias_ferias = @dias_ferias, xferias = @xferias " +
                        "WHERE id_funcionario = @id_funcionario";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                var dias_ferias = cmd.CreateParameter();
                dias_ferias.ParameterName = "@dias_ferias";
                dias_ferias.SqlDbType = SqlDbType.Int;
                dias_ferias.Value = d_ferias;
                cmd.Parameters.Add(dias_ferias);

                var xferias = cmd.CreateParameter();
                xferias.ParameterName = "@xferias";
                xferias.SqlDbType = SqlDbType.Int;
                xferias.Value = x_ferias;
                cmd.Parameters.Add(xferias);

                var id = cmd.CreateParameter();
                id.ParameterName = "@id_funcionario";
                id.SqlDbType = SqlDbType.Int;
                id.Value = id_funcionario;
                cmd.Parameters.Add(id);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    sqlCon.Close();
                    return true;
                }
                else
                {
                    sqlCon.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                sqlCon.Close();
                return false;
            }
        }



        //login
        SqlConnection con = new SqlConnection();
        SqlDataReader dr;
        public bool retornoV = false;
        public string mensagemV = ""; // tudo ok se estiver vazio 

        public bool retornoA = false;
        public string mensagemA = ""; // tudo ok se estiuver vazio 
        public SqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        public bool acessarLogin(string login, string senha)
        {
            retornoA = verificarLogin(login, senha);
            if (!(mensagemV.Equals(""))) //mesagem preenchida com erro
            {
                this.mensagemA = mensagemV;
            }
            return retornoA;
        }

        public bool verificarLogin(string id_funcionario, string senha_real)
        {
            //verifica se existe no banco

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Funcionarios WHERE id_funcionario = @id_funcionario AND senha_real = @senha_real";
            cmd.Parameters.AddWithValue("@id_funcionario", id_funcionario);
            cmd.Parameters.AddWithValue("@senha_real", senha_real);

            try
            {
                cmd.Connection = conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) //se foi encontrado
                {
                    retornoV = true;
                }
            }
            catch (SqlException)
            {
                this.mensagemV = "Erro com o Banco de Dados!";
            }
            return retornoV;
        }





        //folha
        public List<string> pegarPonto(mdlRecurso _mdlRecurso)
        {
            SqlConnection sqlCon = new SqlConnection(strconexao);
            List<string> listaponto = new List<string>();
            try
            {
                sqlCon.Open();

                string query = "SELECT horas_trabalhadas, horas_faltas, extra_noturno, extra_diurno, " +
                                "extra_dom, extra_sab FROM Sistema_Ponto WHERE id_funcionario = @id_funcionario";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                var id = cmd.CreateParameter();
                id.ParameterName = "@id_funcionario";
                id.SqlDbType = SqlDbType.Int;
                id.Value = _mdlRecurso.id;
                cmd.Parameters.Add(id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    listaponto.Add(Convert.ToString(dr["horas_trabalhadas"]));
                    listaponto.Add(Convert.ToString(dr["horas_faltas"]));
                    listaponto.Add(Convert.ToString(dr["extra_noturno"]));
                    listaponto.Add(Convert.ToString(dr["extra_diurno"]));
                    listaponto.Add(Convert.ToString(dr["extra_dom"]));
                    listaponto.Add(Convert.ToString(dr["extra_sab"]));

                    sqlCon.Close();
                    return listaponto;
                }
                else
                {
                    sqlCon.Close();
                    listaponto = null;
                    _mdlRecurso.msg = "Funcionario não encontrado na área Ponto";
                    return listaponto;
                }
            }
            catch(Exception ex)
            {
                sqlCon.Close();
                listaponto = null;
                _mdlRecurso.msg = "Funcionario não encontrado na área Ponto.";
                return listaponto;
            }
        }

        
    }
} 
