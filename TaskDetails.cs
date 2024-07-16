using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{
    public partial class TaskDetails : Form
    {

        private string taskTitle;
        private string taskContent;
        private DateTime addDate;
        private DateTime deadline;
        private string priority;

        public TaskDetails(string title, string content, DateTime adddate, DateTime deadline, string priority)
        {
            InitializeComponent();

            this.taskTitle = title;
            this.taskContent = content;
            this.addDate = adddate;
            this.deadline = deadline;
            this.priority = priority;

            
            //ラベルやテキストボックスに情報を表示する
            lblTitle.Text = taskTitle;
            //lblContent.Text = content;
            lblAddDate.Text = adddate.ToShortDateString();
            lblDeadLine.Text = deadline.ToShortDateString();
            lblPriority.Text = priority;

            //残り日数を計算する
            TimeSpan remainingsTime = deadline - DateTime.Today;
            lblRemainingDays.Text = remainingsTime.Days.ToString();

            //lblContentの表示範囲の調整
            lblContent.AutoSize = false;
            lblContent.Size = new Size(Size.Width, Size.Height);
            lblContent.Text = taskContent;
            
            
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
            EditTask editTaskForm = new EditTask(taskTitle, taskContent, addDate, deadline, priority);
            editTaskForm.Show();
            this.Close();

        }

  
    }
}
