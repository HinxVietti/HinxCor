using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetWorkProxySetting
{
    public partial class NetworkProxySetting : Form
    {
        public NetworkProxySetting()
        {
            InitializeComponent();
            OnInitializeProxyItem();
        }

        private void OnInitializeProxyItem()
        {
            throw new NotImplementedException();
        }


        // Return True if the internet settings has ProxyEnable = true.
        private static bool IsInternetProxyEnabled()
        {
            //Registry.CurrentUser.OpenSubKey
            try
            {

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
            string[] keys = key.GetValueNames();
            bool result = (int)key.GetValue("ProxyEnable", 0) != 0;
            key.Close();
            return result;
            }
            catch
            {
                return false;
            }
        }


        private static void SetInternetProxyEnabled(bool value)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            string[] keys = key.GetValueNames();
            key.SetValue("ProxyEnable", value ? 1 : 0);
            key.Close();

        }

    }
}
