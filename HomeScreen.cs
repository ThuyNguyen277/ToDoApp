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
        //データベース接続文字列を取得
        private string connectionString = ConfigurationManager.ConnectionStrings["ToDoAppConnectionString"].ConnectionString;
        private int currentPage = 1;        //現在のページ番号
        private int totalPages = 1;         //総ページ数
        private int pageSize = 10;          //1ページあたりの表示タスク数

        public HomeScreen()
        {
            InitializeComponent();
            //テーブル一覧の設定
            SetupListView();
            //タスクの読み込み
            LoadTasks();

        }

        //表示されるテーブルの設定
        private void SetupListView()
        {
            // タスク一覧コントロールの設定
            listViewTasks.View = View.Details;      //詳細ビューに設定
            listViewTasks.CheckBoxes = true;        //チェックボックスを有効にする
            listViewTasks.FullRowSelect = true;     //全行選択を有効にする
            listViewTasks.OwnerDraw = false;        //カスタム描画を無効にする

            // タスク一覧の列を追加
            listViewTasks.Columns.Add("完了", 30);    　　
            listViewTasks.Columns.Add("タイトル", 200);
            listViewTasks.Columns.Add("期限日", 100);
            listViewTasks.Columns.Add("編集", 50);
            listViewTasks.Columns.Add("削除", 50);

            // イベント処理
            listViewTasks.ItemCheck += listViewTasks_ItemCheck;　    //チェックボックスの変更イベント
            listViewTasks.MouseUp += listViewTasks_MouseUp;          //マウスアップイベント

            // 完成したタスク一覧のコントロールの設定
            listViewCompleted.View = View.Details;
            listViewCompleted.CheckBoxes = true;
            listViewCompleted.FullRowSelect = true;
            listViewCompleted.OwnerDraw = false;

            // 完成したタスク一覧のカラム追加
            listViewCompleted.Columns.Add("完了", 30);
            listViewCompleted.Columns.Add("タイトル", 200);
            listViewCompleted.Columns.Add("期限日", 100);
            listViewCompleted.Columns.Add("削除", 50); // 

            // イベント処理
            listViewCompleted.MouseUp += listViewCompleted_MouseUp;     //マウスアップイベント
        }

        private void LoadTasks()
        {
            // 既存のアイテムをクリア
            listViewTasks.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //タスクの総数を取得
                    string countQuery = "SELECT COUNT(*) FROM Tasks WHERE Completed = 0";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);   

                    connection.Open();
                    int totalTasks = (int)countCommand.ExecuteScalar(); //タスクの総数を取得
                    totalPages = (int)Math.Ceiling(totalTasks / (double)pageSize); //総ページ数を計算

                    //ページごとのタスクを取得するクエリ
                    string selectQuery = @"
                    SELECT taskId, Title, Deadline, Completed
                    FROM (
                        SELECT ROW_NUMBER() OVER (ORDER BY Deadline) AS RowNum, *
                        FROM Tasks WHERE Completed = 0
                    ) AS RowConstrainedResult
                    WHERE RowNum >= @RowStart AND RowNum < @RowEnd
                    ORDER BY RowNum";

                    SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@RowStart", (currentPage - 1) * pageSize + 1);
                    selectCommand.Parameters.AddWithValue("@RowEnd", currentPage * pageSize + 1);

                    using (SqlDataReader activeTasksReader = selectCommand.ExecuteReader())
                    {
                        while (activeTasksReader.Read())
                        {
                            //ListViewItemを作成し、タスクの情報を設定
                            ListViewItem item = new ListViewItem();
                            item.Checked = false; // デフォルトで未完了
                            item.SubItems.Add(activeTasksReader["Title"].ToString());
                            item.SubItems.Add(((DateTime)activeTasksReader["Deadline"]).ToShortDateString());
                            item.SubItems.Add("編集");
                            item.SubItems.Add("削除");
                            item.Tag = activeTasksReader["taskId"].ToString(); //タスクIDをタグに設定

                            listViewTasks.Items.Add(item);
                        }
                    }

                    // 完了タスクを別に読み込む・期限日のソート
                    string completedTasksQuery = @"
                    SELECT taskId, Title, Deadline, Completed
                    FROM Tasks
                    WHERE Completed = 1
                    ORDER BY Deadline DESC"; 
                    SqlCommand completedTasksCommand = new SqlCommand(completedTasksQuery, connection);
                    SqlDataReader completedTasksReader = completedTasksCommand.ExecuteReader();

                    while (completedTasksReader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Checked = true;// 完成したタスクをチェック表示
                        item.SubItems.Add(completedTasksReader["Title"].ToString());
                        item.SubItems.Add(((DateTime)completedTasksReader["Deadline"]).ToShortDateString());
                        item.SubItems.Add("削除");
                        item.Tag = completedTasksReader["taskId"].ToString();

                        listViewCompleted.Items.Add(item);
                    }

                    lblPageInfo.Text = $"ページ{currentPage} / {totalPages}";　//現在のページ / 総ページ数の表示

                    listViewTasks.EndUpdate();
                    listViewCompleted.EndUpdate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー：" + ex.Message); //エラーメッセージが表示される
                }

            }
        }

        /// <summary>
        /// ＋というアイコンがクリックされる時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAddTask_Click(object sender, EventArgs e)
        {
            //新規タスク追加ページへ移動
            AddTaskScreen addTaskScreen = new AddTaskScreen();
            addTaskScreen.Show();   //新規タスク追加画面を呼び出し
            this.Hide(); 　　　　　　//このページを隠す
        }

        /// <summary>
        /// 完了されたタスクがチェックされる時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                ListViewItem item = listViewTasks.Items[e.Index];
                string taskId = item.Tag.ToString();
                bool completed = e.NewValue == CheckState.Checked;

                UpdateTaskCompletion(taskId, completed);

                if (completed)
                {
                    // チェックされたタスクがタスク一覧からコピーし、完了されたタスク一覧に追加する
                    listViewCompleted.Items.Add((ListViewItem)item.Clone());
                    listViewTasks.Items.RemoveAt(e.Index); //チェックされたタスクをタスク一覧から
                }
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
                    else if (colIndex == 1)
                    {
                        ShowTaskDetails(taskId);
                        this.Hide();
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

        
        private void ShowTaskDetails(string taskId)
        {
            // タスクの詳細画面を表示する
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Content, AddDate, Deadline, Priority FROM Tasks WHERE TaskID = @TaskID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", taskId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        string content = reader["Content"].ToString();
                        DateTime addDate = (DateTime)reader["AddDate"];
                        DateTime deadline = (DateTime)reader["Deadline"];
                        string priority = reader["Priority"].ToString();

                        TaskDetails taskDetailsForm = new TaskDetails(taskId, title, content, addDate, deadline, priority);
                        taskDetailsForm.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー：" + ex.Message);
                }
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

        private void listViewCompleted_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewHitTestInfo hit = listViewCompleted.HitTest(e.Location);
                ListViewItem item = hit.Item;
                if (item != null)
                {
                    int colIndex = hit.Item.SubItems.IndexOf(hit.SubItem);
                    string taskId = item.Tag.ToString();

                    if (colIndex == 3) // 削除ボタンの列インデックス
                    {
                        DialogResult result = MessageBox.Show("この完了したタスクを削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteCompletedTask(taskId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
            }
        }

        private void DeleteCompletedTask(string taskId)
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
                        MessageBox.Show("完了したタスクが削除されました。");
                        LoadCompletedTasks(); // 完了したタスク一覧を再読込する
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

        private void LoadCompletedTasks()
        {
            listViewCompleted.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string selectQuery = @"
                SELECT taskId, Title, Deadline
                FROM Tasks
                WHERE Completed = 1
                ORDER BY taskId";

                    SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                    connection.Open();
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        // Completed tasks are checked by default
                        item.SubItems.Add(reader["Title"].ToString());
                        item.SubItems.Add(((DateTime)reader["Deadline"]).ToShortDateString());
                        item.SubItems.Add("削除"); // Only delete button needed for completed tasks
                        item.Tag = reader["taskId"].ToString();

                        listViewCompleted.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データベースエラー：" + ex.Message);
                }
            }
        }
    }
}