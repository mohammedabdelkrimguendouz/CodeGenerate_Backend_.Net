namespace CodeGeneratore
{
    partial class frmShowCodeGenerated
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopyBusiness = new System.Windows.Forms.Button();
            this.btnCopyDataAccess = new System.Windows.Forms.Button();
            this.tbDataAccessLayer = new System.Windows.Forms.TabPage();
            this.txtDataAccessLayer = new System.Windows.Forms.TextBox();
            this.tbBusinesLayer = new System.Windows.Forms.TabPage();
            this.txtBusinessLayer = new System.Windows.Forms.TextBox();
            this.tcCodeGenerated = new System.Windows.Forms.TabControl();
            this.btnStoredProcedure = new System.Windows.Forms.Button();
            this.tpStoredProcedure = new System.Windows.Forms.TabPage();
            this.txtStoredProcedure = new System.Windows.Forms.TextBox();
            this.tbDataAccessLayer.SuspendLayout();
            this.tbBusinesLayer.SuspendLayout();
            this.tcCodeGenerated.SuspendLayout();
            this.tpStoredProcedure.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(35, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(216, 41);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopyBusiness
            // 
            this.btnCopyBusiness.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCopyBusiness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyBusiness.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyBusiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyBusiness.Location = new System.Drawing.Point(257, 477);
            this.btnCopyBusiness.Name = "btnCopyBusiness";
            this.btnCopyBusiness.Size = new System.Drawing.Size(216, 41);
            this.btnCopyBusiness.TabIndex = 0;
            this.btnCopyBusiness.Text = "Copy Business";
            this.btnCopyBusiness.UseVisualStyleBackColor = false;
            this.btnCopyBusiness.Click += new System.EventHandler(this.btnCopyBusiness_Click);
            // 
            // btnCopyDataAccess
            // 
            this.btnCopyDataAccess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCopyDataAccess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyDataAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyDataAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyDataAccess.Location = new System.Drawing.Point(479, 477);
            this.btnCopyDataAccess.Name = "btnCopyDataAccess";
            this.btnCopyDataAccess.Size = new System.Drawing.Size(216, 41);
            this.btnCopyDataAccess.TabIndex = 11;
            this.btnCopyDataAccess.Text = "Copy Data Access";
            this.btnCopyDataAccess.UseVisualStyleBackColor = false;
            this.btnCopyDataAccess.Click += new System.EventHandler(this.btnCopyDataAccess_Click);
            // 
            // tbDataAccessLayer
            // 
            this.tbDataAccessLayer.Controls.Add(this.txtDataAccessLayer);
            this.tbDataAccessLayer.Location = new System.Drawing.Point(4, 25);
            this.tbDataAccessLayer.Name = "tbDataAccessLayer";
            this.tbDataAccessLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tbDataAccessLayer.Size = new System.Drawing.Size(967, 442);
            this.tbDataAccessLayer.TabIndex = 1;
            this.tbDataAccessLayer.Text = "Data Access Layer";
            this.tbDataAccessLayer.UseVisualStyleBackColor = true;
            // 
            // txtDataAccessLayer
            // 
            this.txtDataAccessLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDataAccessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataAccessLayer.Location = new System.Drawing.Point(3, 3);
            this.txtDataAccessLayer.Multiline = true;
            this.txtDataAccessLayer.Name = "txtDataAccessLayer";
            this.txtDataAccessLayer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDataAccessLayer.Size = new System.Drawing.Size(961, 436);
            this.txtDataAccessLayer.TabIndex = 1;
            // 
            // tbBusinesLayer
            // 
            this.tbBusinesLayer.Controls.Add(this.txtBusinessLayer);
            this.tbBusinesLayer.Location = new System.Drawing.Point(4, 25);
            this.tbBusinesLayer.Name = "tbBusinesLayer";
            this.tbBusinesLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tbBusinesLayer.Size = new System.Drawing.Size(967, 442);
            this.tbBusinesLayer.TabIndex = 0;
            this.tbBusinesLayer.Text = "Busines Layer";
            this.tbBusinesLayer.UseVisualStyleBackColor = true;
            // 
            // txtBusinessLayer
            // 
            this.txtBusinessLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBusinessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusinessLayer.Location = new System.Drawing.Point(3, 3);
            this.txtBusinessLayer.Multiline = true;
            this.txtBusinessLayer.Name = "txtBusinessLayer";
            this.txtBusinessLayer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBusinessLayer.Size = new System.Drawing.Size(961, 436);
            this.txtBusinessLayer.TabIndex = 0;
            // 
            // tcCodeGenerated
            // 
            this.tcCodeGenerated.Controls.Add(this.tbBusinesLayer);
            this.tcCodeGenerated.Controls.Add(this.tbDataAccessLayer);
            this.tcCodeGenerated.Controls.Add(this.tpStoredProcedure);
            this.tcCodeGenerated.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcCodeGenerated.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcCodeGenerated.Location = new System.Drawing.Point(0, 0);
            this.tcCodeGenerated.Name = "tcCodeGenerated";
            this.tcCodeGenerated.SelectedIndex = 0;
            this.tcCodeGenerated.Size = new System.Drawing.Size(975, 471);
            this.tcCodeGenerated.TabIndex = 9;
            // 
            // btnStoredProcedure
            // 
            this.btnStoredProcedure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnStoredProcedure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStoredProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStoredProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoredProcedure.Location = new System.Drawing.Point(704, 477);
            this.btnStoredProcedure.Name = "btnStoredProcedure";
            this.btnStoredProcedure.Size = new System.Drawing.Size(216, 41);
            this.btnStoredProcedure.TabIndex = 12;
            this.btnStoredProcedure.Text = "Copy Stored Procedure";
            this.btnStoredProcedure.UseVisualStyleBackColor = false;
            this.btnStoredProcedure.Click += new System.EventHandler(this.btnStoredProcedure_Click);
            // 
            // tpStoredProcedure
            // 
            this.tpStoredProcedure.Controls.Add(this.txtStoredProcedure);
            this.tpStoredProcedure.Location = new System.Drawing.Point(4, 25);
            this.tpStoredProcedure.Name = "tpStoredProcedure";
            this.tpStoredProcedure.Padding = new System.Windows.Forms.Padding(3);
            this.tpStoredProcedure.Size = new System.Drawing.Size(967, 442);
            this.tpStoredProcedure.TabIndex = 2;
            this.tpStoredProcedure.Text = "Stored Procedure";
            this.tpStoredProcedure.UseVisualStyleBackColor = true;
            // 
            // txtStoredProcedure
            // 
            this.txtStoredProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStoredProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStoredProcedure.Location = new System.Drawing.Point(3, 3);
            this.txtStoredProcedure.Multiline = true;
            this.txtStoredProcedure.Name = "txtStoredProcedure";
            this.txtStoredProcedure.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStoredProcedure.Size = new System.Drawing.Size(961, 436);
            this.txtStoredProcedure.TabIndex = 2;
            // 
            // frmShowCodeGenerated
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(975, 530);
            this.Controls.Add(this.btnStoredProcedure);
            this.Controls.Add(this.btnCopyDataAccess);
            this.Controls.Add(this.tcCodeGenerated);
            this.Controls.Add(this.btnCopyBusiness);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowCodeGenerated";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Code Generate";
            this.Load += new System.EventHandler(this.frmShowCodeGenerate_Load);
            this.tbDataAccessLayer.ResumeLayout(false);
            this.tbDataAccessLayer.PerformLayout();
            this.tbBusinesLayer.ResumeLayout(false);
            this.tbBusinesLayer.PerformLayout();
            this.tcCodeGenerated.ResumeLayout(false);
            this.tpStoredProcedure.ResumeLayout(false);
            this.tpStoredProcedure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopyBusiness;
        private System.Windows.Forms.Button btnCopyDataAccess;
        private System.Windows.Forms.TabPage tbDataAccessLayer;
        private System.Windows.Forms.TextBox txtDataAccessLayer;
        private System.Windows.Forms.TabPage tbBusinesLayer;
        private System.Windows.Forms.TextBox txtBusinessLayer;
        private System.Windows.Forms.TabControl tcCodeGenerated;
        private System.Windows.Forms.Button btnStoredProcedure;
        private System.Windows.Forms.TabPage tpStoredProcedure;
        private System.Windows.Forms.TextBox txtStoredProcedure;
    }
}