using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Udemy.Doviz.ApiServis
{
    public partial class DovizKurlariServisi : ServiceBase
    {
        public Timer t;

        public DovizKurlariServisi()
        {
            InitializeComponent();
            t = new Timer(120000); // 2 dakika olarak ayarlandı
            t.Elapsed += T_Elapsed;
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Udemy.Doviz.Core.BusinessLogicLayer BLL = new Core.BusinessLogicLayer();
            BLL.KurBilgileriniGuncelle(); // 
        }

        protected override void OnStart(string[] args)
        {
            t.Start();
        }

        protected override void OnStop()
        {
            t.Stop();
        }
        protected override void OnContinue()
        {
            t.Start();
        }

        protected override void OnPause()
        {
            t.Stop();
        }

        protected override void OnShutdown()
        {
            t.Stop();
        }
    }
}
