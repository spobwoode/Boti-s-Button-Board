using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotisButtonBoard
{
    public partial class outputControl : UserControl
    {
        public delegate void closeEventHandler();
        public event closeEventHandler tabClosed;
        public string fileLocation
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }
        public string fileContents
        {
            get
            {
                return textBox3.Text;
            }
            set
            {
                textBox3.Text = value;
            }
        }

        public outputControl()
        {
            InitializeComponent();
        }

        public void writeToFile()
        {
            if (textBox2.Text!="" && textBox3.Text!="")
            {
                System.IO.File.WriteAllText(textBox2.Text, textBox3.Text);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox2.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabClosed != null)
                tabClosed();
            this.Dispose();
        }

    }
}
