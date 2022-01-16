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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }


        sqlBaglantisi bgl = new sqlBaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_FIRMALAR",
                bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            lookUpEdit2.Properties.ValueMember = "ID";
            lookUpEdit2.Properties.DisplayMember = "AD";
            lookUpEdit2.Properties.DataSource = dt;
        }
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ComboBoxIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            firmalistesi();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR" +
                "(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values " +
                "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            komut.Parameters.AddWithValue("@p2", ComboBoxIl.Text);
            komut.Parameters.AddWithValue("@p3", comboBoxIlce.Text);
            komut.Parameters.AddWithValue("@p4", textSube.Text);
            komut.Parameters.AddWithValue("@p5", textIBAN.Text);
            komut.Parameters.AddWithValue("@p6", textHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", textYetkili.Text);
            komut.Parameters.AddWithValue("@p8", maskedTelefon.Text);
            komut.Parameters.AddWithValue("@p9", maskedTarih.Text);
            komut.Parameters.AddWithValue("@p10", textHesapTürü.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit2.Text);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgisi sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void maskedTelefon2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER Where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBoxIlce.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBoxIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
