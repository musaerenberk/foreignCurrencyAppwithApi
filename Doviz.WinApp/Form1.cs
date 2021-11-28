using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Udemy.Doviz.Entities;

namespace Udemy.Doviz.WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_jsonkurgu_Click(object sender, EventArgs e)
        {
            //Udemy.Doviz.Core.BusinessLogicLayer BLL = new Core.BusinessLogicLayer();
            //BLL.KurBilgileriniGuncelle();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Udemy.Doviz.Core.BusinessLogicLayer BLL = new Core.BusinessLogicLayer();
            BLL.KurBilgileriniGuncelle();
            List<ParaBirimi> ParaBirimleri = BLL.ParaBirimiListesi();
            List<Kur> KurBilgileri = BLL.KurListe();

            Kur Dolar = KurBilgileri.FirstOrDefault(I => I.ParaBirimiID == ParaBirimleri.FirstOrDefault(x => x.Code == "USD").ID);
            lbl_dolar_alis.Text = Dolar.Alis.ToString();
            lbl_dolar_satis.Text = Dolar.Satis.ToString();

            Kur Euro = KurBilgileri.FirstOrDefault(I => I.ParaBirimiID == ParaBirimleri.FirstOrDefault(x => x.Code == "EUR").ID);
            lbl_euro_alis.Text = Euro.Alis.ToString();
            lbl_euro_satis.Text = Euro.Satis.ToString();

            Kur Sterlin = KurBilgileri.FirstOrDefault(I => I.ParaBirimiID == ParaBirimleri.FirstOrDefault(x => x.Code == "GBP").ID);
            lbl_sterlin_alis.Text = Sterlin.Alis.ToString();
            lbl_sterlin_satis.Text = Sterlin.Satis.ToString();

            grd_kurgecmis.DataSource = BLL.KurGecmisGoruntule();

        }
    }
}
