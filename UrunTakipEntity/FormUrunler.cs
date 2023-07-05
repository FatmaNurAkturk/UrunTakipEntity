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
    public partial class FormUrunler : Form
    {
        public FormUrunler()
        {
            InitializeComponent();
        }
        DbUrunEntities db = new DbUrunEntities();

        void urunListele()
        {
            var urun = from x in db.TblUrunler
                       select new
                       {
                           x.UrunId,
                           x.UrunAd,
                           x.Stok,
                           x.AlisFiyat,
                           x.SatisFiyat, // Değişiklik burada
                           x.TblKategori.Ad
                       };
            dataGridView1.DataSource = urun.ToList();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtStok.Text="";
            txtAlıs.Text="";
            txtSatıs.Text = "";
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            // dataGridView1.DataSource = db.TblUrunler.ToList();
            urunListele();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TblUrunler t = new TblUrunler();
            t.UrunAd = txtAd.Text;
            t.Stok = short.Parse(txtStok.Text);
            t.AlisFiyat = decimal.Parse(txtAlıs.Text);
            t.SatisFiyat = decimal.Parse(txtSatıs.Text);
            t.Kategori = int.Parse(comboBox1.SelectedValue.ToString());
            db.TblUrunler.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün başarılı bir şekilde sisteme kaydedildi");
            urunListele();
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtStok.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAlıs.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSatıs.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                var x = db.TblUrunler.Find(id);
                db.TblUrunler.Remove(x);
                db.SaveChanges();
                MessageBox.Show("Ürün başarılı bir şekilde silindi", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                MessageBox.Show("Lütfen verileri listeledikten sonra bir satıra tıklayıp silmek istediğiniz kaydı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            urunListele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var x = db.TblUrunler.Find(id);
            x.UrunAd = txtAd.Text;
            x.Stok = short.Parse(txtStok.Text);
            x.AlisFiyat = decimal.Parse(txtAlıs.Text);
            x.SatisFiyat = decimal.Parse(txtSatıs.Text);
            x.Kategori = int.Parse(comboBox1.SelectedValue.ToString());
            db.SaveChanges();
            MessageBox.Show("Verileriniz başarılı bir şekilde güncellendi","Güncelleme Bilgisi", MessageBoxButtons.OK,MessageBoxIcon.Information);
            urunListele();
        }

        private void FormUrunler_Load(object sender, EventArgs e)
        {
            urunListele();
            comboBox1.DataSource = db.TblKategori.ToList();
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Ad";
        }
    }
}
