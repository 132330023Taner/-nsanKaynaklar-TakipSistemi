using System;

namespace personelTakip.Entities
{
    public class Performans
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public string PersonelAdSoyad { get; set; }

        public int Puan { get; set; }
        public string Aciklama { get; set; }
        public DateTime DegerlendirmeTarihi { get; set; }
    }
}