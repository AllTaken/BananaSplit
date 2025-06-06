﻿using System;
using System.Windows.Forms;

namespace BananaSplit
{
    public partial class LogForm : Form
    {
        public MainForm MainForm { get; set; }
        public LogForm()
        {
            InitializeComponent();

            FormClosing += LogForm_FormClosing;
            ClearButton.Click += ClearButton_Click;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            LogRichTextBox.Text = "";
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void Log(string text)
        {
            LogRichTextBox.AppendText(text + Environment.NewLine);
        }

        public void ShowLog()
        {
            MainForm?.Invoke(new MethodInvoker(
               delegate ()
               {
                   if (!Visible)
                   {
                       Show();
                   }
               }));
        }
    }
}
