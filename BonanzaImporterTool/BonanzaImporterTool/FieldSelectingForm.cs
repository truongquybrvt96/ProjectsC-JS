using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonanzaImporterTool
{
    public partial class FieldSelectingForm : Form
    {
        public FieldSelectingForm()
        {
            InitializeComponent();
        }

        private void btnFSF_SelectAll_Click(object sender, EventArgs e)
        {
            IEnumerable<CheckBox> ctrCol = grbSFS_Fields.Controls.OfType<CheckBox>().OrderBy(c => c.TabIndex);
            foreach(CheckBox ctr in ctrCol)
            {
                ctr.Checked = true;
            }
        }

        private void btnSFS_DeselectAll_Click(object sender, EventArgs e)
        {
            IEnumerable<CheckBox> ctrCol = grbSFS_Fields.Controls.OfType<CheckBox>().OrderBy(c => c.TabIndex);
            foreach (CheckBox ctr in ctrCol)
            {
                ctr.Checked = false;
            }
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            btnSFS_DeselectAll_Click(sender, e);
            chkTitle.Checked = true;
            chkdescription.Checked = true;
            chkprice.Checked = true;
            chkimages.Checked = true;
            chksku.Checked = true;
            chkquantity.Checked = true;
            chktrait.Checked = true;
            chkID.Checked = true;
            chkforce_update.Checked = true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            Transfer.fields.Clear();
            string fieldsString = "";
            string tempTraits = "";
            IEnumerable<CheckBox> ctrCol = grbSFS_Fields.Controls.OfType<CheckBox>().OrderBy(c => c.TabIndex);
            foreach (CheckBox ctr in ctrCol)
            {
                if(ctr.Checked)
                {
                    Transfer.fields.Add(ctr.Name);
                    tempTraits += ctr.Name + " ";
                }
            }

            Transfer.conditionStatus = txtConditionStatus.Text;
            Transfer.moreTraits = txtMoreTraits.Text;
            Transfer.worldwideFlatRateOf = nudWorldwideFlatRate.Value.ToString();
            Transfer.replaceQttBy = nudReplaceZeroQtt.Value.ToString();
            if (rdoDes_Start.Checked)
            {
                Transfer.desInsertStart = true;
                fieldsString += "desStart:true\n";
            }
            else
            {
                Transfer.desInsertStart = false;
                fieldsString += "desStart:false\n";
            }
                

            fieldsString += "conditionStatus:" + txtConditionStatus.Text + "\n";
            fieldsString += "moreTraits:" + txtMoreTraits.Text + "\n";
            fieldsString += "worldwideFlatRate:" + nudWorldwideFlatRate.Value.ToString() + "\n";
            fieldsString += "replaceZeroQtt:" + nudReplaceZeroQtt.Value.ToString() + "\n";
            fieldsString += "traits:" + tempTraits;
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + @"\data\FieldSelectingForm.txt", fieldsString.ToString());


            this.Close();
        }

        private void FieldSelectingForm_Load(object sender, EventArgs e)
        {
            string[] allTextArr = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\data\FieldSelectingForm.txt");
            for(int i = 0; i < allTextArr.Length; i++)
            {
                if(allTextArr[i] != "" && allTextArr[i] != " ")
                {
                    string[] splitter = allTextArr[i].Split(':');
                    if(splitter.Length == 2)
                    {
                        string _lable = splitter[0];
                        string _value = splitter[1];
                        switch (_lable)
                        {
                            case "desStart":
                                {
                                    if (_value == "true")
                                    {
                                        rdoDes_Start.Checked = true;
                                        rdoDes_End.Checked = false;
                                    }
                                    else
                                    {
                                        rdoDes_End.Checked = true;
                                        rdoDes_Start.Checked = false;
                                    }
                                }
                                break;
                            case "conditionStatus":
                                txtConditionStatus.Text = _value;
                                break;
                            case "moreTraits":
                                txtMoreTraits.Text = _value;
                                break;
                            case "worldwideFlatRate":
                                nudWorldwideFlatRate.Value = decimal.Parse(_value);
                                break;
                            case "replaceZeroQtt":
                                nudReplaceZeroQtt.Value = decimal.Parse(_value);
                                break;
                            case "traits":
                                {
                                    string[] values = _value.Split(' ');
                                    for (int j = 0; j < values.Length; j++)
                                    {
                                        if (values[j] != "" && values[j] != " ")
                                        {
                                            CheckBox chk = (CheckBox)grbSFS_Fields.Controls.Find(values[j], false).First();
                                            chk.Checked = true;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    
                }
            }

        }
    }
}
