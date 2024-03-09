using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notatnik
{
    public partial class Notatnik : Form
    {
        public Notatnik()
        {
            InitializeComponent();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    string wiersz;
                    while ((wiersz = sr.ReadLine()) != null)
                    {
                        list.Add(wiersz);
                    }
                    textBox1.Lines = list.ToArray();

                    //textBox1.Text = sr.ReadToEnd();

                }
            }
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tekst = textBox1.Lines;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (var item in tekst)
                    {
                        sw.WriteLine(item);
                    }
                    sw.Close();
                }
            }
        }

        private void tłoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox1.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
            }
        }

        private void czcionkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Color = textBox1.ForeColor;
            fontDialog1.Font = textBox1.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
            }
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
