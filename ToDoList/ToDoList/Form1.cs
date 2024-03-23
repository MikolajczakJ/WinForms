using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    //Jestem na 6 stronie pdf, anchor komponentów zrobiony
    public partial class Form1 : Form
    {
        private BindingList<ToDoEntry> entries = new BindingList<ToDoEntry>();
        public Form1()
        {
            InitializeComponent();
            entriesSource.DataSource = entries;
        }


    }
}
