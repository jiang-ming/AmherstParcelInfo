namespace AmherstParcelInfo
{
    partial class SearchWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.txtParcel = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUnselect = new System.Windows.Forms.Button();
            this.ckbMapClickSelect = new System.Windows.Forms.CheckBox();
            this.btnLSelect = new System.Windows.Forms.Button();
            this.ckb_MultiSel = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLayoutScale = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBufferDistance = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.btnPortion = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAdjacentParcelNum = new System.Windows.Forms.Label();
            this.lblNoOwnerNameParcelNum = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnMap = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search By:";
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
            this.cmbSearchBy.Location = new System.Drawing.Point(74, 7);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(98, 21);
            this.cmbSearchBy.TabIndex = 3;
            // 
            // txtParcel
            // 
            this.txtParcel.Location = new System.Drawing.Point(179, 7);
            this.txtParcel.Name = "txtParcel";
            this.txtParcel.Size = new System.Drawing.Size(255, 20);
            this.txtParcel.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUnselect);
            this.groupBox1.Controls.Add(this.ckbMapClickSelect);
            this.groupBox1.Controls.Add(this.btnLSelect);
            this.groupBox1.Controls.Add(this.ckb_MultiSel);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 47);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parcel Selection";
            // 
            // btnUnselect
            // 
            this.btnUnselect.Image = global::AmherstParcelInfo.Properties.Resources.SelectionClearSelected_small16;
            this.btnUnselect.Location = new System.Drawing.Point(518, 13);
            this.btnUnselect.Name = "btnUnselect";
            this.btnUnselect.Size = new System.Drawing.Size(75, 23);
            this.btnUnselect.TabIndex = 7;
            this.btnUnselect.Text = "Reset";
            this.btnUnselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUnselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnselect.UseVisualStyleBackColor = true;
            this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
            // 
            // ckbMapClickSelect
            // 
            this.ckbMapClickSelect.AutoSize = true;
            this.ckbMapClickSelect.Location = new System.Drawing.Point(141, 19);
            this.ckbMapClickSelect.Name = "ckbMapClickSelect";
            this.ckbMapClickSelect.Size = new System.Drawing.Size(113, 17);
            this.ckbMapClickSelect.TabIndex = 1;
            this.ckbMapClickSelect.Text = "Enable map select";
            this.ckbMapClickSelect.UseVisualStyleBackColor = true;
            this.ckbMapClickSelect.CheckedChanged += new System.EventHandler(this.ckbMapClickSelect_CheckedChanged);
            this.ckbMapClickSelect.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.ckbMapClickSelect_ChangeUICues);
            // 
            // btnLSelect
            // 
            this.btnLSelect.Enabled = false;
            this.btnLSelect.Image = global::AmherstParcelInfo.Properties.Resources.SelectionSelectLineTool16;
            this.btnLSelect.Location = new System.Drawing.Point(437, 13);
            this.btnLSelect.Name = "btnLSelect";
            this.btnLSelect.Size = new System.Drawing.Size(75, 23);
            this.btnLSelect.TabIndex = 6;
            this.btnLSelect.Text = "L-Select";
            this.btnLSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLSelect.UseVisualStyleBackColor = true;
            this.btnLSelect.Click += new System.EventHandler(this.btnLSelect_Click);
            // 
            // ckb_MultiSel
            // 
            this.ckb_MultiSel.AutoSize = true;
            this.ckb_MultiSel.Location = new System.Drawing.Point(7, 20);
            this.ckb_MultiSel.Name = "ckb_MultiSel";
            this.ckb_MultiSel.Size = new System.Drawing.Size(128, 17);
            this.ckb_MultiSel.TabIndex = 0;
            this.ckb_MultiSel.Text = "Enable multi-selection";
            this.ckb_MultiSel.UseVisualStyleBackColor = true;
            this.ckb_MultiSel.CheckedChanged += new System.EventHandler(this.ckb_MultiSel_CheckedChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LemonChiffon;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(393, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Parcel Search Result:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(6, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Adjacent Parcels Without Owner Name:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.ForestGreen;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(12, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(393, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Selected Target Parcel:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(393, 157);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Location = new System.Drawing.Point(6, 32);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 25;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(187, 138);
            this.dataGridView2.TabIndex = 11;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToOrderColumns = true;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView3.Location = new System.Drawing.Point(12, 280);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 25;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(393, 93);
            this.dataGridView3.TabIndex = 12;
            this.dataGridView3.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellDoubleClick);
            // 
            // listView1
            // 
            this.listView1.ForeColor = System.Drawing.Color.Crimson;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(6, 189);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(187, 87);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "1 inch =";
            // 
            // cmbLayoutScale
            // 
            this.cmbLayoutScale.FormattingEnabled = true;
            this.cmbLayoutScale.Location = new System.Drawing.Point(63, 16);
            this.cmbLayoutScale.Name = "cmbLayoutScale";
            this.cmbLayoutScale.Size = new System.Drawing.Size(48, 21);
            this.cmbLayoutScale.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "feet";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbLayoutScale);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(15, 375);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(146, 44);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Layout Scale";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.cmbBufferDistance);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(192, 375);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(165, 44);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buffer Distance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(125, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "feet";
            // 
            // cmbBufferDistance
            // 
            this.cmbBufferDistance.FormattingEnabled = true;
            this.cmbBufferDistance.Location = new System.Drawing.Point(76, 16);
            this.cmbBufferDistance.Name = "cmbBufferDistance";
            this.cmbBufferDistance.Size = new System.Drawing.Size(48, 21);
            this.cmbBufferDistance.TabIndex = 1;
            this.cmbBufferDistance.SelectedIndexChanged += new System.EventHandler(this.cmbBufferDistance_SelectedIndexChanged_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 0;
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.ForeColor = System.Drawing.Color.Red;
            this.lbl_info.Location = new System.Drawing.Point(417, 376);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(41, 13);
            this.lbl_info.TabIndex = 17;
            this.lbl_info.Text = "Notice:";
            // 
            // btnPortion
            // 
            this.btnPortion.Location = new System.Drawing.Point(529, 434);
            this.btnPortion.Name = "btnPortion";
            this.btnPortion.Size = new System.Drawing.Size(75, 23);
            this.btnPortion.TabIndex = 18;
            this.btnPortion.Text = "Portion";
            this.btnPortion.UseVisualStyleBackColor = true;
            this.btnPortion.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 453);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Number of parcels that have no owner name is";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 429);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(172, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Total number of adjacent parcels is";
            // 
            // lblAdjacentParcelNum
            // 
            this.lblAdjacentParcelNum.AutoSize = true;
            this.lblAdjacentParcelNum.Location = new System.Drawing.Point(198, 433);
            this.lblAdjacentParcelNum.Name = "lblAdjacentParcelNum";
            this.lblAdjacentParcelNum.Size = new System.Drawing.Size(0, 13);
            this.lblAdjacentParcelNum.TabIndex = 22;
            // 
            // lblNoOwnerNameParcelNum
            // 
            this.lblNoOwnerNameParcelNum.AutoSize = true;
            this.lblNoOwnerNameParcelNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOwnerNameParcelNum.ForeColor = System.Drawing.Color.Red;
            this.lblNoOwnerNameParcelNum.Location = new System.Drawing.Point(249, 453);
            this.lblNoOwnerNameParcelNum.Name = "lblNoOwnerNameParcelNum";
            this.lblNoOwnerNameParcelNum.Size = new System.Drawing.Size(0, 13);
            this.lblNoOwnerNameParcelNum.TabIndex = 23;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.dataGridView2);
            this.groupBox5.Controls.Add(this.listView1);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(411, 91);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 282);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Parcels Covered By Buffer Area";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Adjacent Parcels With Owner Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::AmherstParcelInfo.Properties.Resources.GenericSearch16;
            this.btnSearch.Location = new System.Drawing.Point(449, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnMap
            // 
            this.btnMap.Image = global::AmherstParcelInfo.Properties.Resources.Map16;
            this.btnMap.Location = new System.Drawing.Point(411, 424);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(94, 42);
            this.btnMap.TabIndex = 25;
            this.btnMap.Text = "Create Map";
            this.btnMap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnTable
            // 
            this.btnTable.Image = global::AmherstParcelInfo.Properties.Resources.TableDBase16;
            this.btnTable.Location = new System.Drawing.Point(517, 424);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(94, 42);
            this.btnTable.TabIndex = 19;
            this.btnTable.Text = "Export Table";
            this.btnTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Image = global::AmherstParcelInfo.Properties.Resources.SelectionClearSelection_B_16;
            this.btn_Clear.Location = new System.Drawing.Point(529, 7);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(74, 23);
            this.btn_Clear.TabIndex = 1;
            this.btn_Clear.Text = "Clear All";
            this.btn_Clear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // SearchWindow
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 483);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.lblNoOwnerNameParcelNum);
            this.Controls.Add(this.lblAdjacentParcelNum);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnPortion);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtParcel);
            this.Controls.Add(this.cmbSearchBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btnSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchWindow";
            this.Text = "Parcel Notification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchWindow_FormClosing);
            this.Load += new System.EventHandler(this.SearchWindow_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SearchWindow_MouseClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSearchBy;
        private System.Windows.Forms.TextBox txtParcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUnselect;
        private System.Windows.Forms.CheckBox ckbMapClickSelect;
        private System.Windows.Forms.Button btnLSelect;
        private System.Windows.Forms.CheckBox ckb_MultiSel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLayoutScale;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBufferDistance;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Button btnPortion;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAdjacentParcelNum;
        private System.Windows.Forms.Label lblNoOwnerNameParcelNum;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMap;

    }
}