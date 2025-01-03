﻿using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public abstract class BaseAction
    {
        protected readonly ShopItem _itemInUse;
        public event EventHandler<string> OnActionPerformed;
        protected BaseAction(ShopItem itemInUse) { 
            _itemInUse= itemInUse;
        }

        protected void ReportResult(string result) {
            OnActionPerformed?.Invoke(this,result);

        }
    }
}
