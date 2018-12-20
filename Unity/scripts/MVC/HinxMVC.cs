using HinxCor.MVC.Core;

namespace HinxCor.MVC
{
    /// <summary>
    /// HinxMVC入口
    /// </summary>
    public class HinxMVC : Facade
    {
        public static new HinxMVC instance
        {
            get
            {
                if (_instance == null) _instance = new HinxMVC();
                return _instance as HinxMVC;
            }
        }

        /// <summary>
        /// Initialize MVC 
        /// </summary>
        public void StartUp()
        {
            NotificationCenter.Display("HinxMVC Start");
        }
    }
}
