using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ToDoApp
{
    public partial class DBConnect : Form
    {

        //Tạo 2 biến cụ bộ 
        //@ : thể hiện đây là 1 chuỗi
        string strCon = @"Data Source=agilet-2024-008\SQLSERVER;Initial Catalog=Test;Integrated Security=True;Trust Server Certificate=True";

        //Đối tượng kết nối 
        SqlConnection sqlcon = null;
        
        public DBConnect()
        {
            InitializeComponent();
        }

        private void btnOpenConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon == null)
                { 
                //Truyền vào đối tượng kết nối chuỗi kết nối 
                    sqlcon = new SqlConnection(strCon);　　//Rỗng thì tạo mới 
                }
                //Mở kết nối 
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                    MessageBox.Show("成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCloseConnection_Click(object sender, EventArgs e)
        {
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
                MessageBox.Show("Đóng kết nối thành công ");
            }
            else
            {
                MessageBox.Show("Chưa tạo kêts nối");
            }
        }
    }
}
