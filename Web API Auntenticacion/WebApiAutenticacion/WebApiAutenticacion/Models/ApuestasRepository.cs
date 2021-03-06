﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PlaceMyBet.Models
{
    public class ApuestasRepository
    {
        private MySqlConnection Connect()
        {
            string connString = "Server=localhost;Port=3306;Database=PlaceMyBet;Uid=root;password=";
            MySqlConnection con = new MySqlConnection(connString);
            return con;
        }
        internal List<Apuesta> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from apuesta";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                Apuesta a = null;
                List<Apuesta> listaApuestas = new List<Apuesta>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetString(4) + " " + res.GetInt32(5) + " " + res.GetString(6) + " " + res.GetString(7));
                    a = new Apuesta(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetString(4), res.GetInt32(5), res.GetString(6), res.GetString(7));
                    listaApuestas.Add(a);
                }

                return listaApuestas;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
                return null;
            }
        }

        internal List<ApuestaDTO> RetrieveDTO()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from apuesta";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                ApuestaDTO a = null;
                List<ApuestaDTO> listaApuestas = new List<ApuestaDTO>();
                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetString(4) + " " + res.GetString(6), res.GetString(7));
                    a = new ApuestaDTO(res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetString(4), res.GetString(6), res.GetString(7));
                    listaApuestas.Add(a);
                }

                return listaApuestas;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
                return null;
            }
        }

        internal void Save(Apuesta a)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");

            culInfo.NumberFormat.NumberDecimalSeparator = ".";

            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;

            command.CommandText = "select * from mercado where idMercado=" + a.idMercado + ";";
            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();
                res.Read();
                Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetDouble(4) + " " + res.GetDouble(5) + " " + res.GetInt32(6));
                Mercado m = new Mercado(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt32(6));

                double dineroOver = 0;
                double dineroUnder = 0;
                if (a.tipoCuota == "over")
                {
                    dineroOver = a.dinero + m.dineroOver;
                    dineroUnder = m.dineroUnder;
                }
                else
                {
                    dineroOver = m.dineroOver;
                    dineroUnder = a.dinero + m.dineroUnder;
                }

                double cuotaOver = dineroOver / (dineroOver + dineroUnder);
                cuotaOver = (1 / cuotaOver) * 0.95;
                double cuotaUnder = dineroUnder / (dineroUnder + dineroOver);
                cuotaUnder = (1 / cuotaUnder) * 0.95;
                res.Close();
                con.Close();
                command.CommandText = "update mercado set cuotaOver=" + Math.Round(cuotaOver, 2) + ", cuotaUnder=" + Math.Round(cuotaUnder, 2) + ", dineroOver=" + dineroOver + ", dineroUnder=" + dineroUnder + " where idMercado=" + a.idMercado + ";";
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                    double cuotaApuesta = 0;
                    if (a.tipoCuota == "under")
                    {
                        cuotaApuesta = cuotaUnder;
                    }
                    else
                    {
                        cuotaApuesta = cuotaOver;
                    }
                    command.CommandText = "insert into apuesta(tipoMercado, cuota, dinero, fecha, idMercado, gmail, tipoCuota) values (" + a.tipoMercado + ", " + Math.Round(cuotaApuesta, 2) + ", " + a.dinero + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + a.idMercado + ", '" + a.gmail + "', '" + a.tipoCuota + "');";
                    try
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (MySqlException e)
                    {
                        Debug.WriteLine("Se ha producido un error de conexion");
                    }
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("Se ha producido un error de conexion");
                }

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
            }
        }

        internal List<ApuestaDTO2> RetrieveByEmailMercado(string g, double t)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from apuesta where tipoMercado = @A and gmail = @A2;";
            command.Parameters.AddWithValue("@A", t);
            command.Parameters.AddWithValue("@A2", g);

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                ApuestaDTO2 a = null;
                List<ApuestaDTO2> lista = new List<ApuestaDTO2>();
                while (res.Read())
                {

                    Debug.WriteLine("Recuperando: " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetString(7) + " " + res.GetInt32(0));
                    a = new ApuestaDTO2(res.GetString(7), res.GetDouble(2), res.GetDouble(3), res.GetInt32(0));
                    lista.Add(a);

                }
                con.Close();
                return lista;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
                return null;
            }
        }

        internal List<ApuestaDTO3> RetrieveByEmailIdMercado(string g, int i)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from apuesta where idMercado = @A and gmail = @A2;";
            command.Parameters.AddWithValue("@A", i);
            command.Parameters.AddWithValue("@A2", g);

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                ApuestaDTO3 a = null;
                List<ApuestaDTO3> lista = new List<ApuestaDTO3>();
                while (res.Read())
                {

                    Debug.WriteLine("Recuperando: " + res.GetDouble(1) + " " + res.GetString(7) + " " + res.GetDouble(2) + " " + res.GetDouble(3));
                    a = new ApuestaDTO3(res.GetDouble(1), res.GetString(7), res.GetDouble(2), res.GetDouble(3));
                    lista.Add(a);

                }
                con.Close();
                return lista;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexion");
                return null;
            }
        }
    }
}