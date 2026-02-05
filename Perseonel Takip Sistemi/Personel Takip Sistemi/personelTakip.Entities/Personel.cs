using System;

namespace personelTakip.Entities
{
    public class Personel : TemelVarlik
    {
        //veri taşıyıcıları
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcNo { get; set; }
        public decimal Maas { get; set; }
        public int DepartmanId { get; set; }
        public int RolId { get; set; } 
        public string Sifre { get; set; }
        public DateTime IseGirisTarihi { get; set; }

        
        public string AdSoyad
        {
            get { return Ad + " " + Soyad; }
        }
    }
}
