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
    public partial class mainForm : Form
    {
        private List<buttonControl> buttonList = new List<buttonControl>();
        public mainForm()
        {
            InitializeComponent();
            this.FormClosing += Form1_Closing;
            this.Load += Form1_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonControl btn = addButtonControl();
            btn.addOutput();
        }

        private buttonControl addButtonControl(string name = "New Button")
        {
            buttonControl buttonGroup1 = new buttonControl();
            buttonGroup1.btnName = name;
            this.tableLayoutPanel1.Controls.Add(buttonGroup1);
            buttonList.Add(buttonGroup1);

            return buttonGroup1;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + @"\settings.xml")) {
                //FormSerialisor.Deserialise(this, Application.StartupPath + @"\settings.xml");
                XDocument doc = XDocument.Load(Application.StartupPath + @"\settings.xml");
                foreach (var elem in doc.Descendants("Root").Elements())
                {
                    if (elem.Name=="Group")
                    {
                        buttonControl btn = addButtonControl(elem.Element("btnName").Value);
                        foreach (var output in elem.Element("outputs").Elements())
                        {
                            btn.addOutput(output.Element("fileLocation").Value,output.Element("fileContents").Value);
                        }
                    } else if (elem.Name== "alwaysOnTop")
                    {
                        if (elem.Value=="true")
                        {
                            this.TopMost = true;
                            checkBox1.Checked = true;
                        } else
                        {
                            this.TopMost = false;
                            checkBox1.Checked = false;
                        }
                    } else if (elem.Name=="size")
                    {
                        this.Size = new Size(Int32.Parse(elem.Element("width").Value),Int32.Parse(elem.Element("height").Value));
                    }
                }
            }
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //FormSerialisor.Serialise(this, Application.StartupPath + @"\settings.xml");
            XElement srcTree = new XElement(new XElement("Root"));
            foreach (buttonControl btnGroup in buttonList)
            {
                XElement outputs = new XElement("outputs");
                foreach (outputControl output in btnGroup.outputList)
                {
                    outputs.Add(new XElement("output",
                        new XElement("fileLocation", output.fileLocation),
                        new XElement("fileContents", output.fileContents)
                    ));
                }

                srcTree.Add(
                    new XElement("Group",
                        new XElement("btnName", btnGroup.btnName),
                        outputs
                    )
                );
            }
            srcTree.Add(new XElement("alwaysOnTop", this.TopMost));
            srcTree.Add(new XElement("size",
                new XElement("width", this.Size.Width),
                new XElement("height", this.Size.Height)
            ));
            srcTree.Save(Application.StartupPath + @"\settings.xml");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.TopMost = true;
            } else
            {
                this.TopMost = false;
            }
        }

        private void checkBoxShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowDetails.Checked)
            {
                foreach(Control c in this.tableLayoutPanel1.Controls)
                {
                    showDetails(c);
                }
            } else
            {
                foreach(Control c in this.tableLayoutPanel1.Controls)
                {
                    hideDetails(c);
                }
            }
        }

        private void showDetails(Control c)
        {
            if (c.Name != "button1")
            {
                c.Show();
            }
            foreach (Control t in c.Controls)
                showDetails(t);
        }

        private void hideDetails(Control c)
        {
            if (c.Name != "button1")
            {
                c.Hide();
            }  else
            {
                c.Parent.Show();
                c.Parent.Parent.Show();
            }
            foreach (Control t in c.Controls)
                hideDetails(t);
        }
    }
}
