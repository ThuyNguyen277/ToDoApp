namespace ToDoApp
{
    partial class HomeScreen
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel tasksFlowLayoutPanel;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTopSide = new System.Windows.Forms.Panel();
            this.picMin = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picMax = new System.Windows.Forms.PictureBox();
            this.TodoAppName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tasksFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.picAddTask = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.pnlTopSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAddTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopSide
            // 
            this.pnlTopSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.pnlTopSide.Controls.Add(this.picMin);
            this.pnlTopSide.Controls.Add(this.picClose);
            this.pnlTopSide.Controls.Add(this.picMax);
            this.pnlTopSide.Controls.Add(this.TodoAppName);
            this.pnlTopSide.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopSide.Location = new System.Drawing.Point(0, 0);
            this.pnlTopSide.Name = "pnlTopSide";
            this.pnlTopSide.Size = new System.Drawing.Size(464, 45);
            this.pnlTopSide.TabIndex = 0;
            // 
            // picMin
            // 
            this.picMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMin.BackgroundImage = global::ToDoApp.Properties.Resources.Subtract;
            this.picMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMin.Location = new System.Drawing.Point(350, 7);
            this.picMin.Name = "picMin";
            this.picMin.Size = new System.Drawing.Size(30, 30);
            this.picMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMin.TabIndex = 3;
            this.picMin.TabStop = false;
            this.picMin.Click += new System.EventHandler(this.picMin_Click);
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.BackgroundImage = global::ToDoApp.Properties.Resources.Close_Window;
            this.picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picClose.Location = new System.Drawing.Point(422, 7);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(30, 30);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClose.TabIndex = 1;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picMax
            // 
            this.picMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMax.BackgroundImage = global::ToDoApp.Properties.Resources.Expand;
            this.picMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMax.Location = new System.Drawing.Point(386, 7);
            this.picMax.Name = "picMax";
            this.picMax.Size = new System.Drawing.Size(30, 30);
            this.picMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMax.TabIndex = 2;
            this.picMax.TabStop = false;
            this.picMax.Click += new System.EventHandler(this.picMax_Click);
            // 
            // TodoAppName
            // 
            this.TodoAppName.AutoSize = true;
            this.TodoAppName.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodoAppName.ForeColor = System.Drawing.Color.White;
            this.TodoAppName.Location = new System.Drawing.Point(6, 7);
            this.TodoAppName.Name = "TodoAppName";
            this.TodoAppName.Size = new System.Drawing.Size(121, 30);
            this.TodoAppName.TabIndex = 0;
            this.TodoAppName.Text = "TodoApp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.label1.Location = new System.Drawing.Point(153, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "タスク一覧";
            // 
            // tasksFlowLayoutPanel
            // 
            this.tasksFlowLayoutPanel.AutoScroll = true;
            this.tasksFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tasksFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tasksFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tasksFlowLayoutPanel.Name = "tasksFlowLayoutPanel";
            this.tasksFlowLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.tasksFlowLayoutPanel.TabIndex = 0;
            // 
            // picAddTask
            // 
            this.picAddTask.BackgroundImage = global::ToDoApp.Properties.Resources.Add;
            this.picAddTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picAddTask.Location = new System.Drawing.Point(404, 534);
            this.picAddTask.Name = "picAddTask";
            this.picAddTask.Size = new System.Drawing.Size(30, 30);
            this.picAddTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAddTask.TabIndex = 4;
            this.picAddTask.TabStop = false;
            this.picAddTask.Click += new System.EventHandler(this.picAddTask_Click);
            // 
            // listViewTasks
            // 
            this.listViewTasks.CheckBoxes = true;
            this.listViewTasks.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.HideSelection = false;
            this.listViewTasks.Location = new System.Drawing.Point(12, 216);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(440, 250);
            this.listViewTasks.TabIndex = 6;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            this.listViewTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewTasks_ItemCheck);
            this.listViewTasks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewTasks_MouseUp);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.btnPrevious.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.Location = new System.Drawing.Point(30, 472);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(50, 30);
            this.btnPrevious.TabIndex = 7;
            this.btnPrevious.Text = "前へ";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.btnNext.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(366, 472);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 30);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "次へ";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.Location = new System.Drawing.Point(172, 485);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(0, 17);
            this.lblPageInfo.TabIndex = 3;
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 691);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.listViewTasks);
            this.Controls.Add(this.picAddTask);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlTopSide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeScreen";
            this.Text = "Todoアプリ";
            this.pnlTopSide.ResumeLayout(false);
            this.pnlTopSide.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAddTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopSide;
        private System.Windows.Forms.Label TodoAppName;
        private System.Windows.Forms.PictureBox picMin;
        private System.Windows.Forms.PictureBox picMax;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picAddTask;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageInfo;
    }
}

