﻿using System;
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
        static Table<Pracownik> listaPracownikow = bazaDanychFirma.GetTable<Pracownik>(); 
        static Table<Kontrahent> listaKontrahentow = bazaDanychFirma.GetTable<Kontrahent>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void listaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lp = from Pracownik in listaPracownikow
                     orderby Pracownik.Nazwisko
                     select new
                     {
                         Pracownik.Id,
                         Pracownik.Nazwisko,
                         Pracownik.Imie,
                         Pracownik.Telefon,
                         Pracownik.Email
                     };
            dataGridView1.DataSource = lp;
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int noweId = listaPracownikow.Max(pracownik => pracownik.Id)+1;
            Pracownik nowyPracownik = new Pracownik
            {
                Id = noweId,
                Imie = textBoxImie.Text,
                Nazwisko = textBoxNazwisko.Text,
                Email = textBoxEmail.Text,
                Telefon = textBoxTelefon.Text
            };
            listaPracownikow.InsertOnSubmit(nowyPracownik);
            bazaDanychFirma.SubmitChanges();
            listaToolStripMenuItem_Click(this,null);
        }

        private void usunięcieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int wiersz = dataGridView1.CurrentRow.Index;
            int idPracownika = int.Parse(dataGridView1.Rows[wiersz].Cells[0].Value.ToString());

            IEnumerable<Pracownik> doSkasowania =
                from Pracownik in listaPracownikow
                where Pracownik.Id == idPracownika
                select Pracownik;

            listaPracownikow.DeleteAllOnSubmit(doSkasowania);
            bazaDanychFirma.SubmitChanges();
            listaToolStripMenuItem_Click(this, null);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int wiersz = dataGridView1.CurrentRow.Index;
            textBoxNazwisko.Text = dataGridView1.Rows[wiersz].Cells[1].Value.ToString();
            textBoxImie.Text = dataGridView1.Rows[wiersz].Cells[2].Value.ToString();
            textBoxTelefon.Text = dataGridView1.Rows[wiersz].Cells[3].Value.ToString();
            textBoxEmail.Text = dataGridView1.Rows[wiersz].Cells[4].Value.ToString();
            int idPracownik =
                Convert.ToInt32(dataGridView1.Rows[wiersz].Cells[0].Value.ToString());
            var ls = from FakturySprzedazy in listaFaktur
                     where (FakturySprzedazy.Id == idPracownik)
                     select new
                     {
                         FakturySprzedazy.Id,
                         FakturySprzedazy.Numer,
                         FakturySprzedazy.Netto,
                         FakturySprzedazy.Vat,
                         FakturySprzedazy.Data,
                         FakturySprzedazy.Zaplacono
                     };
            dataGridView2.DataSource = ls;
        }

        private void edycjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int wiersz = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            int idPracownik =
                Convert.ToInt32(dataGridView1.Rows[wiersz].Cells[0].Value.ToString());
            foreach (Pracownik pracownik in listaPracownikow)
            {
                if (pracownik.Id == idPracownik)
                {
                    pracownik.Imie = textBoxImie.Text;
                    pracownik.Nazwisko = textBoxNazwisko.Text;
                    pracownik.Email = textBoxEmail.Text;
                    pracownik.Telefon = textBoxTelefon.Text;
                }
            }
            bazaDanychFirma.SubmitChanges();
            listaToolStripMenuItem_Click(this, null);
        }
    }
}
