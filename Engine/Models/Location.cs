using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 202410150926新增地点的可用任务
    /// </summary>
    public class Location
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }

        public string ImageName { set; get; }

        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();
    }
}
