using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Udemy.Doviz.Entities;

namespace Udemy.Doviz.Core
{
    public class BusinessLogicLayer : BaseClass
    {
        DatabaseLogicLayer DLL;
        public BusinessLogicLayer()
        {

            DLL = new DatabaseLogicLayer();

        }

        public List<ParaBirimi> ParaBirimiListesi()
        {
            List<ParaBirimi> ParaBirimleri = new List<ParaBirimi>();

            SqlDataReader reader = DLL.ParaBirimiListesi();
            while (reader.Read())
            {
                ParaBirimleri.Add(new ParaBirimi()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    Code = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    Tanim = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    UyariLimit = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3)

                });
            }
            reader.Close();
            DLL.BaglantiIslemleri();
            return ParaBirimleri;
        }

        public void KurBilgileriniGuncelle()
        {
            WebClient webClient = new WebClient();
            string JsonDatatxt = webClient.DownloadString("https://www.doviz.com/api/v1/currencies/all/latest");
            List<JsonDataType> DovizKurBilgileri = JsonConvert.DeserializeObject<List<JsonDataType>>(JsonDatatxt);

            List<ParaBirimi> ParaBirimiListe = ParaBirimiListesi();
            for (int i = 0; i < ParaBirimiListe.Count; i++)
            {
                JsonDataType BulunanKur = DovizKurBilgileri.FirstOrDefault(I => I.code == ParaBirimiListe[i].Code);
                KurKayitEKLE(Guid.NewGuid(), ParaBirimiListe[i].ID, decimal.Parse(BulunanKur.buying), decimal.Parse(BulunanKur.selling), DateTime.Now);
                if (decimal.Parse(BulunanKur.selling) <= ParaBirimiListe[i].UyariLimit && ParaBirimiListe[i].UyariLimit != 0)
                {
                    EmailGonder(BulunanKur, ParaBirimiListe[i]);
                }
            }

        }

        public DataTable KurGecmisGoruntule()
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Doviz Tanım", typeof(string));
            DT.Columns.Add("Doviz Kod", typeof(string));
            DT.Columns.Add("Alış", typeof(string));
            DT.Columns.Add("Satış", typeof(string));
            DT.Columns.Add("OluşturmaTarih", typeof(string));

            List<KurGecmis> KurGecmisList = KurGecmisListe();
            List<ParaBirimi> ParabirimList = ParaBirimiListesi();

            for (int i = 0; i < KurGecmisList.Count; i++)
            {
                DT.Rows.Add(
                    ParabirimList.FirstOrDefault(I => I.ID == KurGecmisList[i].ParaBirimiID).Tanim,
                    ParabirimList.FirstOrDefault(I => I.ID == KurGecmisList[i].ParaBirimiID).Code,
                    KurGecmisList[i].Alis.ToString(),
                    KurGecmisList[i].Satis.ToString(),
                    KurGecmisList[i].OlusturmaTarih.ToString("dd.MM.yyyy hh:mm")
                    );
            }

            return DT;
        }

        public List<Kur> KurListe()
        {
            List<Kur> KurDegerleri = new List<Kur>();

            SqlDataReader reader = DLL.KurListe();
            while (reader.Read())
            {
                KurDegerleri.Add(new Kur()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    ParaBirimiID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    Alis = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    Satis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    OlusturmaTarih = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                });
            }
            reader.Close();
            DLL.BaglantiIslemleri();

            return KurDegerleri;
        }

        public Kur KurListe(Guid ParabirimiID)
        {
            Kur Kur = new Kur();

            SqlDataReader reader = DLL.KurListe();
            while (reader.Read())
            {
                Kur = new Kur()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    ParaBirimiID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    Alis = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    Satis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    OlusturmaTarih = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                };
            }
            reader.Close();
            DLL.BaglantiIslemleri();

            return Kur;
        }

        public List<KurGecmis> KurGecmisListe()
        {
            List<KurGecmis> KurGecmisListe = new List<KurGecmis>();
            SqlDataReader reader = DLL.KurGecmisListe();
            while (reader.Read())
            {
                KurGecmisListe.Add(new KurGecmis()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    KurID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    ParaBirimiID = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                    Alis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    Satis = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                    OlusturmaTarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                });
            }
            reader.Close();
            DLL.BaglantiIslemleri();
            return KurGecmisListe;
        }

        public List<KurGecmis> KurGecmisListe(Guid ParaBirimiID)
        {
            List<KurGecmis> KurGecmisListe = new List<KurGecmis>();
            SqlDataReader reader = DLL.KurGecmisListe(ParaBirimiID);
            while (reader.Read())
            {
                KurGecmisListe.Add(new KurGecmis()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    KurID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    ParaBirimiID = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                    Alis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    Satis = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                    OlusturmaTarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                });
            }
            reader.Close();
            DLL.BaglantiIslemleri();
            return KurGecmisListe;
        }

        public void KurKayitEKLE(Guid ID, Guid ParaBirimiID, decimal Alis, decimal Satis, DateTime OlusturmaTarih)
        {
            if (ID != Guid.Empty && ParaBirimiID != Guid.Empty && Alis != 0 && Satis != 0 && OlusturmaTarih > DateTime.MinValue)
            {
                DLL.KurKayitEKLE(new Kur()
                {
                    ID = ID,
                    ParaBirimiID = ParaBirimiID,
                    Alis = Alis,
                    Satis = Satis,
                    OlusturmaTarih = OlusturmaTarih
                });
            }
            else
            {
                
            }

        }

        private void EmailGonder(JsonDataType kurBilgisi, ParaBirimi paraBirimi)
        {
            Encoding encode = Encoding.GetEncoding("windows-1254");
            MailMessage Email = new MailMessage();
            MailAddress From = new MailAddress("udemy@cengizatilla.com", "Döviz Uygulaması", encode);
            MailAddress TO = new MailAddress("cengiz.atilla@hotmail.com", "Cengiz ATİLLA", encode);
            Email.To.Add(TO);
            Email.From = From;
            Email.Subject = $"{paraBirimi.Tanim} - {paraBirimi.UyariLimit} Değerinize ulaştı";
            Email.Body = $"{paraBirimi.Tanim} - {paraBirimi.UyariLimit} değerine {DateTime.Now.ToString()} tarihinde ulaştı. Alım yapabilirsiniz.";
            Email.IsBodyHtml = true;

            string SMTPServer = "mail.cengizatilla.com";
            int SMTPPort = 587;
            string KullaniciAdi = "udemy@cengizatilla.com";
            string Sifre = "S15LWJVd";

            SmtpClient client = new SmtpClient(SMTPServer, SMTPPort);
            client.Credentials = new System.Net.NetworkCredential(KullaniciAdi, Sifre);
            client.EnableSsl = false;
            client.Send(Email);

        }
    }
}
