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
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
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
            else if ( this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState= FormWindowState.Normal;       //ウインドウを通常サイズに戻す
            }
        }

        private void picAddTask_Click(object sender, EventArgs e)
        {
            AddTaskScreen addTaskScreen = new AddTaskScreen();
            addTaskScreen.Show();
            this.Hide();
        }
    }
}
