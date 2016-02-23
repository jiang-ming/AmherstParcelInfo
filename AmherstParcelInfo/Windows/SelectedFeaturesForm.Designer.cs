namespace AmherstParcelInfo
{
    partial class SelectedFeaturesForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_dbfExport = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.lbl_selectedNo = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(347, 264);
            this.dataGridView1.TabIndex = 0;
            // 
            // btn_dbfExport
            // 
            this.btn_dbfExport.Image = global::AmherstParcelInfo.Properties.Resources.MetadataExport16;
            this.btn_dbfExport.Location = new System.Drawing.Point(235, 271);
            this.btn_dbfExport.Name = "btn_dbfExport";
            this.btn_dbfExport.Size = new System.Drawing.Size(105, 25);
            this.btn_dbfExport.TabIndex = 1;
            this.btn_dbfExport.Text = "Export to DBF";
            this.btn_dbfExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_dbfExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_dbfExport.UseVisualStyleBackColor = true;
            this.btn_dbfExport.Click += new System.EventHandler(this.btn_dbfExport_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Image = global::AmherstParcelInfo.Properties.Resources.SelectionClearSelected_small16;
            this.btn_Clear.Location = new System.Drawing.Point(235, 297);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(105, 25);
            this.btn_Clear.TabIndex = 2;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // lbl_selectedNo
            // 
            this.lbl_selectedNo.AutoSize = true;
            this.lbl_selectedNo.Location = new System.Drawing.Point(12, 309);
            this.lbl_selectedNo.Name = "lbl_selectedNo";
            this.lbl_selectedNo.Size = new System.Drawing.Size(0, 13);
            this.lbl_selectedNo.TabIndex = 3;
            // 
            // SelectedFeaturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 333);
            this.Controls.Add(this.lbl_selectedNo);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_dbfExport);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectedFeaturesForm";
            this.Text = "SelectedFeaturesForm";
            this.Load += new System.EventHandler(this.SelectedFeaturesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_dbfExport;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label lbl_selectedNo;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}