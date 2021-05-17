






using META.DOMAIN;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace META.INFRA.Repository
{
    public class EmissoraRepository : IEmissora
    {
        private readonly IConfiguration _mySettings;
        private readonly Context _context;
        public EmissoraRepository(IConfiguration configuration, Context context)
        {
            _context = context;
            _mySettings = configuration;
        }

      

        public string getDB()
         {
                    string db = _mySettings.GetConnectionString("DB");
                    return db;

          }
      public string CadastrarEmissora(Emissoras emissoras)
            {
            try
            {
                SqlCommand sqlCmd;
                SqlDataReader reader;
                var sql = "INSERT INTO [dbo].[Emissoras] (Emissoras) VALUES (@Emissoras)";
                using (SqlConnection connection = new SqlConnection(getDB()))
                {
                    connection.Open();
                    sqlCmd = new SqlCommand(sql, connection);

                    sqlCmd.Parameters.Add("@Emissoras", SqlDbType.VarChar, 40);
                    sqlCmd.Parameters["@Emissoras"].Value = emissoras.EMISSORAS.ToString();
                    sqlCmd.ExecuteNonQuery();

                    return "Emissora Cadastrada";


                }
            }
            catch (Exception ex)
            {
                return "ERRO : {0}" + ex.Message;
            }
           
        }
        public List<Emissoras> GetEmissora()
        {
            List<Emissoras> ListEmissoras = new List<Emissoras>();
        
            SqlCommand sqlCmd;
            SqlDataReader reader;


            var sql = "SELECT * FROM EMISSORAS";
            using (SqlConnection connection = new SqlConnection(getDB()))
            {
                connection.Open();
                sqlCmd = new SqlCommand(sql, connection);

           


                SqlDataReader rd = sqlCmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        var obj = new Emissoras();
                        obj.ID = (int)rd["id"];
                        obj.EMISSORAS = rd["Emissoras"].ToString();
                        ListEmissoras.Add(obj);
                    }

                }

                return ListEmissoras;

            }

        }

        public void DeletarEmissora(int id)
        {

            SqlCommand sqlCmd;
            SqlDataReader reader;
            var sql = "delete from [dbo].[Emissoras] where id = @id";
            using (SqlConnection connection = new SqlConnection(getDB()))
            {
                connection.Open();
                sqlCmd = new SqlCommand(sql, connection);

                sqlCmd.Parameters.Add("@id", SqlDbType.Int);
                sqlCmd.Parameters["@id"].Value = id;
                sqlCmd.ExecuteNonQuery();

        

            }

        }

        public string EditarEmissora(Emissoras emissoras)

        {
          
            SqlCommand sqlCmd;
            SqlDataReader reader;
           
            var sql = "UPDATE [dbo].[Emissoras] SET Emissoras = @Emissoras where ID = @ID";
            using (SqlConnection connection = new SqlConnection(getDB()))
            {
                connection.Open();
                sqlCmd = new SqlCommand(sql, connection);

                sqlCmd.Parameters.Add("@Emissoras", SqlDbType.VarChar, 40);
                sqlCmd.Parameters.Add("@ID", SqlDbType.Int, 40);

                sqlCmd.Parameters["@Emissoras"].Value = emissoras.EMISSORAS.ToString();
                sqlCmd.Parameters["@ID"].Value = emissoras.ID;

                sqlCmd.ExecuteNonQuery();
                

                return "Emissora Cadastrada";


            }

         
        }

        public Emissoras GetOneEmissora(int id)
        {
    
            Emissoras Emissoras = new Emissoras();
            SqlCommand sqlCmd;
            SqlDataReader reader;


            var sql = "SELECT * FROM EMISSORAS where id = @id";
            using (SqlConnection connection = new SqlConnection(getDB()))
            {
                connection.Open();
                sqlCmd = new SqlCommand(sql, connection);

                sqlCmd.Parameters.Add("@id", SqlDbType.Int);
                sqlCmd.Parameters["@id"].Value = id;
               
                
                SqlDataReader rd = sqlCmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                     
                        Emissoras.ID  = (int)rd["id"];
                        Emissoras.EMISSORAS = rd["Emissoras"].ToString();
                       
                    }

                }

                return Emissoras;

            }

        }

        public List<Emissoras> GetAudienciEmissora()
        {
            var lista = new List<Emissoras>();

          

            //  lista = _context.Emissoras.Include(f => f.audiencias).ToList();

            var sql = "SELECT * FROM EMISSORAS as Emiss INNER JOIN Audiencia as Audi on Audi.Emissora_audiencia = Emiss.Emissoras";

            using (var cmdo = _context.Database.GetDbConnection().CreateCommand())
            {
                cmdo.CommandText = sql;
                _context.Database.OpenConnection();
                using (var result = cmdo.ExecuteReader())
                {
                    while (result.Read())
                    {
                        var obj = new Emissoras();
                        obj.audiencias = new Audiencia();

                        obj.EMISSORAS = result["EMISSORAS"].ToString();
                        obj.audiencias.Pontos_audiencia = (int)result["Pontos_audiencia"];
                        obj.audiencias.Data_hora_audiencia = (DateTime)result["Data_hora_audiencia"];
                        lista.Add(obj);
                    }

                }
            }
            return lista;
        }

        public List<Emissoras> GetAudienciEmissoraPorVisao(Visao Modo)
        {
            var lista = new List<Emissoras>();
            var sql ="";
            if (Modo.Modo ==1)
            {
                sql = @"
                    SELECT sum(Pontos_audiencia) as Pontos_audiencia, Audi.Emissora_audiencia, Audi.Data_hora_audiencia
                    FROM EMISSORAS as Emiss
                    INNER JOIN Audiencia as Audi on Audi.Emissora_audiencia = Emiss.Emissoras
                    group by Audi.Emissora_audiencia, Audi.Data_hora_audiencia";

            }
            else if(Modo.Modo == 2)
            {
                 sql = @"SELECT AVG(Pontos_audiencia) as Pontos_audiencia, Audi.Emissora_audiencia, Audi.Data_hora_audiencia
                            FROM EMISSORAS as Emiss
                            INNER JOIN Audiencia as Audi on Audi.Emissora_audiencia = Emiss.Emissoras 
                            group by Audi.Emissora_audiencia, Audi.Data_hora_audiencia";
            }


            using (var cmdo = _context.Database.GetDbConnection().CreateCommand())
            {
                cmdo.CommandText = sql;
                _context.Database.OpenConnection();
                using (var result = cmdo.ExecuteReader())
                {
                    while (result.Read())
                    {
                        var obj = new Emissoras();
                        obj.audiencias = new Audiencia();

                        obj.EMISSORAS = result["Emissora_audiencia"].ToString();
                        obj.audiencias.Pontos_audiencia = (int)result["Pontos_audiencia"];
                        obj.audiencias.Data_hora_audiencia = (DateTime)result["Data_hora_audiencia"];
                        lista.Add(obj);
                    }

                }
            }
            return lista;
        }
    }
    
}
