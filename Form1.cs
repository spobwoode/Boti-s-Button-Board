using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BotisButtonBoard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_Closing;
            this.Load += Form1_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl1 buttonGroup1 = new UserControl1();
            this.flowLayoutPanel1.Controls.Add(buttonGroup1);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + @"\settings.xml")) {
                //FormSerialisor.Deserialise(this, Application.StartupPath + @"\settings.xml");
                XDocument doc = XDocument.Load(Application.StartupPath + @"\settings.xml");
                foreach (var btnGroup in doc.Descendants("Root").Elements())
                {
                    UserControl1 btnGroupControl = new UserControl1();
                    btnGroupControl.btnName = btnGroup.Element("btnName").Value;
                    btnGroupControl.fileLocation = btnGroup.Element("fileLocation").Value;
                    btnGroupControl.fileContents = btnGroup.Element("fileContents").Value;
                    this.flowLayoutPanel1.Controls.Add(btnGroupControl);
                }
            }
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //FormSerialisor.Serialise(this, Application.StartupPath + @"\settings.xml");
            XElement srcTree = new XElement(new XElement("Root"));
            foreach (UserControl1 btnGroup in this.flowLayoutPanel1.Controls)
            {
                srcTree.Add(
                    new XElement("Group",
                        new XElement("btnName", btnGroup.btnName),
                        new XElement("fileLocation", btnGroup.fileLocation),
                        new XElement("fileContents", btnGroup.fileContents)
                    )
                );
            }
            srcTree.Save(Application.StartupPath + @"\settings.xml");
        }
    }
}
