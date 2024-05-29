using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWLDotNetCore.Shared;
using ZWLDotNetCore.WinFormsApp.Models;
using ZWLDotNetCore.WinFormsApp.Queries;

namespace ZWLDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlogList()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }
        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvData.DataSource = lst;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var columnIndex = e.ColumnIndex;
            //var colRow = e.RowIndex;
            if (e.RowIndex == -1) return;
            int blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);
            //#region If Else
            //if (e.ColumnIndex == (int)EnumFormControlType.Edit) 
            //{
            //    FrmBlog frmBlog = new FrmBlog(blogId);
            //    frmBlog.ShowDialog();
            //    BlogList();
            //}
            //else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            //{
            //    var dialogResult = MessageBox.Show("Are you sure want t delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult != DialogResult.Yes) return;

            //    DeleteBlog(blogId);
            //    BlogList();
            //}
            //#endregion

            #region Switch Case
            int index =e.ColumnIndex;
            EnumFormControlType enumFormControlType = (EnumFormControlType)index;
            switch (enumFormControlType)
            {
                case EnumFormControlType.Edit:
                    FrmBlog frmBlog = new FrmBlog(blogId);
                    frmBlog.ShowDialog();
                    BlogList();
                    break; 
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Are you sure want t delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes) return;

                    DeleteBlog(blogId);
                    BlogList();
                    break; 
                case EnumFormControlType.None:
                default:
                    MessageBox.Show("");
                    break;
            }
            #endregion
        }

        private void DeleteBlog(int id)
        {
            string query = @"delete from Tbl_Blog where BlogId=@BlogId";
            int result = _dapperService.Execute(query, new { BlogId = @id });
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            MessageBox.Show(message);
        }
    }
}
