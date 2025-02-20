﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ticari_otomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtKulAd.Text = "";
            TxtPass.Text = "";
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKulAd.Text = "";
            TxtPass.Text = "";
        }

        private void BtnIslem_Click(object sender, EventArgs e)
        {
            if (BtnIslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKulAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtPass.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (BtnIslem.Text=="Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set sifre=@p2 where kullaniciad=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1",TxtKulAd.Text);
                komut1.Parameters.AddWithValue("@p2",TxtPass.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Şifre Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }


            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                TxtKulAd.Text = dr["KullaniciAd"].ToString();
                TxtPass.Text = dr["Sifre"].ToString();
                BtnIslem.Text = "Güncelle";
            }
        }

        private void TxtKulAd_TextChanged(object sender, EventArgs e)
        {
            if (TxtKulAd.Text == "")
            {
                BtnIslem.Text = "Kaydet";
            }
        }

        private void BtnClr_Click(object sender, EventArgs e)
        {
            temizle();
        }
        

    }
}
