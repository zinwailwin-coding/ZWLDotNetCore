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
            List<BlogModel> lst= _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvData.DataSource=lst;
        }
    }
}
