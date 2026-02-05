using System;

namespace personelTakip.Entities
{
    public class Izin : TemelVarlik
    {
        public int PersonelId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; }
        public string Durum { get; set; }

        // Ekstra Özellikler 
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
    }
}