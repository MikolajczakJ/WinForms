using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
namespace Firma
{
    public partial class Form1 : Form
    {
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jakub\OneDrive\Pulpit\Studia\SEM VI\Forms\WinForms\Firma\Firma\Firma.mdf;Connect Timeout=30";
        static DataContext bazaDanychFirma = new DataContext(connectionString);
        static Table<FakturaSprzedazy> listaFaktur = bazaDanychFirma.GetTable<FakturaSprzedazy>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "Lista faktur \n";
            var lf = from FakturaSprzedazy in listaFaktur
                     select FakturaSprzedazy;
            foreach (FakturaSprzedazy faktura in lf)
            {
                s+= faktura.Numer + " " + faktura.Netto + "\n";
            }
            MessageBox.Show(s);
        }
    }
}
