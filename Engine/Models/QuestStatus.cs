﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 202410150935跟踪任务完成状态
    /// </summary>
    public class QuestStatus
    {
        public Quest PlayerQuest { get; set; }
        public bool IsCompleted { get; set; }
        public QuestStatus(Quest quest) {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
