﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.MessageSystem;
using PloobsEngine.Entity;

namespace EngineTestes.Scripts
{
    public class InterScriptTalking : IScriptHelper,IRecieveMessageEntity 
    {
        public InterScriptTalking()
        {            
        }

        protected long id;
        public virtual void RecieveMessage(Message message)
        {
            
        }

        public virtual void execute()
        {
        }       

        protected void SendMessage(String tag, object content)
        {
            Message mes = new Message(id, -1, tag, Priority.MEDIUM, 0, SenderType.NORMAL, content,null);
            MessageDeliver.SendMessage(mes);
        }

        #region IRecieveMessageEntity Members

        public bool HandleThisMessageType(SenderType type)
        {
            return true;
        }

        public void HandleMessage(Message mes)
        {
            RecieveMessage(mes);
        }

        #endregion

        #region IEntity Members

        public long GetId()
        {
            return id;
        }

        public void SetId(long id)
        {
            this.id = id;
        }

        #endregion
    }
}
