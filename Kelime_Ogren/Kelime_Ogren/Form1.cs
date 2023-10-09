using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms.VisualStyles;

namespace Kelime_Ogren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\legen\OneDrive\Masaüstü\dbSozluk.accdb");
        Random rast = new Random();
        int Rsayi;
        int kelime;
        int sure=90;
       
        void getir()
        {
            Rsayi = rast.Next(1, 2489);


            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * From sozluk where id=@p1", baglanti);
            komut.Parameters.Add("@p1", OleDbType.SmallInt).Value = Rsayi;
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtingilizce.Text = dr[1].ToString();
                lblCevap.Text = dr[2].ToString();
                lblCevap.Text = lblCevap.Text.ToLower();
              
            }
            baglanti.Close();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
            txtturkce.Focus();
            timer1.Start();
            
        }

        private void txtturkce_TextChanged(object sender, EventArgs e)
        {
            if( txtturkce.Text == lblCevap.Text)
            {
                kelime++;
                lblkelime.Text = kelime.ToString();
                txtturkce.Clear();
                getir();
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblsüre.Text = sure.ToString();
            if(sure == 0)
            {
                txtingilizce.Enabled = false;
                txtturkce.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
