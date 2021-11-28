using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Doviz.Entities;

namespace Udemy.Doviz.Core
{
    public class DatabaseLogicLayer : BaseClass
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;

        public DatabaseLogicLayer()
        {
            con = new SqlConnection("data source=.; initial catalog = UdemyDoviz; user Id = sa; password = 1");

        }

        public void BaglantiIslemleri()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }
        }

        public SqlDataReader ParaBirimiListesi()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("Select * from ParaBirimi", con);
                BaglantiIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public SqlDataReader KurListe()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("select * from Kur", con);
                BaglantiIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public SqlDataReader KurListe(Guid ParabirimiID)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("select * from Kur where ParaBirimiID = @ParaBirimiID", con);
                cmd.Parameters.Add("@ParaBirimiID", System.Data.SqlDbType.UniqueIdentifier).Value = ParabirimiID;
                BaglantiIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public SqlDataReader KurGecmisListe()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("select * from KurGecmis", con);
                BaglantiIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public SqlDataReader KurGecmisListe(Guid ParaBirimiID)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("select * from KurGecmis where ParaBirimiID = @ParaBirimiID", con);
                cmd.Parameters.Add("@ParaBirimiID", System.Data.SqlDbType.UniqueIdentifier).Value = ParaBirimiID;
                BaglantiIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public void KurKayitEKLE(Kur kur)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("KurKayitEKLE", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = kur.ID;
                cmd.Parameters.Add("@ParaBirimiID", SqlDbType.UniqueIdentifier).Value = kur.ParaBirimiID;
                cmd.Parameters.Add("@Alis", SqlDbType.Decimal).Value = kur.Alis;
                cmd.Parameters.Add("@Satis", SqlDbType.Decimal).Value = kur.Satis;
                cmd.Parameters.Add("@OlusturmaTarih", SqlDbType.DateTime).Value = kur.OlusturmaTarih;
                BaglantiIslemleri();
                cmd.ExecuteNonQuery();
            });
            BaglantiIslemleri();
        }


    }
}
