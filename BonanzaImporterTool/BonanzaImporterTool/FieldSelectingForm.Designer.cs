namespace BonanzaImporterTool
{
    partial class FieldSelectingForm
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
            this.btnFSF_SelectAll = new System.Windows.Forms.Button();
            this.btnSFS_DeselectAll = new System.Windows.Forms.Button();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.grbSFS_Fields = new System.Windows.Forms.GroupBox();
            this.chkforce_update = new System.Windows.Forms.CheckBox();
            this.chktrait = new System.Windows.Forms.CheckBox();
            this.chkquantity = new System.Windows.Forms.CheckBox();
            this.chkworldwide_shipping_carrier = new System.Windows.Forms.CheckBox();
            this.chkworldwide_shipping_type = new System.Windows.Forms.CheckBox();
            this.chkworldwide_shipping_price = new System.Windows.Forms.CheckBox();
            this.chksku = new System.Windows.Forms.CheckBox();
            this.chkshipping_package = new System.Windows.Forms.CheckBox();
            this.chkshipping_carrier = new System.Windows.Forms.CheckBox();
            this.chkshipping_oz = new System.Windows.Forms.CheckBox();
            this.chkshipping_lbs = new System.Windows.Forms.CheckBox();
            this.chkshipping_service = new System.Windows.Forms.CheckBox();
            this.chkshipping_type = new System.Windows.Forms.CheckBox();
            this.chkshipping_price = new System.Windows.Forms.CheckBox();
            this.chkbooth_category = new System.Windows.Forms.CheckBox();
            this.chkcategory = new System.Windows.Forms.CheckBox();
            this.chkimages = new System.Windows.Forms.CheckBox();
            this.chkprice = new System.Windows.Forms.CheckBox();
            this.chkdescription = new System.Windows.Forms.CheckBox();
            this.chkTitle = new System.Windows.Forms.CheckBox();
            this.chkID = new System.Windows.Forms.CheckBox();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.txtConditionStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMoreTraits = new System.Windows.Forms.TextBox();
            this.chkReplaceZeroQtt = new System.Windows.Forms.CheckBox();
            this.nudReplaceZeroQtt = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudWorldwideFlatRate = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoDes_Start = new System.Windows.Forms.RadioButton();
            this.rdoDes_End = new System.Windows.Forms.RadioButton();
            this.grbSFS_Fields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplaceZeroQtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorldwideFlatRate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFSF_SelectAll
            // 
            this.btnFSF_SelectAll.Location = new System.Drawing.Point(164, 287);
            this.btnFSF_SelectAll.Name = "btnFSF_SelectAll";
            this.btnFSF_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnFSF_SelectAll.TabIndex = 1;
            this.btnFSF_SelectAll.Text = "Select All";
            this.btnFSF_SelectAll.UseVisualStyleBackColor = true;
            this.btnFSF_SelectAll.Click += new System.EventHandler(this.btnFSF_SelectAll_Click);
            // 
            // btnSFS_DeselectAll
            // 
            this.btnSFS_DeselectAll.Location = new System.Drawing.Point(255, 287);
            this.btnSFS_DeselectAll.Name = "btnSFS_DeselectAll";
            this.btnSFS_DeselectAll.Size = new System.Drawing.Size(90, 23);
            this.btnSFS_DeselectAll.TabIndex = 1;
            this.btnSFS_DeselectAll.Text = "Deselect All";
            this.btnSFS_DeselectAll.UseVisualStyleBackColor = true;
            this.btnSFS_DeselectAll.Click += new System.EventHandler(this.btnSFS_DeselectAll_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(370, 287);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(102, 23);
            this.btnSaveAndClose.TabIndex = 2;
            this.btnSaveAndClose.Text = "Save And Close...";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // grbSFS_Fields
            // 
            this.grbSFS_Fields.Controls.Add(this.chkforce_update);
            this.grbSFS_Fields.Controls.Add(this.chktrait);
            this.grbSFS_Fields.Controls.Add(this.chkquantity);
            this.grbSFS_Fields.Controls.Add(this.chkworldwide_shipping_carrier);
            this.grbSFS_Fields.Controls.Add(this.chkworldwide_shipping_type);
            this.grbSFS_Fields.Controls.Add(this.chkworldwide_shipping_price);
            this.grbSFS_Fields.Controls.Add(this.chksku);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_package);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_carrier);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_oz);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_lbs);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_service);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_type);
            this.grbSFS_Fields.Controls.Add(this.chkshipping_price);
            this.grbSFS_Fields.Controls.Add(this.chkbooth_category);
            this.grbSFS_Fields.Controls.Add(this.chkcategory);
            this.grbSFS_Fields.Controls.Add(this.chkimages);
            this.grbSFS_Fields.Controls.Add(this.chkprice);
            this.grbSFS_Fields.Controls.Add(this.chkdescription);
            this.grbSFS_Fields.Controls.Add(this.chkTitle);
            this.grbSFS_Fields.Controls.Add(this.chkID);
            this.grbSFS_Fields.Location = new System.Drawing.Point(12, 4);
            this.grbSFS_Fields.Name = "grbSFS_Fields";
            this.grbSFS_Fields.Size = new System.Drawing.Size(553, 187);
            this.grbSFS_Fields.TabIndex = 3;
            this.grbSFS_Fields.TabStop = false;
            this.grbSFS_Fields.Text = "Fields";
            // 
            // chkforce_update
            // 
            this.chkforce_update.AutoSize = true;
            this.chkforce_update.Checked = true;
            this.chkforce_update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkforce_update.Enabled = false;
            this.chkforce_update.Location = new System.Drawing.Point(132, 152);
            this.chkforce_update.Name = "chkforce_update";
            this.chkforce_update.Size = new System.Drawing.Size(89, 17);
            this.chkforce_update.TabIndex = 21;
            this.chkforce_update.Text = "force_update";
            this.chkforce_update.UseVisualStyleBackColor = true;
            // 
            // chktrait
            // 
            this.chktrait.AutoSize = true;
            this.chktrait.Checked = true;
            this.chktrait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chktrait.Enabled = false;
            this.chktrait.Location = new System.Drawing.Point(83, 152);
            this.chktrait.Name = "chktrait";
            this.chktrait.Size = new System.Drawing.Size(43, 17);
            this.chktrait.TabIndex = 20;
            this.chktrait.Text = "trait";
            this.chktrait.UseVisualStyleBackColor = true;
            // 
            // chkquantity
            // 
            this.chkquantity.AutoSize = true;
            this.chkquantity.Checked = true;
            this.chkquantity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkquantity.Enabled = false;
            this.chkquantity.Location = new System.Drawing.Point(13, 152);
            this.chkquantity.Name = "chkquantity";
            this.chkquantity.Size = new System.Drawing.Size(63, 17);
            this.chkquantity.TabIndex = 19;
            this.chkquantity.Text = "quantity";
            this.chkquantity.UseVisualStyleBackColor = true;
            // 
            // chkworldwide_shipping_carrier
            // 
            this.chkworldwide_shipping_carrier.AutoSize = true;
            this.chkworldwide_shipping_carrier.Checked = true;
            this.chkworldwide_shipping_carrier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkworldwide_shipping_carrier.Enabled = false;
            this.chkworldwide_shipping_carrier.Location = new System.Drawing.Point(314, 120);
            this.chkworldwide_shipping_carrier.Name = "chkworldwide_shipping_carrier";
            this.chkworldwide_shipping_carrier.Size = new System.Drawing.Size(153, 17);
            this.chkworldwide_shipping_carrier.TabIndex = 18;
            this.chkworldwide_shipping_carrier.Text = "worldwide_shipping_carrier";
            this.chkworldwide_shipping_carrier.UseVisualStyleBackColor = true;
            // 
            // chkworldwide_shipping_type
            // 
            this.chkworldwide_shipping_type.AutoSize = true;
            this.chkworldwide_shipping_type.Checked = true;
            this.chkworldwide_shipping_type.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkworldwide_shipping_type.Enabled = false;
            this.chkworldwide_shipping_type.Location = new System.Drawing.Point(164, 120);
            this.chkworldwide_shipping_type.Name = "chkworldwide_shipping_type";
            this.chkworldwide_shipping_type.Size = new System.Drawing.Size(144, 17);
            this.chkworldwide_shipping_type.TabIndex = 17;
            this.chkworldwide_shipping_type.Text = "worldwide_shipping_type";
            this.chkworldwide_shipping_type.UseVisualStyleBackColor = true;
            // 
            // chkworldwide_shipping_price
            // 
            this.chkworldwide_shipping_price.AutoSize = true;
            this.chkworldwide_shipping_price.Checked = true;
            this.chkworldwide_shipping_price.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkworldwide_shipping_price.Enabled = false;
            this.chkworldwide_shipping_price.Location = new System.Drawing.Point(13, 120);
            this.chkworldwide_shipping_price.Name = "chkworldwide_shipping_price";
            this.chkworldwide_shipping_price.Size = new System.Drawing.Size(147, 17);
            this.chkworldwide_shipping_price.TabIndex = 16;
            this.chkworldwide_shipping_price.Text = "worldwide_shipping_price";
            this.chkworldwide_shipping_price.UseVisualStyleBackColor = true;
            // 
            // chksku
            // 
            this.chksku.AutoSize = true;
            this.chksku.Checked = true;
            this.chksku.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chksku.Enabled = false;
            this.chksku.Location = new System.Drawing.Point(416, 86);
            this.chksku.Name = "chksku";
            this.chksku.Size = new System.Drawing.Size(43, 17);
            this.chksku.TabIndex = 15;
            this.chksku.Text = "sku";
            this.chksku.UseVisualStyleBackColor = true;
            // 
            // chkshipping_package
            // 
            this.chkshipping_package.AutoSize = true;
            this.chkshipping_package.Checked = true;
            this.chkshipping_package.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_package.Enabled = false;
            this.chkshipping_package.Location = new System.Drawing.Point(297, 86);
            this.chkshipping_package.Name = "chkshipping_package";
            this.chkshipping_package.Size = new System.Drawing.Size(113, 17);
            this.chkshipping_package.TabIndex = 14;
            this.chkshipping_package.Text = "shipping_package";
            this.chkshipping_package.UseVisualStyleBackColor = true;
            // 
            // chkshipping_carrier
            // 
            this.chkshipping_carrier.AutoSize = true;
            this.chkshipping_carrier.Checked = true;
            this.chkshipping_carrier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_carrier.Enabled = false;
            this.chkshipping_carrier.Location = new System.Drawing.Point(191, 86);
            this.chkshipping_carrier.Name = "chkshipping_carrier";
            this.chkshipping_carrier.Size = new System.Drawing.Size(100, 17);
            this.chkshipping_carrier.TabIndex = 13;
            this.chkshipping_carrier.Text = "shipping_carrier";
            this.chkshipping_carrier.UseVisualStyleBackColor = true;
            // 
            // chkshipping_oz
            // 
            this.chkshipping_oz.AutoSize = true;
            this.chkshipping_oz.Checked = true;
            this.chkshipping_oz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_oz.Enabled = false;
            this.chkshipping_oz.Location = new System.Drawing.Point(103, 86);
            this.chkshipping_oz.Name = "chkshipping_oz";
            this.chkshipping_oz.Size = new System.Drawing.Size(82, 17);
            this.chkshipping_oz.TabIndex = 12;
            this.chkshipping_oz.Text = "shipping_oz";
            this.chkshipping_oz.UseVisualStyleBackColor = true;
            // 
            // chkshipping_lbs
            // 
            this.chkshipping_lbs.AutoSize = true;
            this.chkshipping_lbs.Checked = true;
            this.chkshipping_lbs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_lbs.Enabled = false;
            this.chkshipping_lbs.Location = new System.Drawing.Point(13, 86);
            this.chkshipping_lbs.Name = "chkshipping_lbs";
            this.chkshipping_lbs.Size = new System.Drawing.Size(84, 17);
            this.chkshipping_lbs.TabIndex = 11;
            this.chkshipping_lbs.Text = "shipping_lbs";
            this.chkshipping_lbs.UseVisualStyleBackColor = true;
            // 
            // chkshipping_service
            // 
            this.chkshipping_service.AutoSize = true;
            this.chkshipping_service.Checked = true;
            this.chkshipping_service.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_service.Enabled = false;
            this.chkshipping_service.Location = new System.Drawing.Point(317, 53);
            this.chkshipping_service.Name = "chkshipping_service";
            this.chkshipping_service.Size = new System.Drawing.Size(105, 17);
            this.chkshipping_service.TabIndex = 10;
            this.chkshipping_service.Text = "shipping_service";
            this.chkshipping_service.UseVisualStyleBackColor = true;
            // 
            // chkshipping_type
            // 
            this.chkshipping_type.AutoSize = true;
            this.chkshipping_type.Checked = true;
            this.chkshipping_type.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_type.Enabled = false;
            this.chkshipping_type.Location = new System.Drawing.Point(220, 53);
            this.chkshipping_type.Name = "chkshipping_type";
            this.chkshipping_type.Size = new System.Drawing.Size(91, 17);
            this.chkshipping_type.TabIndex = 9;
            this.chkshipping_type.Text = "shipping_type";
            this.chkshipping_type.UseVisualStyleBackColor = true;
            // 
            // chkshipping_price
            // 
            this.chkshipping_price.AutoSize = true;
            this.chkshipping_price.Checked = true;
            this.chkshipping_price.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshipping_price.Enabled = false;
            this.chkshipping_price.Location = new System.Drawing.Point(120, 53);
            this.chkshipping_price.Name = "chkshipping_price";
            this.chkshipping_price.Size = new System.Drawing.Size(94, 17);
            this.chkshipping_price.TabIndex = 8;
            this.chkshipping_price.Text = "shipping_price";
            this.chkshipping_price.UseVisualStyleBackColor = true;
            // 
            // chkbooth_category
            // 
            this.chkbooth_category.AutoSize = true;
            this.chkbooth_category.Checked = true;
            this.chkbooth_category.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbooth_category.Enabled = false;
            this.chkbooth_category.Location = new System.Drawing.Point(13, 53);
            this.chkbooth_category.Name = "chkbooth_category";
            this.chkbooth_category.Size = new System.Drawing.Size(100, 17);
            this.chkbooth_category.TabIndex = 7;
            this.chkbooth_category.Text = "booth_category";
            this.chkbooth_category.UseVisualStyleBackColor = true;
            // 
            // chkcategory
            // 
            this.chkcategory.AutoSize = true;
            this.chkcategory.Checked = true;
            this.chkcategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkcategory.Enabled = false;
            this.chkcategory.Location = new System.Drawing.Point(326, 18);
            this.chkcategory.Name = "chkcategory";
            this.chkcategory.Size = new System.Drawing.Size(67, 17);
            this.chkcategory.TabIndex = 6;
            this.chkcategory.Text = "category";
            this.chkcategory.UseVisualStyleBackColor = true;
            // 
            // chkimages
            // 
            this.chkimages.AutoSize = true;
            this.chkimages.Checked = true;
            this.chkimages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkimages.Enabled = false;
            this.chkimages.Location = new System.Drawing.Point(261, 18);
            this.chkimages.Name = "chkimages";
            this.chkimages.Size = new System.Drawing.Size(59, 17);
            this.chkimages.TabIndex = 5;
            this.chkimages.Text = "images";
            this.chkimages.UseVisualStyleBackColor = true;
            // 
            // chkprice
            // 
            this.chkprice.AutoSize = true;
            this.chkprice.Checked = true;
            this.chkprice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkprice.Enabled = false;
            this.chkprice.Location = new System.Drawing.Point(206, 17);
            this.chkprice.Name = "chkprice";
            this.chkprice.Size = new System.Drawing.Size(49, 17);
            this.chkprice.TabIndex = 4;
            this.chkprice.Text = "price";
            this.chkprice.UseVisualStyleBackColor = true;
            // 
            // chkdescription
            // 
            this.chkdescription.AutoSize = true;
            this.chkdescription.Checked = true;
            this.chkdescription.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkdescription.Enabled = false;
            this.chkdescription.Location = new System.Drawing.Point(123, 18);
            this.chkdescription.Name = "chkdescription";
            this.chkdescription.Size = new System.Drawing.Size(77, 17);
            this.chkdescription.TabIndex = 3;
            this.chkdescription.Text = "description";
            this.chkdescription.UseVisualStyleBackColor = true;
            // 
            // chkTitle
            // 
            this.chkTitle.AutoSize = true;
            this.chkTitle.Checked = true;
            this.chkTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTitle.Enabled = false;
            this.chkTitle.Location = new System.Drawing.Point(75, 18);
            this.chkTitle.Name = "chkTitle";
            this.chkTitle.Size = new System.Drawing.Size(42, 17);
            this.chkTitle.TabIndex = 2;
            this.chkTitle.Text = "title";
            this.chkTitle.UseVisualStyleBackColor = true;
            // 
            // chkID
            // 
            this.chkID.AutoSize = true;
            this.chkID.Checked = true;
            this.chkID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkID.Enabled = false;
            this.chkID.Location = new System.Drawing.Point(13, 18);
            this.chkID.Name = "chkID";
            this.chkID.Size = new System.Drawing.Size(34, 17);
            this.chkID.TabIndex = 1;
            this.chkID.Text = "id";
            this.chkID.UseVisualStyleBackColor = true;
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Location = new System.Drawing.Point(74, 287);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(75, 23);
            this.btnSetDefault.TabIndex = 1;
            this.btnSetDefault.Text = "Set Default";
            this.btnSetDefault.UseVisualStyleBackColor = true;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // txtConditionStatus
            // 
            this.txtConditionStatus.Location = new System.Drawing.Point(75, 197);
            this.txtConditionStatus.Name = "txtConditionStatus";
            this.txtConditionStatus.Size = new System.Drawing.Size(84, 20);
            this.txtConditionStatus.TabIndex = 4;
            this.txtConditionStatus.Text = "New";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Condition:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "More traits:";
            // 
            // txtMoreTraits
            // 
            this.txtMoreTraits.Location = new System.Drawing.Point(234, 197);
            this.txtMoreTraits.Name = "txtMoreTraits";
            this.txtMoreTraits.Size = new System.Drawing.Size(90, 20);
            this.txtMoreTraits.TabIndex = 7;
            // 
            // chkReplaceZeroQtt
            // 
            this.chkReplaceZeroQtt.AutoSize = true;
            this.chkReplaceZeroQtt.Checked = true;
            this.chkReplaceZeroQtt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReplaceZeroQtt.Location = new System.Drawing.Point(18, 225);
            this.chkReplaceZeroQtt.Name = "chkReplaceZeroQtt";
            this.chkReplaceZeroQtt.Size = new System.Drawing.Size(107, 17);
            this.chkReplaceZeroQtt.TabIndex = 10;
            this.chkReplaceZeroQtt.Text = "Replace 0 qtt by:";
            this.chkReplaceZeroQtt.UseVisualStyleBackColor = true;
            // 
            // nudReplaceZeroQtt
            // 
            this.nudReplaceZeroQtt.Location = new System.Drawing.Point(131, 224);
            this.nudReplaceZeroQtt.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudReplaceZeroQtt.Name = "nudReplaceZeroQtt";
            this.nudReplaceZeroQtt.Size = new System.Drawing.Size(64, 20);
            this.nudReplaceZeroQtt.TabIndex = 11;
            this.nudReplaceZeroQtt.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Worldwide flat rate of:   $";
            // 
            // nudWorldwideFlatRate
            // 
            this.nudWorldwideFlatRate.Location = new System.Drawing.Point(461, 197);
            this.nudWorldwideFlatRate.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudWorldwideFlatRate.Name = "nudWorldwideFlatRate";
            this.nudWorldwideFlatRate.Size = new System.Drawing.Size(64, 20);
            this.nudWorldwideFlatRate.TabIndex = 11;
            this.nudWorldwideFlatRate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoDes_End);
            this.groupBox1.Controls.Add(this.rdoDes_Start);
            this.groupBox1.Location = new System.Drawing.Point(219, 225);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 45);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insert description from";
            // 
            // rdoDes_Start
            // 
            this.rdoDes_Start.AutoSize = true;
            this.rdoDes_Start.Checked = true;
            this.rdoDes_Start.Location = new System.Drawing.Point(14, 19);
            this.rdoDes_Start.Name = "rdoDes_Start";
            this.rdoDes_Start.Size = new System.Drawing.Size(47, 17);
            this.rdoDes_Start.TabIndex = 0;
            this.rdoDes_Start.TabStop = true;
            this.rdoDes_Start.Text = "Start";
            this.rdoDes_Start.UseVisualStyleBackColor = true;
            // 
            // rdoDes_End
            // 
            this.rdoDes_End.AutoSize = true;
            this.rdoDes_End.Location = new System.Drawing.Point(80, 19);
            this.rdoDes_End.Name = "rdoDes_End";
            this.rdoDes_End.Size = new System.Drawing.Size(44, 17);
            this.rdoDes_End.TabIndex = 0;
            this.rdoDes_End.Text = "End";
            this.rdoDes_End.UseVisualStyleBackColor = true;
            // 
            // FieldSelectingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 332);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudWorldwideFlatRate);
            this.Controls.Add(this.nudReplaceZeroQtt);
            this.Controls.Add(this.chkReplaceZeroQtt);
            this.Controls.Add(this.txtMoreTraits);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConditionStatus);
            this.Controls.Add(this.grbSFS_Fields);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnSFS_DeselectAll);
            this.Controls.Add(this.btnSetDefault);
            this.Controls.Add(this.btnFSF_SelectAll);
            this.Name = "FieldSelectingForm";
            this.Text = "Select Fields";
            this.Load += new System.EventHandler(this.FieldSelectingForm_Load);
            this.grbSFS_Fields.ResumeLayout(false);
            this.grbSFS_Fields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplaceZeroQtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorldwideFlatRate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFSF_SelectAll;
        private System.Windows.Forms.Button btnSFS_DeselectAll;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.GroupBox grbSFS_Fields;
        private System.Windows.Forms.CheckBox chkforce_update;
        private System.Windows.Forms.CheckBox chktrait;
        private System.Windows.Forms.CheckBox chkquantity;
        private System.Windows.Forms.CheckBox chkworldwide_shipping_carrier;
        private System.Windows.Forms.CheckBox chkworldwide_shipping_type;
        private System.Windows.Forms.CheckBox chkworldwide_shipping_price;
        private System.Windows.Forms.CheckBox chksku;
        private System.Windows.Forms.CheckBox chkshipping_package;
        private System.Windows.Forms.CheckBox chkshipping_carrier;
        private System.Windows.Forms.CheckBox chkshipping_oz;
        private System.Windows.Forms.CheckBox chkshipping_lbs;
        private System.Windows.Forms.CheckBox chkshipping_service;
        private System.Windows.Forms.CheckBox chkshipping_type;
        private System.Windows.Forms.CheckBox chkshipping_price;
        private System.Windows.Forms.CheckBox chkbooth_category;
        private System.Windows.Forms.CheckBox chkcategory;
        private System.Windows.Forms.CheckBox chkimages;
        private System.Windows.Forms.CheckBox chkprice;
        private System.Windows.Forms.CheckBox chkdescription;
        private System.Windows.Forms.CheckBox chkTitle;
        private System.Windows.Forms.CheckBox chkID;
        private System.Windows.Forms.Button btnSetDefault;
        private System.Windows.Forms.TextBox txtConditionStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMoreTraits;
        private System.Windows.Forms.CheckBox chkReplaceZeroQtt;
        private System.Windows.Forms.NumericUpDown nudReplaceZeroQtt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudWorldwideFlatRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoDes_End;
        private System.Windows.Forms.RadioButton rdoDes_Start;
    }
}