using System.Collections.Generic;
using personelTakip.DAL;
using personelTakip.Entities;

namespace personelTakip.BLL
{
    public class PerformansYoneticisi
    {
        PerformansDeposu depo = new PerformansDeposu();

        public void Ekle(Performans p)
        {
            depo.Ekle(p);
        }

        public void Sil(int id)
        {
            depo.Sil(id);
        }

        public List<Performans> TumunuGetir()
        {
            return depo.TumunuGetir();
        }
    }
}