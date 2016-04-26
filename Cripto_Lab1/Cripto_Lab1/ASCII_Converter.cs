using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cripto_Lab1
{
    public partial class ASCII_Converter : Form
    {
        public ASCII_Converter()
        {
            InitializeComponent();
        }

        private string ascii_to_hex(string input)
        {
            string hex = String.Empty;
            foreach (char c in input)
                hex += ((int)c).ToString("X");
            return hex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtASCII.Text))
                txtHEX.Text = ascii_to_hex(txtASCII.Text);
            else
                MessageBox.Show("Especifique el texto a convertir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtHEX_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = !String.IsNullOrEmpty(txtHEX.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHEX.Text);
        }
    }
}
