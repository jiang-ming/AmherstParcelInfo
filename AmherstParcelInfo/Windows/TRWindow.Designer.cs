namespace AmherstParcelInfo
{
    partial class TRWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listView1 = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtParcel = new System.Windows.Forms.TextBox();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTable = new System.Windows.Forms.Button();
            this.btnRestrictedStreet = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbBufferDistance = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnMap = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbLayoutScale = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbRight = new System.Windows.Forms.ComboBox();
            this.cmbLeft = new System.Windows.Forms.ComboBox();
            this.btnColorR = new System.Windows.Forms.Button();
            this.btnColorL = new System.Windows.Forms.Button();
            this.ckbBothSide = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLanes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNoOwnerNameParcelNum = new System.Windows.Forms.Label();
            this.lblAdjacentParcelNum = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.Location = new System.Drawing.Point(385, 32);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(207, 79);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtParcel);
            this.groupBox1.Controls.Add(this.cmbSearchBy);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_Clear);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 157);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parcel Search:";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.LemonChiffon;
            this.label12.Location = new System.Drawing.Point(6, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(586, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Search Result:";
            // 
            // txtParcel
            // 
            this.txtParcel.Location = new System.Drawing.Point(187, 13);
            this.txtParcel.Name = "txtParcel";
            this.txtParcel.Size = new System.Drawing.Size(243, 20);
            this.txtParcel.TabIndex = 16;
            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchBy.DropDownWidth = 80;
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Items.AddRange(new object[] {
            "Parcel Address",
            "Owner Name",
            "Printkey"});
            this.cmbSearchBy.Location = new System.Drawing.Point(75, 13);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(106, 21);
            this.cmbSearchBy.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Search By:";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Image = global::AmherstParcelInfo.Properties.Resources.SelectionClearSelection_B_16;
            this.btn_Clear.Location = new System.Drawing.Point(517, 13);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 13;
            this.btn_Clear.Text = "Clear All";
            this.btn_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::AmherstParcelInfo.Properties.Resources.GenericSearch16;
            this.btnSearch.Location = new System.Drawing.Point(436, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.Location = new System.Drawing.Point(6, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(587, 94);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView2.Location = new System.Drawing.Point(6, 32);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView2.RowHeadersWidth = 25;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(373, 79);
            this.dataGridView2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(373, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Adjacent Parcels With Owner Name:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(385, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Adjacent Parcels Without Owner Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTable);
            this.groupBox2.Controls.Add(this.btnRestrictedStreet);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.btnMap);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(13, 298);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(599, 129);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Street and Restriction Info";
            // 
            // btnTable
            // 
            this.btnTable.Image = global::AmherstParcelInfo.Properties.Resources.TableDBase16;
            this.btnTable.Location = new System.Drawing.Point(495, 82);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(98, 37);
            this.btnTable.TabIndex = 5;
            this.btnTable.Text = "Export Table";
            this.btnTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnRestrictedStreet
            // 
            this.btnRestrictedStreet.Image = global::AmherstParcelInfo.Properties.Resources.ElementLineColor16;
            this.btnRestrictedStreet.Location = new System.Drawing.Point(385, 19);
            this.btnRestrictedStreet.Name = "btnRestrictedStreet";
            this.btnRestrictedStreet.Size = new System.Drawing.Size(208, 52);
            this.btnRestrictedStreet.TabIndex = 4;
            this.btnRestrictedStreet.Text = "Select Street";
            this.btnRestrictedStreet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestrictedStreet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestrictedStreet.UseVisualStyleBackColor = true;
            this.btnRestrictedStreet.Click += new System.EventHandler(this.btnRestrictedStreet_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmbBufferDistance);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(230, 71);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(143, 52);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Buffer Distance";
            // 
            // cmbBufferDistance
            // 
            this.cmbBufferDistance.FormattingEnabled = true;
            this.cmbBufferDistance.Location = new System.Drawing.Point(54, 20);
            this.cmbBufferDistance.Name = "cmbBufferDistance";
            this.cmbBufferDistance.Size = new System.Drawing.Size(50, 21);
            this.cmbBufferDistance.TabIndex = 2;
            this.cmbBufferDistance.SelectedIndexChanged += new System.EventHandler(this.cmbBufferDistance_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(105, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "feet";
            // 
            // btnMap
            // 
            this.btnMap.Image = global::AmherstParcelInfo.Properties.Resources.Map16;
            this.btnMap.Location = new System.Drawing.Point(385, 82);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(98, 37);
            this.btnMap.TabIndex = 6;
            this.btnMap.Text = "Export Map";
            this.btnMap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbLayoutScale);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(230, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(143, 48);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Layout Scale";
            // 
            // cmbLayoutScale
            // 
            this.cmbLayoutScale.FormattingEnabled = true;
            this.cmbLayoutScale.Location = new System.Drawing.Point(54, 20);
            this.cmbLayoutScale.Name = "cmbLayoutScale";
            this.cmbLayoutScale.Size = new System.Drawing.Size(50, 21);
            this.cmbLayoutScale.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(105, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "feet";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "1 inch =";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbRight);
            this.groupBox3.Controls.Add(this.cmbLeft);
            this.groupBox3.Controls.Add(this.btnColorR);
            this.groupBox3.Controls.Add(this.btnColorL);
            this.groupBox3.Controls.Add(this.ckbBothSide);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbLanes);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(218, 104);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Restriction and Road Width";
            // 
            // cmbRight
            // 
            this.cmbRight.FormattingEnabled = true;
            this.cmbRight.Location = new System.Drawing.Point(55, 77);
            this.cmbRight.Name = "cmbRight";
            this.cmbRight.Size = new System.Drawing.Size(157, 21);
            this.cmbRight.TabIndex = 28;
            this.cmbRight.SelectedIndexChanged += new System.EventHandler(this.cmbRight_SelectedIndexChanged);
            this.cmbRight.Leave += new System.EventHandler(this.cmbRight_Leave);
            // 
            // cmbLeft
            // 
            this.cmbLeft.FormattingEnabled = true;
            this.cmbLeft.Location = new System.Drawing.Point(55, 53);
            this.cmbLeft.Name = "cmbLeft";
            this.cmbLeft.Size = new System.Drawing.Size(157, 21);
            this.cmbLeft.TabIndex = 27;
            this.cmbLeft.SelectedIndexChanged += new System.EventHandler(this.cmbLeft_SelectedIndexChanged);
            this.cmbLeft.Leave += new System.EventHandler(this.cmbLeft_Leave);
            // 
            // btnColorR
            // 
            this.btnColorR.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnColorR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColorR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColorR.Location = new System.Drawing.Point(9, 79);
            this.btnColorR.Name = "btnColorR";
            this.btnColorR.Size = new System.Drawing.Size(29, 19);
            this.btnColorR.TabIndex = 26;
            this.btnColorR.Text = "R";
            this.btnColorR.UseVisualStyleBackColor = false;
            this.btnColorR.BackColorChanged += new System.EventHandler(this.btnColorR_BackColorChanged);
            this.btnColorR.Click += new System.EventHandler(this.btnColorR_Click);
            // 
            // btnColorL
            // 
            this.btnColorL.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnColorL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColorL.Location = new System.Drawing.Point(9, 54);
            this.btnColorL.Name = "btnColorL";
            this.btnColorL.Size = new System.Drawing.Size(29, 19);
            this.btnColorL.TabIndex = 25;
            this.btnColorL.Text = "L";
            this.btnColorL.UseVisualStyleBackColor = false;
            this.btnColorL.BackColorChanged += new System.EventHandler(this.btnColorL_BackColorChanged);
            this.btnColorL.Click += new System.EventHandler(this.btnColorL_Click);
            // 
            // ckbBothSide
            // 
            this.ckbBothSide.AutoSize = true;
            this.ckbBothSide.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbBothSide.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ckbBothSide.Location = new System.Drawing.Point(96, 36);
            this.ckbBothSide.Name = "ckbBothSide";
            this.ckbBothSide.Size = new System.Drawing.Size(103, 16);
            this.ckbBothSide.TabIndex = 24;
            this.ckbBothSide.Text = "Same On Both Side";
            this.ckbBothSide.UseVisualStyleBackColor = true;
            this.ckbBothSide.CheckedChanged += new System.EventHandler(this.ckbBothSide_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Restriction Type:";
            // 
            // cmbLanes
            // 
            this.cmbLanes.FormattingEnabled = true;
            this.cmbLanes.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
            this.cmbLanes.Location = new System.Drawing.Point(69, 13);
            this.cmbLanes.Name = "cmbLanes";
            this.cmbLanes.Size = new System.Drawing.Size(107, 21);
            this.cmbLanes.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "lanes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Road Width";
            // 
            // lblNoOwnerNameParcelNum
            // 
            this.lblNoOwnerNameParcelNum.AutoSize = true;
            this.lblNoOwnerNameParcelNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOwnerNameParcelNum.ForeColor = System.Drawing.Color.Red;
            this.lblNoOwnerNameParcelNum.Location = new System.Drawing.Point(255, 457);
            this.lblNoOwnerNameParcelNum.Name = "lblNoOwnerNameParcelNum";
            this.lblNoOwnerNameParcelNum.Size = new System.Drawing.Size(0, 13);
            this.lblNoOwnerNameParcelNum.TabIndex = 27;
            // 
            // lblAdjacentParcelNum
            // 
            this.lblAdjacentParcelNum.AutoSize = true;
            this.lblAdjacentParcelNum.Location = new System.Drawing.Point(204, 434);
            this.lblAdjacentParcelNum.Name = "lblAdjacentParcelNum";
            this.lblAdjacentParcelNum.Size = new System.Drawing.Size(0, 13);
            this.lblAdjacentParcelNum.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 430);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(172, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Total number of adjacent parcels is";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 457);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Number of parcels that have no owner name is";
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.ForeColor = System.Drawing.Color.Red;
            this.lbl_info.Location = new System.Drawing.Point(389, 430);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(41, 13);
            this.lbl_info.TabIndex = 28;
            this.lbl_info.Text = "Notice:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dataGridView2);
            this.groupBox7.Controls.Add(this.listView1);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Location = new System.Drawing.Point(13, 176);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(599, 116);
            this.groupBox7.TabIndex = 29;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Parcels Covered By Buffer Area";
            // 
            // TRWindow
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 487);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lblNoOwnerNameParcelNum);
            this.Controls.Add(this.lblAdjacentParcelNum);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TRWindow";
            this.Text = "Traffic Restriction Notification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TRWindow_FormClosing);
            this.Load += new System.EventHandler(this.TRWindow_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TRWindow_MouseClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtParcel;
        private System.Windows.Forms.ComboBox cmbSearchBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbRight;
        private System.Windows.Forms.ComboBox cmbLeft;
        private System.Windows.Forms.Button btnColorR;
        private System.Windows.Forms.Button btnColorL;
        private System.Windows.Forms.CheckBox ckbBothSide;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLanes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbBufferDistance;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbLayoutScale;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Button btnRestrictedStreet;
        private System.Windows.Forms.Label lblNoOwnerNameParcelNum;
        private System.Windows.Forms.Label lblAdjacentParcelNum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnMap;
    }
}