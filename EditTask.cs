﻿
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ToDoApp
{


    public partial class EditTask : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ToDoAppConnectionString"].ConnectionString;
        private string taskId;
        private DateTime addDate;

        
        public EditTask(string taskId)
        {
            InitializeComponent();
            this.taskId = taskId;
            LoadTaskDetails();
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

        private void LoadTaskDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE taskId = @taskId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@taskId", taskId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtTitle.Text = reader["Title"].ToString();
                    txtContent.Text = reader["Content"].ToString();
                    addDate = (DateTime)reader["AddDate"];
                    deadlinePicker.Value = (DateTime)reader["Deadline"];
                    string priority = reader["Priority"].ToString();

                    if (priority == "高")
                    {
                        radioButtonHigh.Checked = true;
                    }
                    else if (priority == "中")
                    {
                        radioButtonMedium.Checked = true;
                    }
                    else if (priority == "低")
                    {
                        radioButtonLow.Checked = true;
                    }
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtContent.Text;
            DateTime deadline = deadlinePicker.Value;
            string priority = GetPriority();
            DateTime updateDate = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Tasks
                    SET Title = @Title, Content = @Content, Deadline = @Deadline, Priority = @Priority, UpdateDate = @UpdateDate
                    WHERE taskId = @taskId;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@taskId", taskId);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Content", content);
                command.Parameters.AddWithValue("@Deadline", deadline);
                command.Parameters.AddWithValue("@Priority", priority);
                command.Parameters.AddWithValue("@UpdateDate", updateDate);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("タスクが更新されました。");
                        TaskDetails taskDetailsForm = new TaskDetails(taskId, title, content, addDate, deadline, priority, updateDate);
                        taskDetailsForm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("タスクの更新に失敗しました。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー: " + ex.Message);
                }
            }
        }

        private string GetPriority()
        {
            if (radioButtonHigh.Checked)
                return radioButtonHigh.Text;
            else if (radioButtonMedium.Checked)
                return radioButtonMedium.Text;
            else if (radioButtonLow.Checked)
                return radioButtonLow.Text;
            else
                return "";
        }
    }
}
