﻿using System;
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
    public partial class Result : Form
    {
        public Result(string text, bool enable_copy)
        {
            InitializeComponent();
            textBox1.Text = text;
            button1.Enabled = enable_copy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

    }
}
