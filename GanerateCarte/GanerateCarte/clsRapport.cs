using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanerateCarte
{
    class clsRapport
    {
        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        



        private static clsRapport glos;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter dt = null;
        //SqlDataReader dr = null;


        public void innitialiseConnect()
        {
            try
            {
                con = new SqlConnection(clsConnexion.chemin);
            }
            catch (Exception)
            {
                throw new Exception("l'un de vos fichiers de configuration est incorrect");
            }

        }
        public static clsRapport GetInstance()
        {
            if (glos == null)
                glos = new clsRapport();
            return glos;
        }

        private static void setParameter(SqlCommand cmd, string name, DbType type, int length, object paramValue)
        {

            IDbDataParameter a = cmd.CreateParameter();
            a.ParameterName = name;
            a.Size = length;
            a.DbType = type;

            if (paramValue == null)
            {
                if (!a.IsNullable)
                {
                    a.DbType = DbType.String;
                }
                a.Value = DBNull.Value;
            }
            else
                a.Value = paramValue;
            cmd.Parameters.Add(a);
        }



        public DataSet data_date(DateTime date1, DateTime date2)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec sp_print_livre_caisse @date1,@date2", con);
                setParameter(cmd, "@date1", DbType.Date, 20, date1);
                setParameter(cmd, "@date2", DbType.Date, 20, date2);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "sp_print_livre_caisse");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }


        public DataSet data_number(int nbr1, int nbr2)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from tGenerate where codeChiffre between @nbr1 and @nbr2", con);
                setParameter(cmd, "@nbr1", DbType.Int64, 20, nbr1);
                setParameter(cmd, "@nbr2", DbType.Int64, 20, nbr2);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "tGenerate");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }


        public DataSet data_beneficaire(int nbr1, int nbr2)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from vPersonne where id between @nbr1 and @nbr2", con);
                setParameter(cmd, "@nbr1", DbType.Int64, 20, nbr1);
                setParameter(cmd, "@nbr2", DbType.Int64, 20, nbr2);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "vPersonne");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }

        public DataSet data_beneficaire_enceinte(int nbr1, int nbr2)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from vPersonne where (id between @nbr1 and @nbr2) and (femme_enceinte > 0)", con);
                setParameter(cmd, "@nbr1", DbType.Int64, 20, nbr1);
                setParameter(cmd, "@nbr2", DbType.Int64, 20, nbr2);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "vPersonne");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }


        public DataSet data_air(int nbr1, int nbr2,string air_sante)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from vPersonne where (code between @nbr1 and @nbr2) and air_sante = @air_sante", con);
                setParameter(cmd, "@nbr1", DbType.Int64, 20, nbr1);
                setParameter(cmd, "@nbr2", DbType.Int64, 20, nbr2);
                setParameter(cmd, "@air_sante", DbType.String, 100, air_sante);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "vPersonne");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }

        public DataSet data_air_enceinte(int nbr1, int nbr2, string air_sante)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from vPersonne where (code between @nbr1 and @nbr2) and (air_sante = @air_sante) and (femme_enceinte > 0)", con);
                setParameter(cmd, "@nbr1", DbType.Int64, 20, nbr1);
                setParameter(cmd, "@nbr2", DbType.Int64, 20, nbr2);
                setParameter(cmd, "@air_sante", DbType.String, 100, air_sante);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "vPersonne");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }













    }
}
