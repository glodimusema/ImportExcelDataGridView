using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace GanerateCarte
{
    public partial class Form1 : Form
    {    
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter dt = null;
        SqlDataReader dr = null;

        PictureEdit pic = new PictureEdit();
        clsImabar codage = new clsImabar();

        char sexe;
        string colère;
        int A;

        public Form1()
        {
            InitializeComponent();
        }

        public static byte[] GetByteImage(Image picPhoto)
        {
            if (picPhoto == null)
            {
                throw new ArgumentNullException(nameof(picPhoto), "The image cannot be null.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                // Save the image to the memory stream in a specific format (e.g., PNG, JPEG)
                picPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public int GetMaxId(string tableName)
        {
            int maxId = 0;
            // Créez la requête SQL pour obtenir l'ID maximal
            string query = $"SELECT MAX(codeChiffre) FROM {tableName}";
            using (SqlConnection connection = new SqlConnection(clsConnexion.chemin))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    // Vérifiez si le résultat est null et affectez la valeur maximale
                    if (result != DBNull.Value)
                    {
                        maxId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Gérez les exceptions ici (journalisation, relance, etc.)
                    Console.WriteLine("Une erreur est survenue : " + ex.Message);
                }
            }

            return maxId;
        }


        void saveData()
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idMax = GetMaxId("tGenerate");
                int nbrLimte = idMax + int.Parse(txtCode.Text);
                int age = 18;



                for (int i = (idMax + 1); i < nbrLimte; i++)
                {
                    try
                    {
                        codage.QRCode(pic, "GM" + i.ToString());
                        con = new SqlConnection(clsConnexion.chemin);
                        con.Open();
                        cmd = new SqlCommand("INSERT INTO tGenerate (codeLettre,codeChiffre,photo) VALUES (@codeLettre,@codeChiffre,@photo)", con);
                        cmd.Parameters.AddWithValue("@codeLettre", "GM");
                        cmd.Parameters.AddWithValue("@codeChiffre", i);
                        cmd.Parameters.AddWithValue("@photo", GetByteImage(pic.Image));
                        cmd.ExecuteNonQuery();
                        con.Close();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Enregistrement reussi");
            }
            catch (Exception ex)
            { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          txtMax.Text= GetMaxId("tGenerate").ToString();
        }

        private void txtMax_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtNombre1.Text == "" || txtNombre2.Text == "")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else
            {
                try
                {
                    rptAutocollant rpt = new rptAutocollant();
                    rpt.DataSource = clsRapport.GetInstance().data_number(int.Parse(txtNombre1.Text), int.Parse(txtNombre2.Text));
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }
    }
}
