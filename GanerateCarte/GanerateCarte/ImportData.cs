﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace GanerateCarte
{
    public partial class ImportData : Form
    {

        private string excelFilePath;
        PictureEdit pic = new PictureEdit();
        clsImabar codage = new clsImabar();
        public ImportData()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Définir le contexte de licence
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertDataFromDataGridView();
            txtMax.Text = GetMaxId("tpersonne").ToString();
        }

        private void LoadSheetsIntoComboBox(string filePath)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                comboBoxSheets.Items.Clear(); // Effacer les éléments précédents

                // Ajouter les noms des feuilles au ComboBox
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    comboBoxSheets.Items.Add(worksheet.Name);
                }

                comboBoxSheets.Enabled = true; // Activer le ComboBox
                btnImport.Enabled = true; // Activer le bouton d'importation
            }
        }

        private void ImportSelectedSheet(string sheetName)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(excelFilePath)))
            {
                var selectedSheet = package.Workbook.Worksheets[sheetName];
                DataTable dt = new DataTable();

                // Ajouter les colonnes au DataTable
                foreach (var firstRowCell in selectedSheet.Cells[1, 1, 1, selectedSheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                // Ajouter les lignes au DataTable
                for (var rowNumber = 2; rowNumber <= selectedSheet.Dimension.End.Row; rowNumber++)
                {
                    var row = selectedSheet.Cells[rowNumber, 1, rowNumber, selectedSheet.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    dt.Rows.Add(newRow);
                }

                // Lier le DataTable au DataGridView
                dataGridView1.DataSource = dt;
            }
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

        public int GetMaxId(string tableName, string nomChamp, string champCondition, string champValeur)
        {
            int maxId = 0;
            try
            {
                // Créez la requête SQL pour obtenir l'ID maximal
                string query = "SELECT ISNULL(MAX(" + nomChamp + "), 0) FROM " + tableName + " WHERE " + champCondition + " = @champValeur";

                using (SqlConnection connection = new SqlConnection(clsConnexion.chemin))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@champValeur", champValeur); // Utilisation des paramètres pour éviter les injections SQL

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Lire la première ligne
                            {
                                // Vérifiez si le résultat est null et affectez la valeur maximale
                                if (!reader.IsDBNull(0)) // Vérifie si la première colonne n'est pas null
                                {
                                    maxId = reader.GetInt32(0); // Récupérer la valeur entière
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Gérez les exceptions ici (journalisation, relance, etc.)
                        Console.WriteLine("Une erreur est survenue : " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici si nécessaire
                Console.WriteLine("Une erreur est survenue : " + ex.Message);
            }

            return maxId;
        }


        private void InsertDataFromDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnexion.chemin))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Vérifiez si la ligne n'est pas nouvelle
                        if (!row.IsNewRow)
                        {
                            // Vérifiez que la cellule 11 a une valeur
                            if (row.Cells[11].Value != null)
                            {
                                int idMax = GetMaxId("tpersonne", "code", "air_sante", row.Cells[11].Value.ToString());
                                int nbrLimte = idMax + 1;
                                codage.QRCode(pic, row.Cells[11].Value.ToString() + " : " + nbrLimte.ToString());

                                // Créez une commande SQL pour insérer les données
                                string query = "INSERT INTO tpersonne (code,noms,sexe,age,garcon,fille,femme_enceinte,provenace,handicap,observation,zone_sante,air_sante,photo) VALUES (@code,@noms,@sexe,@age,@garcon,@fille,@femme_enceinte,@provenace,@handicap,@observation,@zone_sante,@air_sante,@photo)";

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@code", nbrLimte);
                                    command.Parameters.AddWithValue("@noms", row.Cells[1].Value?.ToString() ?? string.Empty);
                                    command.Parameters.AddWithValue("@sexe", row.Cells[2].Value?.ToString() ?? string.Empty);
                                    command.Parameters.AddWithValue("@age", Convert.ToInt32(row.Cells[3].Value ?? 0));
                                    command.Parameters.AddWithValue("@garcon", Convert.ToInt32(row.Cells[4].Value ?? 0));
                                    command.Parameters.AddWithValue("@fille", Convert.ToInt32(row.Cells[5].Value ?? 0));
                                    command.Parameters.AddWithValue("@femme_enceinte", Convert.ToInt32(row.Cells[6].Value ?? 0));
                                    command.Parameters.AddWithValue("@provenace", row.Cells[7].Value?.ToString() ?? string.Empty);
                                    command.Parameters.AddWithValue("@handicap", Convert.ToInt32(row.Cells[8].Value ?? 0));
                                    command.Parameters.AddWithValue("@observation", row.Cells[9].Value?.ToString() ?? string.Empty);
                                    command.Parameters.AddWithValue("@zone_sante", row.Cells[10].Value?.ToString() ?? string.Empty);
                                    command.Parameters.AddWithValue("@air_sante", row.Cells[11].Value.ToString());
                                    command.Parameters.AddWithValue("@photo", pic?.Image != null ? GetByteImage(pic.Image) : (object)DBNull.Value);

                                    // Exécutez la commande
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Données insérées avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelFilePath = openFileDialog.FileName; // Stocker le chemin du fichier
                    LoadSheetsIntoComboBox(excelFilePath);
                }
            }
        }

        private void comboBoxSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSheets.SelectedItem != null)
            {
                ImportSelectedSheet(comboBoxSheets.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une feuille à importer.");
            }
        }

        public int GetMaxId(string tableName)
        {
            int maxId = 0;
            // Créez la requête SQL pour obtenir l'ID maximal
            string query = $"SELECT COUNT(id) FROM {tableName}";
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

        private void ImportData_Load(object sender, EventArgs e)
        {
            txtMax.Text = GetMaxId("tpersonne").ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtNombre1.Text == "" || txtNombre2.Text == "")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else
            {
                try
                {
                    rpt_CarteBenefiaire rpt = new rpt_CarteBenefiaire();
                    rpt.DataSource = clsRapport.GetInstance().data_beneficaire(int.Parse(txtNombre1.Text), int.Parse(txtNombre2.Text));
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtNombre1.Text == "" || txtNombre2.Text == "")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else
            {
                try
                {
                    rpt_CarteBeneficiaire2 rpt = new rpt_CarteBeneficiaire2();
                    rpt.DataSource = clsRapport.GetInstance().data_beneficaire_enceinte(int.Parse(txtNombre1.Text), int.Parse(txtNombre2.Text));
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtNombre1.Text == "" || txtNombre2.Text == "" || txtAirSante.Text=="")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else
            {
                try
                {
                    rpt_CarteBenefiaire rpt = new rpt_CarteBenefiaire();
                    rpt.DataSource = clsRapport.GetInstance().data_air(int.Parse(txtNombre1.Text), int.Parse(txtNombre2.Text),txtAirSante.Text);
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (txtNombre1.Text == "" || txtNombre2.Text == "" || txtAirSante.Text == "")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else
            {
                try
                {
                    rpt_CarteBeneficiaire2 rpt = new rpt_CarteBeneficiaire2();
                    rpt.DataSource = clsRapport.GetInstance().data_air_enceinte(int.Parse(txtNombre1.Text), int.Parse(txtNombre2.Text), txtAirSante.Text);
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
