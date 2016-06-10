using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;
using Antlr.Runtime;
using Oracle.DataAccess.Client;
using PatheAsp.Models;
using Pathe_ASP2015.Models;


namespace Participation_ASP.Models
{
    public static class DatabaseManager
    {

        public static void TestConnection()
        {
            using (OracleConnection con = Connection)
            {
                try
                {
                    con.Open();
                    con.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets a new OracleConnection with it's connection string set (using 'OracleConnectionString' from the web config).
        /// </summary>
        private static OracleConnection Connection
        {
            get
            {
                return
                    new OracleConnection(
                        ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString);
            }
        }

        /// <summary>
        /// Creates an OracleCommand for the given query using the static OracleConnection field, and sets the CommandType to CommandType.Text (used for plain text queries).
        /// Used prior to adding parameters and executing the query.
        /// </summary>
        /// <param name="connection">The connection information, which should be made using the Connection property.</param>
        /// <param name="sql">Query string to run</param>
        /// <returns>OracleCommand with the query and Connection information set</returns>
        private static OracleCommand CreateOracleCommand(OracleConnection connection, string sql)
        {
            OracleCommand command = new OracleCommand(sql, connection);
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }





        /// <summary>
        /// Runs the query of an OracleCommand, and returns an unread OracleDataReader with the results of the query.
        /// </summary>
        /// <param name="command">OracleCommand containing the data for the query</param>
        /// <returns>OracleDataReader with the result of the query</returns>
        private static OracleDataReader ExecuteQuery(OracleCommand command)
        {
            try
            {
                if (command.Connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        command.Connection.Open();
                    }
                    catch (OracleException exc)
                    {
                        Debug.WriteLine("Database Connection failed!\n" + exc.Message);
                        throw exc;
                    }
                }

                OracleDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Runs the command in the given OracleCommand with ExecuteNonQuery; which is used for queries where no data is returned (in an OracleDataReader).
        /// Return value indicates if any rows are updated.
        /// </summary>
        /// <param name="command">OracleCommand containing the data for the query.</param>
        /// <returns>True when at least one row is updated.</returns>
        private static bool ExecuteNonQuery(OracleCommand command)
        {
            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
            }

            return command.ExecuteNonQuery() != 0;
        }

        private static bool CheckReader(OracleDataReader reader)
        {
            return reader.HasRows;
        }

        public static List<Bioscoop> GetAllBioscopen()
        {
            using (OracleConnection con = Connection)
            {
                List<Bioscoop> tempList = new List<Bioscoop>();
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "SELECT * FROM BIOSCOOP");
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string naam = reader["Naam"].ToString();
                        string plaats = reader["Plaats"].ToString();
                        string adres = reader["Adres"].ToString();
                        string postcode = reader["Postcode"].ToString();
                        int lift = Convert.ToInt32(reader["Lift"]);
                        int rolstoelmogelijkheid = Convert.ToInt32(reader["Rolstoelmogelijkheid"]);
                        int ringleiding = Convert.ToInt32(reader["Ringleiding"]);
                        string geluidSysteem = reader["Geluidssysteem"].ToString();
                        tempList.Add(new Bioscoop(id, naam, plaats, adres, postcode, Handler.GetBoolFromInt(lift),
                            Handler.GetBoolFromInt(rolstoelmogelijkheid), Handler.GetBoolFromInt(ringleiding),
                            geluidSysteem));
                    }
                    return tempList;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static List<Zaal> GetZalenFromBioscoopId(int biosId)
        {
            using (OracleConnection con = Connection)
            {
                List<Zaal> tempList = new List<Zaal>();
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "SELECT * FROM ZAAL WHERE bioscoop_id = :bioscoopId");
                    cmd.Parameters.Add("bioscoopId", biosId);
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        int nummer = Convert.ToInt32(reader["Nummer"]);
                        tempList.Add(new Zaal(id, nummer));
                    }
                    return tempList;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static List<Stoel> GetStoelenFromZaalId(int zaalId)
        {
            using (OracleConnection con = Connection)
            {
                List<Stoel> tempList = new List<Stoel>();
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "SELECT * FROM STOEL WHERE zaal_id = :zaalId");
                    cmd.Parameters.Add("zaalId", zaalId);
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string type = reader["Type"].ToString();
                        int rij = Convert.ToInt32(reader["Rij"]);
                        int nummer = Convert.ToInt32(reader["Nummer"]);
                        int xpos = Convert.ToInt32(reader["XPos"]);
                        int ypos = Convert.ToInt32(reader["YPos"]);
                        string status = reader["Status"].ToString();
                        tempList.Add(new Stoel(id, type, rij, nummer, xpos, ypos, status));
                    }
                    return tempList;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static List<Voorstelling> GetVoorstellingenFromZaalId(int zaalId)
        {
            using (OracleConnection con = Connection)
            {
                List<Voorstelling> tempList = new List<Voorstelling>();
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "SELECT * FROM Voorstelling WHERE zaal_id = :zaalId");
                    cmd.Parameters.Add("zaalId", zaalId);
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string formaat = reader["Formaat"].ToString();
                        int filmId = Convert.ToInt32(reader["Film_Id"]);
                        DateTime tijd = Convert.ToDateTime(reader["Tijd"]);
                        DateTime datum = Convert.ToDateTime(reader["Datum"]);
                        tempList.Add(new Voorstelling(id, new DateTime(datum.Year, datum.Month, datum.Day, tijd.Hour, tijd.Minute, tijd.Second),  formaat, filmId));
                    }
                    return tempList;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static Film GetFilmVanVoorstelling(int filmId)
        {
            using (OracleConnection con = Connection)
            {
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "SELECT * FROM Film WHERE id = :filmId");
                    cmd.Parameters.Add("filmId", filmId);
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    Film newFilm = null;
                    while (reader.Read())
                    {
                        string naam = reader["Naam"].ToString();
                        int duur = Convert.ToInt32(reader["Duur"]);
                        string beschrijving = reader["Beschrijving"].ToString();
                        string taalversie = reader["Taalversie"].ToString();
                        DateTime releaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                        newFilm = new Film(filmId, naam, duur, beschrijving, taalversie, releaseDate);
                    }
                    return newFilm;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static List<Acteur> GetActeursFromFilm(int filmId)
        {
            using (OracleConnection con = Connection)
            {
                List<Acteur> tempList = new List<Acteur>();
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "select * FROM ACTEUR INNER JOIN FILM_ACTEUR ON acteur_id = acteur.id WHERE film_id = :filmId");
                    cmd.Parameters.Add("filmId", filmId);
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    Film newFilm = null;
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string naam = reader["Naam"].ToString();
                        DateTime geboorteDatum = Convert.ToDateTime(reader["GeboorteDatum"]);
                        string biografie = reader["Biografie"].ToString();
                        string fotoUrl = reader["FotoUrl"].ToString();
                        tempList.Add(new Acteur(id, naam, geboorteDatum, biografie, fotoUrl));
                    }
                    return tempList;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static Regisseur GetRegisseurFromFilmId(int filmId)
        {
            using (OracleConnection con = Connection)
            {
                Regisseur regisseur = null;
                try
                {

                    OracleCommand cmd = CreateOracleCommand(con, "select REGISSEUR.NAAM, REGISSEUR.ID, REGISSEUR.GEBOORTEDATUM, REGISSEUR.WOONPLAATS, REGISSEUR.FOTOURL FROM Regisseur INNER JOIN FILM ON regisseur_id = REGISSEUR.ID WHERE FILM.ID = :filmId");
                    cmd.Parameters.Add("filmId", filmId);
                    con.Open();
                    OracleDataReader reader = ExecuteQuery(cmd);
                    Film newFilm = null;
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string naam = reader["Naam"].ToString();
                        DateTime geboorteDatum = Convert.ToDateTime(reader["GeboorteDatum"]);
                        string woonplaats = reader["Woonplaats"].ToString();
                        string fotoUrl = reader["FotoUrl"].ToString();
                        regisseur = new Regisseur(id, naam, geboorteDatum, woonplaats, fotoUrl);
                    }
                    return tempList;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}