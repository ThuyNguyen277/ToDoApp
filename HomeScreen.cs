using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{
    public partial class HomeScreen : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ToDoAppConnectionString"].ConnectionString;
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 10;

        public HomeScreen()
        {
            InitializeComponent();
            SetupListView();
            LoadTasks();
            
        }

        private void SetupListView()
        {
            // Set up the ListView control
            listViewTasks.View = View.Details;
            listViewTasks.CheckBoxes = true;
            listViewTasks.FullRowSelect = true;
            listViewTasks.OwnerDraw = false;

            // Add columns to the ListView control
            listViewTasks.Columns.Add("完了", 30);
            listViewTasks.Columns.Add("タイトル", 200);
            listViewTasks.Columns.Add("期限日", 100);
            listViewTasks.Columns.Add("編集", 50);
            listViewTasks.Columns.Add("削除", 50);

            // Attach event handlers for custom drawing and clicking
            listViewTasks.ItemCheck += listViewTasks_ItemCheck;
            listViewTasks.MouseUp += listViewTasks_MouseUp;
        }

        private void LoadTasks()
        {
            // Clear existing items
            listViewTasks.Items.Clear();

           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string countQuery = "SELECT COUNT(*) FROM Tasks WHERE Completed = 0";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);

                    connection.Open();
                    int totalTasks = (int)countCommand.ExecuteScalar();
                    totalPages = (int)Math.Ceiling(totalTasks / (double)pageSize);

                    string selectQuery = @"
                    SELECT taskId, Title, Deadline, Completed
                    FROM (
                        SELECT ROW_NUMBER() OVER (ORDER BY taskId) AS RowNum, *
                        FROM Tasks WHERE Completed = 0
                    ) AS RowConstrainedResult
                    WHERE RowNum >= @RowStart AND RowNum < @RowEnd
                    ORDER BY RowNum";

                    SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@RowStart", (currentPage - 1) * pageSize + 1);
                    selectCommand.Parameters.AddWithValue("@RowEnd", currentPage * pageSize + 1);

                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Checked = (bool)reader["Completed"]; // Unchecked by default
                        item.SubItems.Add(reader["Title"].ToString());
                        item.SubItems.Add(((DateTime)reader["Deadline"]).ToShortDateString());
                        item.SubItems.Add("編集");
                        item.SubItems.Add("削除");
                        item.Tag = reader["taskId"].ToString();

                        listViewTasks.Items.Add(item);
                    }

                    lblPageInfo.Text = $"ページ{currentPage} / {totalPages}";

                }
                catch (Exception ex) 
                {
                    MessageBox.Show("データベースエラー：" + ex.Message);
                }

            }
        }


        /*
        private void EditTask(string taskId)
        {
            // EditTaskScreenを開く処理を追加する
            EditTask editForm = new EditTask(taskId);
            editForm.Show();
            this.Hide(); // このフォームを隠す
        }

        private void DeleteTask(string taskId)
        {
            // タスク削除処理を追加する
            MessageBox.Show($"タスクID {taskId} を削除しました。");

            // 再度タスク一覧をロードする
            LoadTasks();
        }


        */
        private void picAddTask_Click(object sender, EventArgs e)
        {
            AddTaskScreen addTaskScreen = new AddTaskScreen();
            addTaskScreen.Show();
            this.Hide();
        }
        /*
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > 0)
            {
                int taskId = (int)listViewTasks.SelectedItems[0].Tag;

                DialogResult result = MessageBox.Show("このタスクを削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteTask(taskId);
                }
            }
            else
            {
                MessageBox.Show("削除するタスクを選択してください。");
            }
        }
        */
        /*
        private void DeleteTask(int taskId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Tasks WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", taskId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("タスクが削除されました。");
                        LoadTasks(); // Refresh task list after deletion
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
        */
        /*
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > 0)
            {
                int taskId = (int)listViewTasks.SelectedItems[0].Tag;
                EditTask editTaskForm = new EditTask(taskId.ToString());
                editTaskForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("編集するタスクを選択してください。");
            }
        }
        */
        

        



        

        private void listViewTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                ListViewItem item = listViewTasks.Items[e.Index];
                string taskId = item.Tag.ToString();
                bool completed = e.NewValue == CheckState.Checked;

                UpdateTaskCompletion(taskId, completed);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
            }
        }

        private void UpdateTaskCompletion(string taskId, bool completed)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tasks SET Completed = @Completed WHERE taskId = @taskId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@taskId", taskId);
                command.Parameters.AddWithValue("@Completed", completed);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("タスクの完了状態が更新されました。");
                    }
                    else
                    {
                        MessageBox.Show("タスクの完了状態の更新に失敗しました。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー: " + ex.Message);
                }
            }
        }


        private void listViewTasks_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewHitTestInfo hit = listViewTasks.HitTest(e.Location);
                ListViewItem item = hit.Item;
                if (item != null)
                {
                    int colIndex = hit.Item.SubItems.IndexOf(hit.SubItem);
                    string taskId = item.Tag.ToString();

                    if (colIndex == 3) // 編集ボタンの列インデックス
                    {
                        EditTask editTaskForm = new EditTask(taskId);
                        editTaskForm.ShowDialog();
                        LoadTasks();
                    }
                    else if (colIndex == 4) // 削除ボタンの列インデックス
                    {
                        DialogResult result = MessageBox.Show("このタスクを削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteTask(taskId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
            }
        }

        private void DeleteTask(string taskId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Tasks WHERE taskId = @taskId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@taskId", taskId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("タスクが削除されました。");
                        LoadTasks(); // Reload tasks after deletion
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

        

        /// <summary>
        /// 「ｘ」ボタンがクリックされる時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();     //アプリケーションを終了する
        }

        /// <summary>
        /// 「ー」ボタンがクリックされる時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;       //ウインドウの状態を最小化に設定する
        }

        /// <summary>
        /// 最大化アイコンがクリックされた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picMax_Click(object sender, EventArgs e)
        {
            //ウィンドウの状態が通常の場合
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;   //ウィンドウを最大化する
            }
            //ウインドウの状態が最大化されている場合
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;       //ウインドウを通常サイズに戻す
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadTasks();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadTasks();
            }
        }


    }
}
