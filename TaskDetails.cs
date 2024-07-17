using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{

    // タスクの詳細情報を格納するカスタムクラス

    
    public partial class TaskDetails : Form
    {
        private string taskID;
        private string taskTitle;
        private string taskContent;
        private DateTime addDate;
        private DateTime deadline;
        private string priority;

        private string connectionString = ConfigurationManager.ConnectionStrings["ToDoAppConnectionString"].ConnectionString;


        public TaskDetails(string taskId, string title, string content, DateTime adddate, DateTime deadline, string priority, DateTime? updateDate = null)
        {
            InitializeComponent();

            this.taskID = taskId;
            this.taskTitle = title;
            this.taskContent = content;
            this.addDate = adddate;
            this.deadline = deadline;
            this.priority = priority;

            DisplayTaskDetails(updateDate);


            
        }

        private void DisplayTaskDetails(DateTime? updateDate)
        {
            //ラベルやテキストボックスに情報を表示する
            lblTitle.Text = taskTitle;
            //lblContent.Text = content;
            lblAddDate.Text = addDate.ToShortDateString();
            lblDeadLine.Text = deadline.ToShortDateString();
            lblPriority.Text = priority;

            //残り日数を計算する
            TimeSpan remainingsTime = deadline - DateTime.Today;
            lblRemainingDays.Text = remainingsTime.Days.ToString() + " 日";

            //lblContentの表示範囲の調整
            lblContent.AutoSize = false;
            lblContent.Size = new Size(Size.Width, Size.Height);
            lblContent.Text = taskContent;

            if (updateDate.HasValue)
            {
                lblUpdateDate.Text = "更新日：" + updateDate.Value.ToShortDateString();
            }
            else
            { lblUpdateDate.Text = ""; }
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

        private void btnTaskList_Click(object sender, EventArgs e)
        {
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask editTaskForm = new EditTask(taskID);
            editTaskForm.Show();
            this.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("このタスクを削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DeleteTask();
            }
        }

        private void DeleteTask()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Tasks WHERE Title = @Title AND AddDate = @AddDate";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", taskTitle);
                command.Parameters.AddWithValue("@AddDate", addDate);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("タスクが削除されました。");
                        HomeScreen homeScreen = new HomeScreen();
                        homeScreen.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("タスクの削除に失敗しました。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー: " + ex.Message);
                }

            }
        }

        private void TaskDetails_Load(object sender, EventArgs e)
        {

            DisplayTaskDetails(null);
        }
    }
}
