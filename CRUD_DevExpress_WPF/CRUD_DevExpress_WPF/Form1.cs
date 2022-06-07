using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VBSQLHelper;

namespace CRUD_DevExpress_WPF
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var data_gender = SQLHelper.ExecQueryDataAsDataTable("select * from tbl_gender");
            cb_gender.Properties.DataSource = data_gender;
            cb_gender.Properties.ValueMember = "id";
            cb_gender.Properties.DisplayMember = "name";

            LoadDataToGridview();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var id = text_id.Text;
            var firstname = text_firstname.Text;
            var lastname = text_lastname.Text;
            var address = text_address.Text;
            var age = Convert.ToInt32(spin_age.EditValue);
            var gender = Convert.ToBoolean(cb_gender.EditValue);

            if (string.IsNullOrEmpty(id))
            {
                XtraMessageBox.Show("Enter your id", "Information");
                text_id.Focus();
                return;
            }

            //check id is existe

            var isExist = Convert.ToInt32(SQLHelper.ExecQuerySacalar($"select count(*) from tbl_student where id='{id}'")) > 0;
            if (isExist)
            {
                XtraMessageBox.Show($"Your id {id} is existed!", "Information");
                text_id.Focus();
                text_id.SelectAll();
                return;
            }

            if (string.IsNullOrEmpty(firstname))
            {
                XtraMessageBox.Show("Enter your FirstName", "Information");
                text_firstname.Focus();
                return;
            }
            if (string.IsNullOrEmpty(lastname))
            {
                XtraMessageBox.Show("Enter your LastName", "Information");
                text_lastname.Focus();
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                XtraMessageBox.Show("Enter your Address", "Information");
                text_address.Focus();
                return;
            }

            if (age < 1)
            {
                XtraMessageBox.Show("Enter your Age", "Information");
                spin_age.Focus();
                return;
            }

            if (cb_gender.EditValue == null)
            {
                cb_gender.ShowPopup();
                return;
            }
            var student = new Student
            {
                id = Convert.ToInt32(id),
                firstname = firstname,
                lastname = lastname,
                address = address,
                age = age,
                gender = gender
            };
            var affectrow = SQLHelper.Insert(student);
            if (affectrow > 0)
            {
                text_id.Text = "";
                text_firstname.Text = "";
                text_lastname.Text = "";
                text_address.Text = "";
                spin_age.EditValue = 0;
                cb_gender.EditValue = null;
                LoadDataToGridview();
            }
            else
            {
                XtraMessageBox.Show("Insert Student Fail", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void LoadDataToGridview()
        {
            var dataStudents = SQLHelper.ExecQueryData<Student>("select * from tbl_student");
            gridControl1.DataSource = dataStudents;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var id = text_id.Text;
            var firstname = text_firstname.Text;
            var lastname = text_lastname.Text;
            var address = text_address.Text;
            var age = Convert.ToInt32(spin_age.EditValue);
            var gender = Convert.ToBoolean(cb_gender.EditValue);
            var studentEdit = new Student
            {
                id = Convert.ToInt32(id),
                firstname = firstname,
                lastname = lastname,
                address = address,
                age = age,
                gender = gender
            };

            SQLHelper.Update(studentEdit);
            LoadDataToGridview();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var gridview = sender as GridView;
            if (gridview.IsDataRow(e.FocusedRowHandle))
            {
                var student = gridview.GetFocusedRow() as Student;
                text_id.EditValue = student.id;
                text_firstname.EditValue = student.firstname;
                text_lastname.EditValue = student.lastname;
                text_address.EditValue = student.address;
                spin_age.EditValue = student.age;
                cb_gender.EditValue = student.gender;
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column == colDelete)
            {
                var studentDelete = gridView1.GetFocusedRow() as Student;
                var dlg = XtraMessageBox.Show($"Are you sure Delete Student {studentDelete.firstname} {studentDelete.lastname}?", "Infomation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    SQLHelper.Delete(studentDelete);
                    gridView1.DeleteSelectedRows();
                }
            }
        }
    }
}
