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
    public partial class EditTask : Form
    {
        private string taskTitle;
        private string taskContent;
        private DateTime adddate;
        private DateTime deadline;
        private string priority;
        public EditTask(string title, string content, DateTime adddate, DateTime deadline, string priority)
        {
            InitializeComponent();

            this.taskTitle = title;
            this.taskContent = content;
            this.adddate = adddate;
            this.deadline = deadline;
            this.priority = priority;

            // 各コントロールに情報を表示する
            txtTitle.Text = taskTitle;
            txtContent.Text = taskContent;
            addDate.Value = adddate;
            deadLine.Value = deadline;

            // 優先度によって適切な RadioButton を選択する
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
