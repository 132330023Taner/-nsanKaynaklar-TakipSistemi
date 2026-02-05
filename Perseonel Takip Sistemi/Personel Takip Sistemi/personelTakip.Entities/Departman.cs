using System;
using System.Security;

namespace personelTakip.Entities
{
    public class Departman : TemelVarlik
    {
        //veri taşıyıcısı
        public string Ad { get; set; }
        public string Aciklama { get; set; }

        public override string ToString()
        {
            return Ad; // Listede sadece adını göstermek için
        }
    }
}