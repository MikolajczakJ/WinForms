using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cw1.Properties
{
    internal class MyForm:Form
    {
        private Button button1;
        public MyForm()
        {
            button1 = new Button();
            button1.Width = 100;
            button1.Height = 100;
            button1.Text = ": )";
            button1.Left = 20;
            button1.Top = 20;
            this.Controls.Add(button1);
        }

    }
}
