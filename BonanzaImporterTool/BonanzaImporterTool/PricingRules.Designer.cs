namespace BonanzaImporterTool
{
    partial class FrmPricingRules
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
            this.grpCostRange = new System.Windows.Forms.GroupBox();
            this.btnLessTxt = new System.Windows.Forms.Button();
            this.btnMoreTxt = new System.Windows.Forms.Button();
            this.nudCRCol1 = new System.Windows.Forms.NumericUpDown();
            this.nudCRCol2 = new System.Windows.Forms.NumericUpDown();
            this.grpMarkup = new System.Windows.Forms.GroupBox();
            this.nudMarkup1 = new System.Windows.Forms.NumericUpDown();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpCostRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCRCol1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCRCol2)).BeginInit();
            this.grpMarkup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkup1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCostRange
            // 
            this.grpCostRange.Controls.Add(this.btnLessTxt);
            this.grpCostRange.Controls.Add(this.btnMoreTxt);
            this.grpCostRange.Controls.Add(this.nudCRCol1);
            this.grpCostRange.Controls.Add(this.nudCRCol2);
            this.grpCostRange.Location = new System.Drawing.Point(13, 13);
            this.grpCostRange.Name = "grpCostRange";
            this.grpCostRange.Size = new System.Drawing.Size(235, 598);
            this.grpCostRange.TabIndex = 1;
            this.grpCostRange.TabStop = false;
            this.grpCostRange.Text = "Cost range";
            // 
            // btnLessTxt
            // 
            this.btnLessTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessTxt.Location = new System.Drawing.Point(36, 46);
            this.btnLessTxt.Name = "btnLessTxt";
            this.btnLessTxt.Size = new System.Drawing.Size(25, 23);
            this.btnLessTxt.TabIndex = 1;
            this.btnLessTxt.Text = "-";
            this.btnLessTxt.UseVisualStyleBackColor = true;
            this.btnLessTxt.Click += new System.EventHandler(this.btnLessTxt_Click);
            // 
            // btnMoreTxt
            // 
            this.btnMoreTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoreTxt.Location = new System.Drawing.Point(5, 46);
            this.btnMoreTxt.Name = "btnMoreTxt";
            this.btnMoreTxt.Size = new System.Drawing.Size(25, 23);
            this.btnMoreTxt.TabIndex = 1;
            this.btnMoreTxt.Text = "+";
            this.btnMoreTxt.UseVisualStyleBackColor = true;
            this.btnMoreTxt.Click += new System.EventHandler(this.btnMoreTxt_Click);
            // 
            // nudCRCol1
            // 
            this.nudCRCol1.DecimalPlaces = 2;
            this.nudCRCol1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudCRCol1.Location = new System.Drawing.Point(6, 19);
            this.nudCRCol1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudCRCol1.Name = "nudCRCol1";
            this.nudCRCol1.Size = new System.Drawing.Size(100, 20);
            this.nudCRCol1.TabIndex = 0;
            this.nudCRCol1.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // nudCRCol2
            // 
            this.nudCRCol2.DecimalPlaces = 2;
            this.nudCRCol2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudCRCol2.Location = new System.Drawing.Point(129, 19);
            this.nudCRCol2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudCRCol2.Name = "nudCRCol2";
            this.nudCRCol2.Size = new System.Drawing.Size(100, 20);
            this.nudCRCol2.TabIndex = 0;
            this.nudCRCol2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // grpMarkup
            // 
            this.grpMarkup.Controls.Add(this.nudMarkup1);
            this.grpMarkup.Location = new System.Drawing.Point(264, 13);
            this.grpMarkup.Name = "grpMarkup";
            this.grpMarkup.Size = new System.Drawing.Size(117, 598);
            this.grpMarkup.TabIndex = 1;
            this.grpMarkup.TabStop = false;
            this.grpMarkup.Text = "Markup (Multiplier)";
            // 
            // nudMarkup1
            // 
            this.nudMarkup1.DecimalPlaces = 2;
            this.nudMarkup1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMarkup1.Location = new System.Drawing.Point(6, 19);
            this.nudMarkup1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudMarkup1.Name = "nudMarkup1";
            this.nudMarkup1.Size = new System.Drawing.Size(100, 20);
            this.nudMarkup1.TabIndex = 0;
            this.nudMarkup1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(262, 632);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(119, 23);
            this.btnSaveAndClose.TabIndex = 2;
            this.btnSaveAndClose.Text = "Save And Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(173, 632);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmPricingRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 663);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.grpMarkup);
            this.Controls.Add(this.grpCostRange);
            this.Name = "FrmPricingRules";
            this.Text = "Pricing Rules";
            this.Load += new System.EventHandler(this.FrmPricingRules_Load);
            this.grpCostRange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCRCol1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCRCol2)).EndInit();
            this.grpMarkup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCostRange;
        private System.Windows.Forms.NumericUpDown nudCRCol1;
        private System.Windows.Forms.NumericUpDown nudCRCol2;
        private System.Windows.Forms.GroupBox grpMarkup;
        private System.Windows.Forms.NumericUpDown nudMarkup1;
        private System.Windows.Forms.Button btnMoreTxt;
        private System.Windows.Forms.Button btnLessTxt;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.Button btnCancel;
    }
}