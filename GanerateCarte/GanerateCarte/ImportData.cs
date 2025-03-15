using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;


namespace GanerateCarte
{
    public partial class ImportData : Form
    {

        private string excelFilePath;
        public ImportData()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Définir le contexte de licence
        }

        private void button1_Click(object sender, EventArgs e)
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


    }
}
