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
    public partial class buttonControl : UserControl
    {
        public string btnName
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public List<outputControl> outputList = new List<outputControl>();

        public buttonControl()
        {
            InitializeComponent();
        }

        public void onTabRemoved(TabPage closedTab, outputControl newControl)
        {
            this.tabControl1.TabPages.Remove(closedTab);
            outputList.Remove(newControl);
        }

        public void addOutput(string fileLocation = "", string fileContents = "")
        {
            outputControl newOutControl = new outputControl();
            TabPage newTab = new TabPage("Output "+(outputList.Count+1));
            newOutControl.tabClosed += () => onTabRemoved(newTab,newOutControl);
            newTab.Controls.Add(newOutControl);
            this.tabControl1.TabPages.Add(newTab);
            //this.tabControl1.Controls.Add(newOutControl);
            newOutControl.fileLocation = fileLocation;
            newOutControl.fileContents = fileContents;
            outputList.Add(newOutControl);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (outputControl output in outputList)
            {
                output.writeToFile();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.addOutput();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            button1.Text = textBox1.Text;
        }

        private void buttonControl_Load(object sender, EventArgs e)
        {

        }
    }
}
