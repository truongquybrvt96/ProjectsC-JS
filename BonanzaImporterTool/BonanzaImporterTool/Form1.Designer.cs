namespace BonanzaImporterTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelFields = new System.Windows.Forms.Button();
            this.btnToCSVFile = new System.Windows.Forms.Button();
            this.btnPricingRules = new System.Windows.Forms.Button();
            this.btnWriteToXML = new System.Windows.Forms.Button();
            this.lblURL = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLinks = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoOriginalPrice = new System.Windows.Forms.RadioButton();
            this.rdoDiscountPrice = new System.Windows.Forms.RadioButton();
            this.lblTxtLoadingStatus = new System.Windows.Forms.Label();
            this.lable3 = new System.Windows.Forms.Label();
            this.lblProcess = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblErrorCount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblOutputFolderPath = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text file: ";
            // 
            // btnSelFields
            // 
            this.btnSelFields.Location = new System.Drawing.Point(16, 125);
            this.btnSelFields.Name = "btnSelFields";
            this.btnSelFields.Size = new System.Drawing.Size(93, 33);
            this.btnSelFields.TabIndex = 2;
            this.btnSelFields.Text = "Select fields...";
            this.btnSelFields.UseVisualStyleBackColor = true;
            this.btnSelFields.Click += new System.EventHandler(this.btnSelFields_Click);
            // 
            // btnToCSVFile
            // 
            this.btnToCSVFile.Enabled = false;
            this.btnToCSVFile.Location = new System.Drawing.Point(257, 125);
            this.btnToCSVFile.Name = "btnToCSVFile";
            this.btnToCSVFile.Size = new System.Drawing.Size(130, 33);
            this.btnToCSVFile.TabIndex = 4;
            this.btnToCSVFile.Text = "Write to CSV file...";
            this.btnToCSVFile.UseVisualStyleBackColor = true;
            this.btnToCSVFile.Click += new System.EventHandler(this.btnToCSVFile_Click);
            // 
            // btnPricingRules
            // 
            this.btnPricingRules.Location = new System.Drawing.Point(115, 125);
            this.btnPricingRules.Name = "btnPricingRules";
            this.btnPricingRules.Size = new System.Drawing.Size(130, 33);
            this.btnPricingRules.TabIndex = 4;
            this.btnPricingRules.Text = "Pricing rules...";
            this.btnPricingRules.UseVisualStyleBackColor = true;
            this.btnPricingRules.Click += new System.EventHandler(this.btnPricingRules_Click);
            // 
            // btnWriteToXML
            // 
            this.btnWriteToXML.Enabled = false;
            this.btnWriteToXML.Location = new System.Drawing.Point(415, 125);
            this.btnWriteToXML.Name = "btnWriteToXML";
            this.btnWriteToXML.Size = new System.Drawing.Size(130, 33);
            this.btnWriteToXML.TabIndex = 5;
            this.btnWriteToXML.Text = "Write to XML file...";
            this.btnWriteToXML.UseVisualStyleBackColor = true;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(70, 13);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(0, 13);
            this.lblURL.TabIndex = 6;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(600, 13);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(88, 23);
            this.btnSelectFile.TabIndex = 7;
            this.btnSelectFile.Text = "Select file...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Links:";
            // 
            // lblLinks
            // 
            this.lblLinks.AutoSize = true;
            this.lblLinks.Location = new System.Drawing.Point(57, 38);
            this.lblLinks.Name = "lblLinks";
            this.lblLinks.Size = new System.Drawing.Size(0, 13);
            this.lblLinks.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoOriginalPrice);
            this.groupBox1.Controls.Add(this.rdoDiscountPrice);
            this.groupBox1.Location = new System.Drawing.Point(15, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 45);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Price";
            // 
            // rdoOriginalPrice
            // 
            this.rdoOriginalPrice.AutoSize = true;
            this.rdoOriginalPrice.Checked = true;
            this.rdoOriginalPrice.Location = new System.Drawing.Point(106, 19);
            this.rdoOriginalPrice.Name = "rdoOriginalPrice";
            this.rdoOriginalPrice.Size = new System.Drawing.Size(86, 17);
            this.rdoOriginalPrice.TabIndex = 0;
            this.rdoOriginalPrice.TabStop = true;
            this.rdoOriginalPrice.Text = "Original price";
            this.rdoOriginalPrice.UseVisualStyleBackColor = true;
            // 
            // rdoDiscountPrice
            // 
            this.rdoDiscountPrice.AutoSize = true;
            this.rdoDiscountPrice.Location = new System.Drawing.Point(7, 20);
            this.rdoDiscountPrice.Name = "rdoDiscountPrice";
            this.rdoDiscountPrice.Size = new System.Drawing.Size(93, 17);
            this.rdoDiscountPrice.TabIndex = 0;
            this.rdoDiscountPrice.Text = "Discount price";
            this.rdoDiscountPrice.UseVisualStyleBackColor = true;
            // 
            // lblTxtLoadingStatus
            // 
            this.lblTxtLoadingStatus.AutoSize = true;
            this.lblTxtLoadingStatus.Location = new System.Drawing.Point(121, 38);
            this.lblTxtLoadingStatus.Name = "lblTxtLoadingStatus";
            this.lblTxtLoadingStatus.Size = new System.Drawing.Size(0, 13);
            this.lblTxtLoadingStatus.TabIndex = 11;
            // 
            // lable3
            // 
            this.lable3.AutoSize = true;
            this.lable3.Location = new System.Drawing.Point(16, 200);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(48, 13);
            this.lable3.TabIndex = 12;
            this.lable3.Text = "Process:";
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(73, 200);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(60, 13);
            this.lblProcess.TabIndex = 12;
            this.lblProcess.Text = "none/none";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Error(s):";
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.ForeColor = System.Drawing.Color.Red;
            this.lblErrorCount.Location = new System.Drawing.Point(73, 223);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(13, 13);
            this.lblErrorCount.TabIndex = 12;
            this.lblErrorCount.Text = "0";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(18, 250);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Select output folder...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblOutputFolderPath
            // 
            this.lblOutputFolderPath.AutoSize = true;
            this.lblOutputFolderPath.Location = new System.Drawing.Point(146, 180);
            this.lblOutputFolderPath.Name = "lblOutputFolderPath";
            this.lblOutputFolderPath.Size = new System.Drawing.Size(31, 13);
            this.lblOutputFolderPath.TabIndex = 14;
            this.lblOutputFolderPath.Text = "none";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(15, 250);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(822, 290);
            this.webBrowser1.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(469, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 427);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.lblOutputFolderPath);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.lblErrorCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lable3);
            this.Controls.Add(this.lblTxtLoadingStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLinks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.btnWriteToXML);
            this.Controls.Add(this.btnPricingRules);
            this.Controls.Add(this.btnToCSVFile);
            this.Controls.Add(this.btnSelFields);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Bonanza Product Importer Tool (Beta 0.1)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelFields;
        private System.Windows.Forms.Button btnToCSVFile;
        private System.Windows.Forms.Button btnPricingRules;
        private System.Windows.Forms.Button btnWriteToXML;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLinks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoOriginalPrice;
        private System.Windows.Forms.RadioButton rdoDiscountPrice;
        private System.Windows.Forms.Label lblTxtLoadingStatus;
        private System.Windows.Forms.Label lable3;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblErrorCount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblOutputFolderPath;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button2;
    }
}

