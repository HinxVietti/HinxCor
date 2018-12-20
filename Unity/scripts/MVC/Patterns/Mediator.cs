using HinxCor.MVC.Core;
using HinxCor.MVC.Interfaces;
using System;

namespace HinxCor.MVC.Patterns
{
    class Mediator : IMediator
    {
        public string mediatorName{ get;set;}

        public const string NAME = "Mediator";

        public Mediator()
        {
            this.mediatorName = NAME;
        }

        public virtual string [] listNotificationInterests()
        {
            throw new NotImplementedException();
        }

        public virtual void HandleNotification(Notification notification)
        {

        }

        public void SendNotification(string name,object data = null)
        {
            Facade.instance.SendNotification(name,data);
        }
    }
}
