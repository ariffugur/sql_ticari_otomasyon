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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl4.DataSource = dt;
        }

        void MusteriHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {

        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            FirmaHareketleri();
            MusteriHareketleri();

        }
    }
}
