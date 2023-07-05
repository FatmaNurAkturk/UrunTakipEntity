using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrunTakipEntity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbUrunEntities db = new DbUrunEntities();
        private void btnListele_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TblMusteri.ToList();
            var deger = from x in db.TblMusteri
                        select new
                        {
                            x.MusteriId,
                            x.Ad,
                            x.Soyad,
                            x.Sehir,
                            x.Bakiye
                        };
            dataGridView1.DataSource = deger.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TblMusteri t = new TblMusteri();
            t.Ad = txtAd.Text;
            t.Soyad = txtSoyad.Text;
            t.Sehir = txtSehir.Text;
            t.Bakiye = decimal.Parse(txtBakiye.Text);
            db.TblMusteri.Add(t);
            db.SaveChanges();
            MessageBox.Show("Müşteri başarı ile kaydedildi.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(txtId.Text);
            var x = db.TblMusteri.Find(Id);
            db.TblMusteri.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Müşteri silme işlemi başarı ile gerçekleştirildi.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(txtId.Text);
            var x = db.TblMusteri.Find(Id);
            x.Ad = txtAd.Text;
            x.Soyad = txtSoyad.Text;
            x.Sehir = txtSehir.Text;
            x.Bakiye = decimal.Parse(txtBakiye.Text);
            db.SaveChanges();
            MessageBox.Show("Müşteri bilgisi güncellendi.");

        }
    }
}
