



using META.DOMAIN;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace META.INFRA.Repository
{
    public class AudienciaRepository : IAudiencia
    {
 
        private readonly IConfiguration _mySettings;

        public AudienciaRepository(IConfiguration configuration)
        {
           
            _mySettings = configuration;
        }

        public string getDB()
        {
            string db = _mySettings.GetConnectionString("DB");
            return db;

        }

        public string CadastrarAudiencia(Audiencia audiencia)
        {
            try
            {
                SqlCommand sqlCmd;
                SqlDataReader reader;
                var sql = "INSERT INTO [dbo].[Audiencia] (Pontos_audiencia,Emissora_audiencia ) VALUES (@Pontos_audiencia,@Emissora_audiencia)";
                using (SqlConnection connection = new SqlConnection(getDB()))
                {
                    connection.Open();
                    sqlCmd = new SqlCommand(sql, connection);
                    sqlCmd.Parameters.Add("@Pontos_audiencia", SqlDbType.Int, 40);
                    sqlCmd.Parameters.Add("@Emissora_audiencia", SqlDbType.VarChar, 40);

                    sqlCmd.Parameters["@Pontos_audiencia"].Value = audiencia.Pontos_audiencia;
                    sqlCmd.Parameters["@Emissora_audiencia"].Value = audiencia.Emissora_audiencia.ToString();
                    sqlCmd.ExecuteNonQuery();

                    return "Audiência Cadastrada";


                }
            }
            catch (Exception ex)
            {
                return "ERRO : {0}" + ex.Message;
            }

        }
    }
}
