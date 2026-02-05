using System;
using System.Collections.Generic;
using personelTakip.DAL;
using personelTakip.Entities;

namespace personelTakip.BLL
{
    public class MaasYoneticisi
    {
        MaasDeposu depo = new MaasDeposu();

        public List<Maas> TumunuGetir()
        {
            return depo.Listele();
        }

        public int Ekle(Maas m)
        {
            // Kurallar
            if (m.Tutar <= 0)
                throw new Exception("Maaş tutarı 0 veya negatif olamaz!");

            if (m.Ay < 1 || m.Ay > 12)
                throw new Exception("Geçersiz ay bilgisi!");

            return depo.Ekle(m);
        }

        public int Guncelle(Maas m)
        {
            if (m.Tutar <= 0)
                throw new Exception("Maaş tutarı 0 veya negatif olamaz!");

            return depo.Guncelle(m);
        }

        public int Sil(int id)
        {
            return depo.Sil(id);
        }
    }
}