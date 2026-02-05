using System;

namespace personelTakip.Entities
{
    
    // her tabloda ıd olacak her sınıfta tekrar yazmamak için
    //personel departman izin buna bağlı (base class)
    //her tabloya tek tek get set yazmak yerine buraya bi kere yazıldı
    public class TemelVarlik
    {
        public int Id { get; set; }
    }
}
