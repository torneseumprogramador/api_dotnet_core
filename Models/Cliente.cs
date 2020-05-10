using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Table("Clientes")]
    public class Cliente
    {
        private static DBContexto db;
        public Cliente()
        {
            db = new DBContexto();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public static DbSet<Cliente> Busca()
        {
            db = new DBContexto();
            return db.Clientes;
        }

        public Cliente Salvar()
        {
            db = new DBContexto();
            if(this.Id > 0)
            {
                db.Clientes.Update(this);
            }
            else
            {
                db.Clientes.Add(this);
            }
            db.SaveChanges();
            return this;
        }

        public static void Excluir(int id)
        {
            db = new DBContexto();
            var cliente = db.Clientes.Where(c => c.Id == id).First();
            db.Clientes.Remove(cliente);
            db.SaveChanges();
        }

        public static List<Cliente> Todos()
        {
            db = new DBContexto();
            return db.Clientes.ToList();
        }


        public static List<Cliente> Todos_Com_SqlConnection()
        {
            var lista = new List<Cliente>();

            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * FROM clientes", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString(),
                    Telefone = reader["Telefone"].ToString(),
                    Endereco = reader["Endereco"].ToString()
                });
            }

            conn.Close();
            conn.Dispose();
            cmd.Dispose();

            return lista;
        }

        public Cliente Salvar_Com_SqlConnection()
        {
            using (SqlConnection conn = new SqlConnection(Conexao.Dados))
            {
                conn.Open();


                if(this.Id > 0)
                {
                    SqlCommand cmd = new SqlCommand("update clientes set nome=@nome, endereco=@endereco, telefone=@telefone where id = @id", conn);
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = this.Id;

                    cmd.Parameters.Add("@nome", SqlDbType.VarChar);
                    cmd.Parameters["@nome"].Value = this.Nome;

                    cmd.Parameters.Add("@telefone", SqlDbType.VarChar);
                    cmd.Parameters["@telefone"].Value = this.Telefone;

                    cmd.Parameters.Add("@endereco", SqlDbType.VarChar);
                    cmd.Parameters["@endereco"].Value = this.Endereco;

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("CriarCliente @nome, @endereco, @telefone", conn);
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar);
                    cmd.Parameters["@nome"].Value = this.Nome;

                    cmd.Parameters.Add("@telefone", SqlDbType.VarChar);
                    cmd.Parameters["@telefone"].Value = this.Telefone;

                    cmd.Parameters.Add("@endereco", SqlDbType.VarChar);
                    cmd.Parameters["@endereco"].Value = this.Endereco;

                    this.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }

                conn.Close();
            }
                

            return this;
        }
    }
}
