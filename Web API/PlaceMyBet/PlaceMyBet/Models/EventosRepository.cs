using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class EventosRepository
    {
        private MySqlConnection Connect()
        {
            string connString = "Server=localhost;Port=3306;Database=PlaceMyBet;Uid=root;password=";
            MySqlConnection con = new MySqlConnection(connString);
            return con;
        }
        internal List<Evento> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from evento";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                Evento a = null;
                List<Evento> lista = new List<Evento>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetString(2) + " " + res.GetString(3));
                    a = new Evento(res.GetInt32(0), res.GetString(1), res.GetString(2), res.GetString(3));
                    lista.Add(a);
                }

                return lista;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
                return null;
            }
        }

        internal List<EventoDTO> RetrieveDTO()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from evento";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                EventoDTO a = null;
                List<EventoDTO> lista = new List<EventoDTO>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: "+ res.GetString(1) + " " + res.GetString(2) + " " + res.GetString(3));
                    a = new EventoDTO(res.GetString(1), res.GetString(2), res.GetString(3));
                    lista.Add(a);
                }

                return lista;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
                return null;
            }
        }

        internal EventoDTO2 RetrieveDTO2(string c)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from evento where local='" + c + "' OR visitante='"+c+"';";

                con.Open();
                MySqlDataReader res = command.ExecuteReader();
                EventoDTO2 e;
                double cantidad = 0;
                string rival = "";
                int id;
                res.Read();
                id = res.GetInt32(0);
                if (res.GetString(1).Equals(c))
                {
                    rival = res.GetString(2);
                }
                else
                {
                    rival = res.GetString(1);
                }
                con.Close();
                res.Close();
                command.CommandText = "select * from mercado where idEvento=" + id + ";";
                con.Open();
                MySqlDataReader res2 = command.ExecuteReader();
                while (res2.Read())
                {
                    cantidad = cantidad + res2.GetDouble(3) + res2.GetDouble(4);
                }
                if (cantidad == 0)
                {
                    e = new EventoDTO2(rival, "No existe ninguna apuesta"); 
                }
                else
                {
                    e = new EventoDTO2(rival, cantidad.ToString());
                }
                return e;

        }
    }
}