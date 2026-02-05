using System;
using System.Collections.Generic;
using personelTakip.DAL;
using personelTakip.Entities;

namespace personelTakip.BLL
{
    public class IzinYoneticisi
    {
        IzinDeposu depo = new IzinDeposu();

        public List<Izin> TumunuGetir()
        {
            return depo.Listele();
        }

        public int Ekle(Izin i)
        {

            // 1. Tarih Kontrolü
            if (i.BitisTarihi < i.BaslangicTarihi)
            {
                throw new Exception("İzin bitiş tarihi, başlangıç tarihinden önce olamaz!");
            }

            // 2. Açıklama Kontrolü
            if (string.IsNullOrEmpty(i.Aciklama))
            {
                throw new Exception("Lütfen izin için bir açıklama giriniz (Örn: Yıllık İzin).");
            }

            return depo.Ekle(i);
        }
        public int Sil( int id)
        {
            return depo.Sil(id);
        }
        public int Guncelle(Izin i)
        {
            // Bitiş tarihi başlangıçtan önce olamaz
            if (i.BitisTarihi < i.BaslangicTarihi)
            {
                throw new Exception("Bitiş tarihi başlangıç tarihinden önce olamaz!");
            }
            int etkilenenSatir = depo.Guncelle(i);
            if (etkilenenSatir == 0)
            {
                throw new Exception("Güncelleme yapılamadı! ID numarası bulunamadı veya veri değişmedi.");
            }
            return etkilenenSatir;

            return depo.Guncelle(i);
        }
    }
}