using CodeGeneratore_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneratore
{
    public partial class Form1 : Form
    {
        private bool _IsAddAdditionalDetails= false;
        public Form1()
        {
            InitializeComponent();
        }

        private void _FillTablesInComboBox()
        {
            DataTable dt = clsCodeGeneratore.GetAllTables();
            if (dt == null)
                return;


            if (cbTables.Items.Count > 0)
                  cbTables.Items.Clear();

            
            foreach (DataRow Row in dt.Rows)
            {
                cbTables.Items.Add(Row["TABLE_NAME"]);
            }

            if(cbTables.Items.Count>0)
                 cbTables.SelectedIndex = 0;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            

            if (!clsCodeGeneratore.SetConnection(txtServer.Text.Trim(), txtDataBase.Text.Trim(),
                txtUserId.Text.Trim(), txtPassword.Text.Trim()))
            {
                gbGenerateCode.Enabled = false;
                btnCopyDataAccessSettings.Enabled = false;
                MessageBox.Show("Error Connection -( ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            gbGenerateCode.Enabled = true;
            btnCopyDataAccessSettings.Enabled = true;
            _FillTablesInComboBox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGenrate_Click(object sender, EventArgs e)
        {
            clsCodeGeneratore.SelectedTableName = cbTables.Text;
            frmAdditionalDetailsForTableSelected frm1=new frmAdditionalDetailsForTableSelected(cbTables.Text);
            frm1.DataBack += DataBackEvent;
            frm1.ShowDialog();

            if (!_IsAddAdditionalDetails)
                return;

            string BusinessLayer   = clsCodeGeneratore.GenerateBusinessLayer();
            string DataAccessLayer = clsCodeGeneratore.GenerateDataAccessLayer();
            string StoredProcedure = clsCodeGeneratore.GenerateStoredProcedure();

            frmShowCodeGenerated frm2 = new frmShowCodeGenerated(BusinessLayer, DataAccessLayer, StoredProcedure);
            frm2.ShowDialog();
           
        }

        private void DataBackEvent(object sender, string ClassName,string PrimaryKey)
        {
            clsCodeGeneratore.ClassName = ClassName;
            clsCodeGeneratore.IDName = PrimaryKey;
            _IsAddAdditionalDetails = (ClassName != "" && PrimaryKey != "");
        }

        private void _CopyToClipboard(string Text)
        {
            Clipboard.SetText(Text);
            if (Clipboard.ContainsText())
                MessageBox.Show("Text Copied Successfully -) ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed To Copy Text -( ", "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCopyDataAccessSettings_Click(object sender, EventArgs e)
        {
            _CopyToClipboard(clsCodeGeneratore.GenerateDataAccessSettings());
        }

        private void btnCopyEventLog_Click(object sender, EventArgs e)
        {
            _CopyToClipboard(clsCodeGeneratore.GenerateEventLog());
        }
    }
}
