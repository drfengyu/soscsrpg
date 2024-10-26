using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 202410150935跟踪任务完成状态
    /// </summary>
    public class QuestStatus:BaseNotificationClass
    {
        private bool isCompleted;

        public Quest PlayerQuest { get; }
        public bool IsCompleted { get => isCompleted;
            set { isCompleted = value;
                OnPropertyChanged();
            } }
        public QuestStatus(Quest quest) {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
