using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cripto_Lab1
{
    public partial class Form1 : Form
    {
        #region arreglos
        List<string> hexa_characters = new List<string>()
        {
            "0", "1", "2", "3",
            "4", "5", "6", "7",
            "8", "9", "A", "B",
            "C", "D", "E", "F"
        };
        List<string> binary_values = new List<string>()
        {
            "0000", "0001", "0010", "0011",
            "0100", "0101", "0110", "0111",
            "1000", "1001", "1010", "1011",
            "1100", "1101", "1110", "1111"
        };
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void rdCifrar_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = rdCifrar.Checked ? "Cifrar Texto" : "Descifrar Texto";
            lblText.Text = rdCifrar.Checked ? "Texto Plano" : "Texto Cifrado";
        }

        private string ascii_to_hex(string input)
        {
            string hex = String.Empty;
            foreach (char c in input)
                hex += ((int)c).ToString("X");
            return hex;
        }

        private string ExpandString(string str, int length)
        {
            if (length <= str.Length) return str.Substring(0, length);
            while (str.Length * 2 <= length)
            {
                str += str;
            }
            if (str.Length < length)
            {
                str += str.Substring(0, length - str.Length);
            }
            return str;
        }

        private string Cifrar(string texto, string key)
        {
            string res = String.Empty;

            string bin_TP = HEX_to_BIN(texto);
            string bin_KEY = HEX_to_BIN(ExpandString(key,texto.Length));
            string xor_result_bin = XOR(bin_TP, bin_KEY);
            string xor_result_hex = BIN_to_HEX(xor_result_bin);
            new Result(xor_result_hex, true).ShowDialog();

            return res;
        }

        private string Descifrar(string texto, string key)
        {
            string ascii = String.Empty;

            string bin_TP = HEX_to_BIN(texto);
            string bin_KEY = HEX_to_BIN(key.Length >= texto.Length ? key.Substring(0,texto.Length) : ExpandString(key, texto.Length));
            string xor_result_bin = XOR(bin_TP, bin_KEY);
            string xor_result_hex = BIN_to_HEX(xor_result_bin);
            string ascii_result = HEX_to_ASCII(xor_result_hex);
            new Result(ascii_result, false).ShowDialog();

            return ascii;
        }

        private string HEX_to_ASCII(string hex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hex.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }

        private string XOR(string bin1, string bin2)
        {
            string res = String.Empty;
            for (int i = 0; i < bin1.Length; i++)
            {
                res += bin1.Substring(i, 1) != bin2.Substring(i, 1) ? "1" : "0";    
            }
            return res;
        }

        private bool IsTextHEX(string text)
        {
            Regex r = new Regex(@"^[0-9A-F]+$");
            return r.Match(text).Success && text.Length % 2 == 0;
        }

        private string HEX_to_BIN(string hex)
        {
            string res = String.Empty;
            foreach (char c in hex)
            {
                res += binary_values.ElementAt(hexa_characters.IndexOf(c.ToString()));
            }
            return res;
        }

        private string BIN_to_HEX(string bin)
        {
            string res = String.Empty;
            List<string> bins = bin.Select((x, i) => i).Where(i => i % 4 == 0).Select(i => bin.Substring(i, bin.Length - i >= 4 ? 4 : bin.Length - i)).ToList();
            foreach (string b in bins)
            {
                res += hexa_characters.ElementAt(binary_values.IndexOf(b));
            }
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Todos los campos llenados
            if (!String.IsNullOrEmpty(txtInput.Text) && !String.IsNullOrEmpty(txtKey.Text))
            {
                //Entrada es HEX
                if (IsTextHEX(txtInput.Text))
                {
                    //LLave es HEX
                    if (IsTextHEX(txtKey.Text))
                    {
                        //Acción
                        switch (rdCifrar.Checked)
                        {
                            //Cifrado Vigenère XOR
                            case true:
                                Cifrar(txtInput.Text, txtKey.Text);
                                break;
                            //Descifrado Vigenère XOR
                            case false:
                                Descifrar(txtInput.Text, txtKey.Text);
                                break;
                        }
                        txtInput.Clear();
                        txtKey.Clear();
                        txtInput.Focus();
                    }
                    else
                        MessageBox.Show("Llave HEX Inválida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Texto HEX Inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Debe llenar todos los campos primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cifrarTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rdCifrar.Checked = true;
        }

        private void descifrarTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rdDescifrar.Checked = true;
        }

        private void convertirASCIIAHEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ASCII_Converter().ShowDialog();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
