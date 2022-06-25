using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using CRM.Entidades;

namespace CRM
{
    public class DapperBD
    {
        private string connectionString = @"Data Source=DESKTOP-FJJLQBR\SQLEXPRESS;Initial Catalog=test;Integrated Security=True;TrustServerCertificate=True";

        public DapperBD()
        {
            connect();
        }

        public void connect()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("conectados");
            }
        }

        public List<Trabajador> Select(String tabla)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM "+ tabla;
                return connection.Query<Trabajador>(sql).ToList();
            }
        }
        public void InsertTrabajador(string nombre,string telefono,string dni, string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var parametros = new DynamicParameters();
                parametros.Add("nombre", nombre);
                parametros.Add("telefono", telefono);
                parametros.Add("dni", dni);
                string sql = "";
                if (id.Equals("N"))
                {
                    sql = @"INSERT INTO empleados (nombre,telefono,dni)
                        VALUES (@nombre,@telefono,@dni)";
                }
                else
                {
                    parametros.Add("id", id);
                    sql = @"UPDATE empleados
                          SET nombre = @nombre,
                          telefono = @telefono,
                          dni = @dni
                          WHERE ID = @id";
                }
                connection.Execute(sql,parametros);
            }
        }


    }
}

