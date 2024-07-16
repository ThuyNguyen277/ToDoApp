using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoApp;
using System.Configuration;
using System.Data.SqlClient;

namespace ToDoApp
{
    public partial class AddTaskScreen : Form
    {
        private string connecitonString = ConfigurationMannager.ConnectionStrings["ToDoAppConnectionString"].ConnectionString;
        public AddTaskScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 「ｘ」ピクチャーがクリックされた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();     //アプリケーションを終了する
        }

        /// <summary>
        /// 「ー」ピクチャーがクリックされた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;        //ウィンドウの状態が最小化に設定する
        }

        /// <summary>
        /// 最大化というピクチャーがクリックされた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picMax_Click(object sender, EventArgs e)
        {
            //ウィンドウの状況が最大化されている場合
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;      //ウインドウが通常サイズに戻す
            }
            //ウインドウの状況が通常の場合
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;    //ウインドウを最大化する
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            //入力された情報を取得
            string title = txtTitle.Text;
            string content = txtContent.Text;
            DateTime adddate = addDate.Value;
            DateTime deadline = deadLine.Value;
            string priority = "";

            if (radioButtonHigh.Checked)
            {
                priority = radioButtonHigh.Text;
            }
            else if (radioButtonMedium.Checked)
            {
                priority = radioButtonMedium.Text;
            }
            else if (radioButtonLow.Checked)
            {
                priority = radioButtonLow.Text;
            }

            using (SqlConnection connection = new SqlConnection(connecitonString))
            {
                string query = @"
                    INSERT INTO Tasks (Title, Content, AddDate, Deadline, Priority)
                    VALUES (@Title, @Content, @AddDate, @Deadline, @Priority);";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Content", content);
                command.Parameters.AddWithValue("@AddDate", addDate);
                command.Parameters.AddWithValue("@Deadline", (object)deadline ?? DBNull.Value);
                command.Parameters.AddWithValue("@Priority", priority);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("タスクが追加されました。");
                    }
                    else
                    {
                        MessageBox.Show("タスクの追加に失敗しました。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー: " + ex.Message);
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Hide();
        }
    }
}
