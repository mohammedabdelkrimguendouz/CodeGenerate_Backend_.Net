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
    public partial class frmAdditionalDetailsForTableSelected : Form
    {
        public delegate void DataBackEventHandler(object sender, string ClassName,string PrimaryKey);

        public event DataBackEventHandler DataBack;

        private string _TableName = "";

        private void _FillColumnsInComboBox()
        {
            DataTable dt = clsCodeGeneratore.GetColumnsInfo(_TableName);
            if (dt == null)
                return;

            if (cbPrimaryKey.Items.Count > 0)
                cbPrimaryKey.Items.Clear();
            
            foreach (DataRow Row in dt.Rows)
            {
                cbPrimaryKey.Items.Add(Row["COLUMN_NAME"]);
            }

            if (cbPrimaryKey.Items.Count > 0)
                cbPrimaryKey.SelectedIndex = 0;
        }

        public frmAdditionalDetailsForTableSelected(string TableName)
        {
            InitializeComponent();
            _TableName=TableName;
        }
       

        private void frmAdditionalDetailsForTableSelected_Load(object sender, EventArgs e)
        {
            lblTableName.Text = _TableName;
            txtClassName.Text = "";
            _FillColumnsInComboBox();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtClassName.Text.Trim()=="")
            {
                MessageBox.Show("Please Write Class Name ","Faild Not Completed ",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            DataBack?.Invoke(this,txtClassName.Text.Trim(),cbPrimaryKey.Text.Trim());
            this.Close();
        }
    }
}
