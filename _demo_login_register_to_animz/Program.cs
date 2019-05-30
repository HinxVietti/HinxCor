using HinxCor.Security;
using RestSharp;
using System;
using System.Xml;

namespace _demo_login_register_to_animz
{
    class Program
    {
        static string rcpass = "5d89fAfkej-A7c20K_5";

        static void Main()
        {

            MobianoAccount ac = new MobianoAccount();
            ac.Register((d, a) =>
            {
                Console.WriteLine(d + "\t" + a);
            }, "free_v_02focusky.com.cn", "wancai");

            Console.ReadKey();
        }

        static void Main_99(string[] args)
        {
            RestClient client = new RestClient();
            //client.BaseHost = "http://192.168.2.136:90/clientapi/account/login";
            ////client.AddDefaultParameter(p: new Parameter("umail", "free@focusky.com.cn", ParameterType.QueryString));
            ////client.AddDefaultParameter(p: new Parameter("upass", "wancai", ParameterType.QueryString));
            //RestRequest req = new RestRequest(resource: "http://192.168.2.136:90/clientapi/account/login?&umail=free@focusky.com.cn&upass=wancai", method: Method.POST);
            //var res = client.Post(req);

            RestRequest req_1 = new RestRequest("http://192.168.2.136:90/clientapi/account/login", Method.POST);
            req_1.AddParameter("umail", "free@focusky.com.cn");
            req_1.AddParameter("upass", "wancai");
            var resbones = client.Execute(req_1);
            var b64str = resbones.Content;

            //var b64str = res.ContentEncoding;
            //res

            //WebClient client = new WebClient();
            //var b64str = client.DownloadString("http://192.168.2.136:90/clientapi/account/login?&umail=free@focusky.com.cn&upass=wancai");
            var b64dat = Convert.FromBase64String(b64str);
            RC4 rc4 = new RC4(rcpass);
            var dch5 = rc4.Decrypt(b64dat);
            var dc = System.Web.HttpUtility.HtmlDecode(dch5);
            Console.WriteLine(dc);
            Console.ReadKey();
        }
    }
}
