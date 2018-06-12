using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace BonanzaImporterTool
{
    public partial class FrmPricingRules : Form
    {
        //H: 20. W: 100. Space: 3
        const int _width = 100;
        const int _height = 20;
        const int _space = 3;
        Point CRCol1;
        Point CRCol2;
        Point Markup1;
        int IDCount = 0;
        public FrmPricingRules()
        {
            InitializeComponent();
            CRCol1 = nudCRCol1.Location;
            CRCol2 = nudCRCol2.Location;
            Markup1 = nudMarkup1.Location;
            nudCRCol1.Name += "0";
            nudCRCol2.Name += "0";
            nudMarkup1.Name += "0";
        }

        private void AddNUD(ref int _IDCount)
        {
            NumericUpDown _curNUDCol1 = (NumericUpDown)Controls.Find("nudCRCol1" + _IDCount, true).First();
            NumericUpDown _curNUDCol2 = (NumericUpDown)Controls.Find("nudCRCol2" + _IDCount, true).First();
            NumericUpDown _curNUDMarkup1 = (NumericUpDown)Controls.Find("nudMarkup1" + _IDCount, true).First();
            if (_curNUDCol1.Value >= _curNUDCol2.Value)
            {
                MessageBox.Show("Cost Range: Cột trái < cột phải!");
                return;
            }
            _IDCount++;
            GroupAddsControl(grpCostRange, btnMoreTxt, CRCol1.X, "nudCRCol1" + _IDCount, _curNUDCol2.Value + 0.01m);
            NumericUpDown CRCol1Ref = GroupAddsControl(grpCostRange, btnMoreTxt, CRCol2.X, "nudCRCol2" + _IDCount, _curNUDCol2.Value + 0.02m);
            GroupAddsControl(grpMarkup, btnMoreTxt, Markup1.X, "nudMarkup1" + _IDCount, _curNUDMarkup1.Value);

            Point btnLocation = btnMoreTxt.Location;
            btnLocation.Y += btnMoreTxt.Size.Width + _space;
            btnMoreTxt.Location = btnLocation;

            Point buttonLessLocation = btnLessTxt.Location;
            buttonLessLocation.Y = btnMoreTxt.Location.Y;
            btnLessTxt.Location = buttonLessLocation;

            CRCol1Ref.Select();
            CRCol1Ref.Select(0, CRCol1Ref.Value.ToString().Length);
        }

        private void btnMoreTxt_Click(object sender, EventArgs e)
        {
            if(btnMoreTxt.Location.Y < grpCostRange.Location.Y + btnMoreTxt.Size.Height)
            {
                AddNUD(ref IDCount);
            }
            
        }

        private NumericUpDown CreateNUD(Point location, string controlID, decimal value)
        {
            NumericUpDown nudNew = new NumericUpDown();
            Size nudNewSize = new Size(_width, _height);
            nudNew.Size = nudNewSize;
            Point nudNewLocation = location;
            nudNew.Location = nudNewLocation;
            nudNew.DecimalPlaces = 2;
            nudNew.Increment = 0.01m;
            nudNew.Maximum = 9999;
            nudNew.Minimum = 0;
            nudNew.Name = controlID;
            nudNew.Value = value;
            return nudNew;
        }

        private NumericUpDown GroupAddsControl(GroupBox grpBox, Button locationButton, int xOffset, string controlID, decimal value)
        {
            NumericUpDown nudNew = new NumericUpDown();
            Size nudNewSize = new Size(_width, _height);
            nudNew.Size = nudNewSize;
            Point nudNewLocation = locationButton.Location;
            //nudNewLocation.Y += _space;
            nudNewLocation.X = xOffset;
            nudNew.Location = nudNewLocation;
            nudNew.DecimalPlaces = 2;
            nudNew.Increment = 0.01m;
            nudNew.Maximum = 9999;
            nudNew.Minimum = 0;
            nudNew.Name = controlID;
            nudNew.Value = value;

            grpBox.Controls.Add(nudNew);

            NumericUpDown nudRef = nudNew;
            if (controlID.Contains("nudCRCol2"))
            {
                return nudNew;
            }
            return null;


        }

        private void btnLessTxt_Click(object sender, EventArgs e)
        {
            if(grpMarkup.Controls.Count > 1)
            {
                int lineCount = grpMarkup.Controls.Count;
                Point lastPosition = grpMarkup.Controls[lineCount - 1].Location;

                Point buttonLessLocation = btnLessTxt.Location;
                buttonLessLocation.Y = lastPosition.Y;
                btnLessTxt.Location = buttonLessLocation;

                Point btnLocation = btnMoreTxt.Location;
                btnLocation.Y = btnLessTxt.Location.Y;
                btnMoreTxt.Location = btnLocation;

                grpMarkup.Controls.RemoveAt(lineCount - 1);
                grpCostRange.Controls.RemoveByKey("nudCRCol1" + (lineCount - 1));
                grpCostRange.Controls.RemoveByKey("nudCRCol2" + (lineCount - 1));
                IDCount--;
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("rules"); // This overload assumes the document already knows about the rdf schema as it is in the Schemas set
                doc.AppendChild(rootNode);
                for (int i = 0; i <= IDCount; i++)
                {
                    NumericUpDown _curNUDCol1 = (NumericUpDown)Controls.Find("nudCRCol1" + i, true).First();
                    NumericUpDown _curNUDCol2 = (NumericUpDown)Controls.Find("nudCRCol2" + i, true).First();
                    NumericUpDown _curNUDMarkup1 = (NumericUpDown)Controls.Find("nudMarkup1" + i, true).First();

                    //Check 
                    if (_curNUDCol1.Value >= _curNUDCol2.Value)
                    {
                        MessageBox.Show("Can not save. Error code = 200");
                        return;
                    }
                    try
                    {
                        NumericUpDown _curNUDCol1Next = (NumericUpDown)Controls.Find("nudCRCol1" + (i + 1), true).First();
                        if (_curNUDCol2.Value >= _curNUDCol1Next.Value)
                        {
                            MessageBox.Show("Can not save. Error code = 201");
                            return;
                        }
                    }
                    catch
                    {
                        continue;
                    }


                    XmlNode rule = doc.CreateElement("rule");
                    XmlNode minCR = doc.CreateElement("minCR");
                    XmlNode maxCR = doc.CreateElement("maxCR");
                    XmlNode markup = doc.CreateElement("markup");

                    minCR.InnerText = _curNUDCol1.Value.ToString();
                    maxCR.InnerText = _curNUDCol2.Value.ToString();
                    markup.InnerText = _curNUDMarkup1.Value.ToString();

                    rule.AppendChild(minCR);
                    rule.AppendChild(maxCR);
                    rule.AppendChild(markup);
                    doc.DocumentElement.AppendChild(rule);
                }
                doc.Save(@"data\Pricing_Rules.xml");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unknown error: " + ex.Message);
            }
        }

        private void FrmPricingRules_Load(object sender, EventArgs e)
        {
            XmlDocument xmlRead = new XmlDocument();
            xmlRead.Load(@"data\Pricing_Rules.xml");
            XmlNodeList ruleList = xmlRead.DocumentElement.ChildNodes;

            int lastYNUD = _height + _space;

            IDCount = ruleList.Count - 1;

            for(int i = 1; i < ruleList.Count; i++)
            {
                NumericUpDown temp_nudCRCol1 = (NumericUpDown)grpCostRange.Controls.Find("nudCRCol1" + (i - 1).ToString(), true).First();
                NumericUpDown temp_nudCRCol2 = (NumericUpDown)grpCostRange.Controls.Find("nudCRCol2" + (i - 1).ToString(), true).First();
                NumericUpDown temp_nudMarkup1 = (NumericUpDown)grpMarkup.Controls.Find("nudMarkup1" + (i - 1).ToString(), true).First();
                if(i == 1)
                {
                    NumericUpDown temp_nudCRCol10 = (NumericUpDown)grpCostRange.Controls.Find("nudCRCol1" + (i - 1).ToString(), true).First();
                    NumericUpDown temp_nudCRCol20 = (NumericUpDown)grpCostRange.Controls.Find("nudCRCol2" + (i - 1).ToString(), true).First();
                    NumericUpDown temp_nudMarkup10 = (NumericUpDown)grpMarkup.Controls.Find("nudMarkup1" + (i - 1).ToString(), true).First();
                    temp_nudCRCol10.Value = decimal.Parse(ruleList[0]["minCR"].InnerText);
                    temp_nudCRCol20.Value = decimal.Parse(ruleList[0]["maxCR"].InnerText);
                    temp_nudMarkup10.Value = decimal.Parse(ruleList[0]["markup"].InnerText);
                }
                //value
                string minCR = ruleList[i]["minCR"].InnerText;
                string maxCR = ruleList[i]["maxCR"].InnerText;
                string markup = ruleList[i]["markup"].InnerText;

                Point locationNUDCR1 = temp_nudCRCol1.Location;
                locationNUDCR1.X = temp_nudCRCol1.Location.X;
                locationNUDCR1.Y += temp_nudCRCol1.Height + _space;

                Point locationNUDCR2 = temp_nudCRCol2.Location;
                locationNUDCR2.X = temp_nudCRCol2.Location.X;
                locationNUDCR2.Y += temp_nudCRCol2.Height + _space;

                Point locationNUDMarkup1 = temp_nudMarkup1.Location;
                locationNUDMarkup1.X = temp_nudMarkup1.Location.X;
                locationNUDMarkup1.Y += temp_nudMarkup1.Height + _space;

                NumericUpDown nud1 = CreateNUD(locationNUDCR1, "nudCRCol1" + i, decimal.Parse(minCR));
                NumericUpDown nud2 = CreateNUD(locationNUDCR2, "nudCRCol2" + i, decimal.Parse(maxCR));
                NumericUpDown nud3 = CreateNUD(locationNUDMarkup1, "nudMarkup1" + i, decimal.Parse(markup));

                lastYNUD = nud1.Location.Y;

                grpCostRange.Controls.Add(nud1);
                grpCostRange.Controls.Add(nud2);
                grpMarkup.Controls.Add(nud3);

            }
            Point plusButtonLocation = btnMoreTxt.Location;
            plusButtonLocation.Y = lastYNUD + _height + 5;
            btnMoreTxt.Location = plusButtonLocation;

            Point minusButtonLocation = btnLessTxt.Location;
            minusButtonLocation.Y = lastYNUD + _height + 5;
            btnLessTxt.Location = minusButtonLocation;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
