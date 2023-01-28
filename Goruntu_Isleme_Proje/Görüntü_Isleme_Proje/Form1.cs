using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace Görüntü_Isleme_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUygula_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("İşlem seçiniz...");
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    NegatifAl();
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    GriyeDonustur();
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    int deger = 0;
                    try
                    {
                        deger = int.Parse(Interaction.InputBox("Parlaklık ayarı için '-255' ile '+255' arasında bir değer giriniz", "Parlaklık Değiştir", "Değer"));
                    }
                    catch { }

                    if (deger >= -255 && deger <= 255)
                        ParlaklikDegistir(deger);
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    int deger = 0;
                    try
                    {
                        deger = int.Parse(Interaction.InputBox("Eşik değeri için '0' ile '+255' arasında bir değer giriniz", "Eşikleme", "Değer"));
                    }
                    catch { }

                    if (deger >= 0 && deger <= 255)
                        Esikleme(deger);
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    Histogram();
                }
                else if (comboBox1.SelectedIndex == 5)
                {
                    int deger = 0;
                    try
                    {
                        deger = int.Parse(Interaction.InputBox("Kontrast ayarı için '0' ile '+255' arasında bir değer giriniz", "Kontrast", "Değer"));
                    }
                    catch { }

                    if (deger >= 0 && deger <= 255)
                        Kontrast(deger);
                }
                else if (comboBox1.SelectedIndex == 6)
                {
                    Aynala(1);
                }
                else if (comboBox1.SelectedIndex == 7)
                {
                    Aynala(2);
                }
                else if (comboBox1.SelectedIndex == 8)
                {
                    int deger = 0;
                    try
                    {
                        deger = int.Parse(Interaction.InputBox("Döndürme için açı değeri giriniz", "Döndürme", "Değer"));
                    }
                    catch { }

                    //if (deger >= 0 && deger <= 255)
                    Dondurme(deger);
                }
                else if (comboBox1.SelectedIndex == 9)
                {
                    Tasima();
                }
                else if (comboBox1.SelectedIndex == 10)
                {
                    Egme_Kaydirma();
                }
                else if (comboBox1.SelectedIndex == 11)
                {
                    int deger = 0;
                    try
                    {
                        deger = int.Parse(Interaction.InputBox("Küçültme katsayısı giriniz", "Küçültme", "0"));
                    }
                    catch { }

                    if (0 < deger)
                        Kucultme(deger);
                    else
                        MessageBox.Show("Hatalı Giriş");
                }
                else if (comboBox1.SelectedIndex == 12)
                {
                    HistEsitleme();
                }
                else if (comboBox1.SelectedIndex == 13)
                {
                    MeanFiltresi();
                }
                else if (comboBox1.SelectedIndex == 14)
                {
                    MedianFiltresi();
                }
                else if (comboBox1.SelectedIndex == 15)
                {
                    SobelFiltresi();
                }
                else if (comboBox1.SelectedIndex == 16)
                {
                    GaussFiltresi();
                }
                else if (comboBox1.SelectedIndex == 17)
                {
                    Netlestir();

                }
                else if (comboBox1.SelectedIndex == 18)
                {
                    Oteleme();
                }
                else if (comboBox1.SelectedIndex == 19)
                {
                    Kucultme();
                }
                else if (comboBox1.SelectedIndex == 20)
                {
                    Buyutme();
                }
                else if (comboBox1.SelectedIndex == 21)
                {
                    Yayma();
                }
                else if (comboBox1.SelectedIndex == 22)
                {
                    Konvolusyon();
                }
                else if (comboBox1.SelectedIndex == 23)
                {
                    Dondurme();
                }
                else if (comboBox1.SelectedIndex == 24)
                {
                    Prewitt();
                }
                else if (comboBox1.SelectedIndex == 25)
                {
                    Laplace();
                }
            }

            else
            {
                MessageBox.Show("Fotoğraf ekleyiniz!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.DefaultExt = ".jpg";
                openFileDialog1.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                openFileDialog1.ShowDialog();
                String resminYolu = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(resminYolu);


            }
            catch { }
        }

       

       

        public void NegatifAl()
        {
            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı. İçerisine görüntü yüklendi.
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor.
            //Boyutları giriş resmi ile aynı olur. Tanımlaması globalde yapıldı.

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = 255 - OkunanRenk.R;
                    G = 255 - OkunanRenk.G;
                    B = 255 - OkunanRenk.B;

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void GriyeDonustur()
        {
            Color OkunanRenk, DonusenRenk;

            Bitmap GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı. Fonksiyonla gelmedi.
            int ResimYuksekligi = GirisResmi.Height;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor.Boyutları giriş resmi ile aynı olur.
            int GriDeger = 0;

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    double R = OkunanRenk.R;
                    double G = OkunanRenk.G;
                    double B = OkunanRenk.B;
                    //GriDeger = Convert.ToInt16((R + G + B) / 3);

                    GriDeger = Convert.ToInt16(R * 0.3 + G * 0.6 + B * 0.1);

                    DonusenRenk = Color.FromArgb(GriDeger, GriDeger, GriDeger);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void ParlaklikDegistir(int i)
        {
            int R = 0, G = 0, B = 0;
            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor. Boyutları giriş resmi ile aynı olur.

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    //Rengini i değeri ile açacak.
                    R = OkunanRenk.R + i;
                    G = OkunanRenk.G + i;
                    B = OkunanRenk.B + i;
                    //Renkler 255 geçtiyse son sınır olan 255 alınacak.
                    if (R > 255) R = 255;
                    else if (R < 0) R = 0;
                    if (G > 255) G = 255;
                    else if (G < 0) G = 0;
                    if (B > 255) B = 255;
                    else if (B < 0) B = 0;

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void Esikleme(int i)
        {
            int R = 0, G = 0, B = 0;

            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor. Boyutları giriş resmi ile aynı olur.

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    if (OkunanRenk.R >= i)
                        R = 255;
                    else
                        R = 0;

                    if (OkunanRenk.G >= i)
                        G = 255;
                    else
                        G = 0;

                    if (OkunanRenk.B >= i)
                        B = 255;
                    else
                        B = 0;

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void Histogram()
        {
            pictureBox2.Image = null;
            ArrayList DiziPiksel = new ArrayList();

            int OrtalamaRenk = 0;
            Color OkunanRenk;

            Bitmap GirisResmi; //Histogram için giriş resmi gri-ton olmalıdır.
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = GirisResmi.Height;

            for (int x = 0; x < GirisResmi.Width; x++)
            {
                for (int y = 0; y < GirisResmi.Height; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    OrtalamaRenk = (int)(OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3; //Griton resimde üç kanal rengi aynı değere sahiptir.
                    DiziPiksel.Add(OrtalamaRenk); //Resimdeki tüm noktaları diziye atıyor.
                }
            }

            int[] DiziPikselSayilari = new int[256];

            for (int r = 0; r <= 255; r++) //256 tane renk tonu için dönecek.
            {
                int PikselSayisi = 0;

                for (int s = 0; s < DiziPiksel.Count; s++) //resimdeki piksel sayısınca dönecek.
                {
                    if (r == Convert.ToInt16(DiziPiksel[s]))
                        PikselSayisi++;
                }

                DiziPikselSayilari[r] = PikselSayisi;
            }

            //Değerleri listbox'a ekliyor.
            int RenkMaksPikselSayisi = 0; //Grafikte y eksenini ölçeklerken kullanılacak.

            for (int k = 0; k <= 255; k++)
            {
                //Maksimum piksel sayısını bulmaya çalışıyor.

                if (DiziPikselSayilari[k] > RenkMaksPikselSayisi)
                {
                    RenkMaksPikselSayisi = DiziPikselSayilari[k];
                }
            }

            //Grafiği çiziyor.
            Graphics CizimAlani;

            Pen Kalem1 = new Pen(System.Drawing.Color.Yellow, 1);
            Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);

            CizimAlani = pictureBox2.CreateGraphics();

            pictureBox2.Refresh();

            int GrafikYuksekligi = 300;

            double OlcekY = RenkMaksPikselSayisi / GrafikYuksekligi;
            double OlcekX = 1.5;

            int X_kaydirma = 10;

            for (int x = 0; x <= 255; x++)
            {
                if (x % 50 == 0)
                    CizimAlani.DrawLine(Kalem2, (int)(X_kaydirma + x * OlcekX),
                   GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX), 0);
                CizimAlani.DrawLine(Kalem1, (int)(X_kaydirma + x * OlcekX), GrafikYuksekligi,
               (int)(X_kaydirma + x * OlcekX), (GrafikYuksekligi - (int)(DiziPikselSayilari[x] / OlcekY)));
                //Dikey kırmızı çizgiler.
            }
        }

        public void Kontrast(int i)
        {
            int R = 0, G = 0, B = 0;
            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor. Boyutları  giriş resmi ile aynı olur.


            double F_KontrastFaktoru = (259 * (i + 255)) / (255 * (259 - i));

            for (int x = 0; x < ResimGenisligi; x++)
            {

                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;

                    R = (int)((F_KontrastFaktoru * (R - 128)) + 128);
                    G = (int)((F_KontrastFaktoru * (G - 128)) + 128);
                    B = (int)((F_KontrastFaktoru * (B - 128)) + 128);

                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (R > 255) R = 255;
                    else if (R < 0) R = 0;

                    if (G > 255) G = 255;
                    else if (G < 0) G = 0;

                    if (B > 255) B = 255;
                    else if (B < 0) B = 0;

                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }

            pictureBox2.Image = CikisResmi;
        }

        public void Aynala(int i)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            double x2 = 0, y2 = 0;

            //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.
            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;

            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    if (i == 1)
                    {
    
                        y2 = Convert.ToInt16(y1);
                    }

                    else if (i == 2)
                    {
                        x2 = Convert.ToInt16(x1);
                        y2 = Convert.ToInt16(-y1 + 2 * y0);
                    }

                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public void Dondurme(int i)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int Aci = i;
            double RadyanAci = Aci * 2 * Math.PI / 360;

            double x2 = 0, y2 = 0;
            //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.

            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;

            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    //Döndürme Formülleri
                    x2 = Math.Cos(RadyanAci) * (x1 - x0) - Math.Sin(RadyanAci) * (y1 - y0) + x0;
                    y2 = Math.Sin(RadyanAci) * (x1 - x0) + Math.Cos(RadyanAci) * (y1 - y0) + y0;
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }

            pictureBox2.Image = CikisResmi;
        }

        public void Tasima()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double x2 = 0, y2 = 0;
            //Taşıma mesafelerini atıyor.
            int Tx = 100;
            int Ty = 50;
            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    x2 = x1 + Tx;
                    y2 = y1 + Ty;
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;

        }

        public void Egme_Kaydirma()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            //Taşıma mesafelerini atıyor. 
            double EgmeKatsayisi = 0.2;
            double x2 = 0, y2 = 0;
            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    // +X ekseni yönünde
                    //x2 = x1 + EgmeKatsayisi * y1;
                    //y2 = y1;
                    // -X ekseni yönünde
                    //x2 = x1 - EgmeKatsayisi * y1;
                    //y2 = y1;
                    // +Y ekseni yönünde
                    //x2 = x1;
                    //y2 = EgmeKatsayisi * x1 + y1;
                    // -Y ekseni yönünde
                    x2 = x1;
                    y2 = -EgmeKatsayisi * x1 + y1;

                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public void Kucultme(int i)
        {
            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x2 = 0, y2 = 0; //Çıkış resminin x ve y si olacak.
            int KucultmeKatsayisi = i;
            for (int x1 = 0; x1 < ResimGenisligi; x1 = x1 + KucultmeKatsayisi)
            {
                y2 = 0;
                for (int y1 = 0; y1 < ResimYuksekligi; y1 = y1 + KucultmeKatsayisi)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    DonusenRenk = OkunanRenk;
                    CikisResmi.SetPixel(x2, y2, DonusenRenk);
                    y2++;
                }
                x2++;
            }
            pictureBox2.Image = CikisResmi;
        }

        public void HistEsitleme()
        {
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            int[] HistogramRed = new int[256];
            int[] HistogramGreen = new int[256];
            int[] HistogramBlue = new int[256];
            int[] HistogramRedK = new int[256];
            int[] HistogramGreenK = new int[256];
            int[] HistogramBlueK = new int[256];
            int[] YüzdelikRed = new int[256];
            int[] YüzdelikGreen = new int[256];
            int[] YüzdelikBlue = new int[256];

            uint pixels = (uint)ResimYuksekligi * (uint)ResimGenisligi;
            decimal Const = 255 / (decimal)pixels;

            int x, y, R, G, B;


            int[] HistogramRed2 = new int[256];
            int[] HistogramGreen2 = new int[256];
            int[] HistogramBlue2 = new int[256];


            for (var i = 0; i < ResimGenisligi; i++)
            {
                for (var j = 0; j < ResimYuksekligi; j++)
                {
                    var piksel = GirisResmi.GetPixel(i, j);

                    HistogramRed2[(int)piksel.R]++;
                    HistogramGreen2[(int)piksel.G]++;
                    HistogramBlue2[(int)piksel.B]++;

                }
            }

            int[] cdfR = HistogramRed2;
            int[] cdfG = HistogramGreen2;
            int[] cdfB = HistogramBlue2;

            for (int r = 1; r <= 255; r++)
            {
                cdfR[r] = cdfR[r] + cdfR[r - 1];
                cdfG[r] = cdfG[r] + cdfG[r - 1];
                cdfB[r] = cdfB[r] + cdfB[r - 1];
            }

            for (y = 0; y < ResimYuksekligi; y++)
            {
                for (x = 0; x < ResimGenisligi; x++)
                {
                    Color pixelColor = GirisResmi.GetPixel(x, y);

                    R = (int)((decimal)cdfR[pixelColor.R] * Const);
                    G = (int)((decimal)cdfG[pixelColor.G] * Const);
                    B = (int)((decimal)cdfB[pixelColor.B] * Const);

                    Color newColor = Color.FromArgb(R, G, B);
                    GirisResmi.SetPixel(x, y, newColor);
                    pictureBox2.Image = GirisResmi;
                }
            }
        }

        public void MeanFiltresi()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 5;
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R;
                            toplamG = toplamG + OkunanRenk.G;
                            toplamB = toplamB + OkunanRenk.B;
                        }
                    }
                    ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                    ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                    ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public void MedianFiltresi()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = Convert.ToInt32(5);
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int[] R = new int[ElemanSayisi];
            int[] G = new int[ElemanSayisi];
            int[] B = new int[ElemanSayisi];
            int[] Gri = new int[ElemanSayisi];
            int x, y, i, j;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R[k] = OkunanRenk.R;
                            G[k] = OkunanRenk.G;
                            B[k] = OkunanRenk.B;
                            Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114); //Gri ton formülü                    
                            k++;
                        }
                    }
                    //Gri tona göre sıralama yapıyor. Aynı anda üç rengide değiştiriyor.
                    int GeciciSayi = 0;
                    for (i = 0; i < ElemanSayisi; i++)
                    {
                        for (j = i + 1; j < ElemanSayisi; j++)
                        {
                            if (Gri[j] < Gri[i])
                            {
                                GeciciSayi = Gri[i];
                                Gri[i] = Gri[j];
                                Gri[j] = GeciciSayi;
                                GeciciSayi = R[i];
                                R[i] = R[j];
                                R[j] = GeciciSayi;
                                GeciciSayi = G[i];
                                G[i] = G[j];
                                G[j] = GeciciSayi;
                                GeciciSayi = B[i];
                                B[i] = B[j];
                                B[j] = GeciciSayi;
                            }
                        }
                    }
                    //Sıralama sonrası ortadaki değeri çıkış resminin piksel değeri olarak atıyor.
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) /
                   2], B[(ElemanSayisi - 1) / 2]));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public void SobelFiltresi()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmiX, CikisResmiY, CikisResmiXY;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmiX = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiXY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j;
            int Gri = 0;
            int[] MatrisX = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
            int[] MatrisY = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
            int RenkX, RenkY, RenkXY;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    int toplamGriX = 0, toplamGriY = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            Gri = (OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3;
                            toplamGriX = toplamGriX + Gri * MatrisX[k];
                            toplamGriY = toplamGriY + Gri * MatrisY[k];
                            k++;
                        }
                    }
                    RenkX = Math.Abs(toplamGriX);
                    RenkY = Math.Abs(toplamGriY);
                    RenkXY = Math.Abs(toplamGriX) + Math.Abs(toplamGriY);
                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (RenkX > 255) RenkX = 255;
                    if (RenkY > 255) RenkY = 255;
                    if (RenkXY > 255) RenkXY = 255;
                    if (RenkX < 0) RenkX = 0;
                    if (RenkY < 0) RenkY = 0;
                    if (RenkXY < 0) RenkXY = 0;
                    //===========================================================
                    CikisResmiX.SetPixel(x, y, Color.FromArgb(RenkX, RenkX, RenkX));
                    CikisResmiY.SetPixel(x, y, Color.FromArgb(RenkY, RenkY, RenkY));
                    CikisResmiXY.SetPixel(x, y, Color.FromArgb(RenkXY, RenkXY, RenkXY));
                }
            }
            //pictureBox2.Image = CikisResmiY;
            //pictureBox3.Image = CikisResmiX;
            pictureBox2.Image = CikisResmiXY;
        }

        public void GaussFiltresi()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;

            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 5; //Çekirdek matrisin boyutu
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;

            int[] Matris = { 1, 4, 7, 4, 1, 4, 20, 33, 20, 4, 7, 33, 55, 33, 7, 4, 20, 33, 20, 4, 1, 4, 7, 4, 1 };
            int MatrisToplami = 1 + 4 + 7 + 4 + 1 + 4 + 20 + 33 + 20 + 4 + 7 + 33 + 55 + 33 + 7 + 4 + 20 +
           33 + 20 + 4 + 1 + 4 + 7 + 4 + 1; for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                        ortalamaR = toplamR / MatrisToplami;
                        ortalamaG = toplamG / MatrisToplami;
                        ortalamaB = toplamB / MatrisToplami;
                        CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                    }
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public void Netlestir()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB;
            int R, G, B;
            int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };
            int MatrisToplami = 0 + -2 + 0 + -2 + 11 + -2 + 0 + -2 + 0;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    R = toplamR / MatrisToplami;
                    G = toplamG / MatrisToplami;
                    B = toplamB / MatrisToplami;
                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    //===========================================================
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            pictureBox2.Image = CikisResmi;
        }
        public void Oteleme()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double x2 = 0, y2 = 0;
            //Taşıma mesafelerini atıyor.
            int Tx = 100;
            int Ty = 50;
            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    x2 = x1 + Tx;
                    y2 = y1 + Ty;
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }
        public void Kucultme()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x2 = 0, y2 = 0; //Çıkış resminin x ve y si olacak.
            int KucultmeKatsayisi = 2;
            for (int x1 = 0; x1 < ResimGenisligi; x1 = x1 + KucultmeKatsayisi)
            {
                y2 = 0;
                for (int y1 = 0; y1 < ResimYuksekligi; y1 = y1 + KucultmeKatsayisi)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    CikisResmi.SetPixel(x2, y2, OkunanRenk);
                    y2++;
                }
                x2++;
            }
            pictureBox2.Image = CikisResmi;
        }
        public void Buyutme()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int BuyutmeKatsayisi = 2;

            int x2 = 0, y2 = 0;
            for (int x1 = 0; x1 < ResimGenisligi; x1++)
            {

                for (int y1 = 0; y1 < ResimYuksekligi; y1++)
                {

                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    for (int i = 0; i < BuyutmeKatsayisi; i++)
                    {
                        for (int j = 0; j < BuyutmeKatsayisi; j++)
                        {
                            x2 = x1 * BuyutmeKatsayisi + i;
                            y2 = y1 * BuyutmeKatsayisi + j;

                            if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                                CikisResmi.SetPixel(x2, y2, OkunanRenk);
                        }
                    }

                }

            }
            pictureBox2.Image = CikisResmi;
        }

        public void Yayma()
        {
            Bitmap GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int Sablon = 3;
            for (int x = (Sablon - 1) / 2; x < ResimGenisligi - (Sablon - 1) / 2; x++)
            {
                for (int y = (Sablon - 1) / 2; y < ResimYuksekligi - (Sablon - 1) / 2; y++)
                {
                    bool BeyazKontrol = false;
                    for (int i = -((Sablon - 1) / 2); i <= (Sablon - 1) / 2; i++)
                    {
                        for (int j = -((Sablon - 1) / 2); j <= (Sablon - 1) / 2; j++)
                        {
                            Color p = GirisResmi.GetPixel(x + i, y + j);
                            int ortalama = (p.R + p.G + p.B) / 3;
                            if (ortalama == 255)
                                BeyazKontrol = true;
                        }
                    }
                    if (BeyazKontrol)
                    {
                        CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        Color MevcutRenk = GirisResmi.GetPixel(x, y);
                        CikisResmi.SetPixel(x, y, Color.FromArgb(MevcutRenk.R, MevcutRenk.G, MevcutRenk.B));
                    }
                }
            }
            pictureBox2.Image = CikisResmi;


        }



        public void Konvolusyon()
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB;
            int R, G, B;
            int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };
            int MatrisToplami = 0 + -2 + 0 + -2 + 11 + -2 + 0 + -2 + 0;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)

                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    R = toplamR / MatrisToplami;
                    G = toplamG / MatrisToplami;
                    B = toplamB / MatrisToplami;
                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    //===========================================================
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            pictureBox2.Image = CikisResmi;

        }
        int sayac = 0;
        public void Dondurme()
        {
            sayac++;
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int Aci = 90 * sayac;
            double RadyanAci = Aci * 2 * Math.PI / 360;
            double x2 = 0, y2 = 0;
            //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.
            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;

            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    //Döndürme Formülleri
                    x2 = Math.Cos(RadyanAci) * (x1 - x0) - Math.Sin(RadyanAci) * (y1 - y0) + x0;
                    y2 = Math.Sin(RadyanAci) * (x1 - x0) + Math.Cos(RadyanAci) * (y1 - y0) + y0;
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        public void Prewitt()
        {
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y;
            Color Renk;
            int P1, P2, P3, P4, P5, P6, P7, P8, P9;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    Renk = GirisResmi.GetPixel(x - 1, y - 1);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y - 1);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y - 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y);
                    P5 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y);
                    P6 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y + 1);
                    P7 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y + 1);
                    P8 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y + 1);
                    P9 = (Renk.R + Renk.G + Renk.B) / 3;
                    int Gx = Math.Abs(-P1 + P3 - P4 + P6 - P7 + P9); //Dikey çizgileri Bulur
                    int Gy = Math.Abs(P1 + P2 + P3 - P7 - P8 - P9); //Yatay Çizgileri Bulur.
                    int PrewittDegeri = 0;
                    PrewittDegeri = Gx;
                    PrewittDegeri = Gy;
                    PrewittDegeri = Gx + Gy; //1. Formül
                                             //PrewittDegeri = Convert.ToInt16(Math.Sqrt(Gx * Gx + Gy * Gy)); //2.Formül
                                             //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (PrewittDegeri > 255) PrewittDegeri = 255;
                    //Eşikleme: Örnek olarak 100 değeri kullanıldı.
                    //if (PrewittDegeri > 100)
                    //PrewittDegeri = 255;
                    //else
                    //PrewittDegeri = 0;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(PrewittDegeri, PrewittDegeri, PrewittDegeri));
                }
            }
            pictureBox2.Image = CikisResmi;
        }
        public void Laplace()
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap image = new Bitmap(img);
            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    Color color2, color4, color5, color6, color8;
                    color2 = image.GetPixel(x, y - 1);
                    color4 = image.GetPixel(x - 1, y);
                    color5 = image.GetPixel(x, y);
                    color6 = image.GetPixel(x + 1, y);
                    color8 = image.GetPixel(x, y + 1);
                    int r = color2.R + color4.R + color5.R * (-4) + color6.R + color8.R;
                    int g = color2.G + color4.G + color5.G * (-4) + color6.G + color8.G;
                    int b = color2.B + color4.B + color5.B * (-4) + color6.B + color8.B;




                    int avg = (r + g + b) / 3;
                    if (avg > 255) avg = 255;
                    if (avg < 0) avg = 0;
                    image.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            pictureBox2.Image = image;


        }

        private void görüntüyüAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                pictureBox1.Image = pictureBox2.Image;
                pictureBox2.Image = null;
            }
        }
        public void ResmiKaydet()
        {
            if (pictureBox2.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG|*.png" };
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox2.Image.Save(saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox2.Image = pictureBox1.Image;
                    ResmiKaydet();
                }
                else
                    MessageBox.Show("Fotoğraf yok!");
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResmiKaydet();
        }
        public void DosyaAc()
        {
            try
            {
                openFileDialog1.DefaultExt = ".jpg";
                openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                String ResminYolu = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(ResminYolu);
            }
            catch { }
        }

        private void yeniButton_Click(object sender, EventArgs e)
        {
            DosyaAc();
        }
    }

}


