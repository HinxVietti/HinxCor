namespace HinxCor.MVC.Patterns
{
    public class Notification
    {
        public string name;
        public object data;

        public Notification(string name,object data = null)
        {
            this.name = name;
            this.data = data;
        }
    }
}
