using System;

namespace personelTakip.Entities
{
    public class Maas : TemelVarlik
    {
        public int PersonelId { get; set; }
        public int Ay { get; set; }
        public int Yil { get; set; }
        public decimal Tutar { get; set; }
        public string Durum { get; set; } // ödendi-bekliyor

        //  ekstra
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
    }
}