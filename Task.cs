using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    internal class Task
    {
        public int taskID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Priority { get; set; }

        // コンストラクタ
        public Task(int taskId, string title, string content, DateTime addDate, DateTime deadline, string priority)
        {
            taskID = taskId;
            Title = title;
            Content = content;
            AddDate = addDate;
            Deadline = deadline;
            Priority = priority;
        }
    }
}
