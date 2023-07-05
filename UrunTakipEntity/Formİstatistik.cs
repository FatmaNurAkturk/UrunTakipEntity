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
    public partial class Formİstatistik : Form
    {
        public Formİstatistik()
        {
            InitializeComponent();
        }
        DbUrunEntities db = new DbUrunEntities();
        private void Formİstatistik_Load(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Today;
            lblMusteriSayisi.Text = db.TblMusteri.Count().ToString();
            lblKategoriSayisi.Text = db.TblKategori.Count().ToString();
            lblUrunSayisi.Text = db.TblUrunler.Count().ToString();
            lblBeyazEsya.Text = db.TblUrunler.Count(x=>x.Kategori==1).ToString();
            lblToplamStok.Text = db.TblUrunler.Sum(x=>x.Stok).ToString();
            lblBugunSatıs.Text = db.TblSatislar.Count(x => x.Tarih == bugun).ToString();
            lblToplamKasa.Text = db.TblSatislar.Sum(x => x.ToplamTutar).ToString()+"$";
            lblBugunKasa.Text = db.TblSatislar.Where(x => x.Tarih == bugun).Sum(y => y.ToplamTutar).ToString() + "$";
            lblEnYuksekFiyat.Text = (from x in db.TblUrunler
                                     orderby x.SatisFiyat descending
                                     select x.UrunAd).FirstOrDefault();
            lblEnDusukFiyat.Text = (from x in db.TblUrunler
                                    orderby x.SatisFiyat
                                    select x.UrunAd).FirstOrDefault();
            lblEnfazlaStoklu.Text = (from x in db.TblUrunler
                                     orderby x.Stok descending
                                     select x.UrunAd).FirstOrDefault();
            lblEnzAzStok.Text = (from x in db.TblUrunler
                                 orderby x.Stok
                                 select x.UrunAd).FirstOrDefault();
        }
    }
}
