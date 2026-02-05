-- phpMyAdmin SQL Dump
-- version 5.2.1deb3
-- https://www.phpmyadmin.net/
--
-- Anamakine: localhost:3306
-- Üretim Zamanı: 14 Oca 2026, 18:25:19
-- Sunucu sürümü: 8.0.44-0ubuntu0.24.04.1
-- PHP Sürümü: 8.3.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `26_132430040`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `admins`
--

CREATE TABLE `admins` (
  `id` int NOT NULL,
  `kullanici_adi` varchar(50) DEFAULT NULL,
  `sifre` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `admins`
--

INSERT INTO `admins` (`id`, `kullanici_adi`, `sifre`) VALUES
(1, 'admin', '1234');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `departments`
--

CREATE TABLE `departments` (
  `id` int NOT NULL,
  `ad` varchar(100) NOT NULL,
  `aciklama` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `departments`
--

INSERT INTO `departments` (`id`, `ad`, `aciklama`) VALUES
(4, 'Yönetim', 'Yöneticiler'),
(5, 'İnsan Kaynakları', 'Personel işlemleri'),
(6, 'Muhasebe', 'Finans işlemleri'),
(7, 'Satış ve Pazarlama', 'Genel Personel'),
(8, 'Bilgi İşlem', 'Yazılım ve Donanım işlemleri'),
(9, 'Güvenlik', 'Şirket Güvenliği');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `employees`
--

CREATE TABLE `employees` (
  `id` int NOT NULL,
  `ad` varchar(50) NOT NULL,
  `soyad` varchar(50) NOT NULL,
  `tc_no` varchar(11) NOT NULL,
  `departman_id` int DEFAULT NULL,
  `maas` decimal(10,2) NOT NULL,
  `rol_id` int DEFAULT '3',
  `sifre` varchar(50) NOT NULL,
  `ise_giris_tarihi` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `employees`
--

INSERT INTO `employees` (`id`, `ad`, `soyad`, `tc_no`, `departman_id`, `maas`, `rol_id`, `sifre`, `ise_giris_tarihi`) VALUES
(14, 'Selin', 'Çalık', '20202020202', 5, 65000.00, 0, '1234', '2023-02-24 15:15:48'),
(15, 'Sıla', 'Suna', '30303030303', 6, 85000.00, 0, '1234', '2023-04-14 15:15:48'),
(16, 'İrem', 'Afşar', '40404040404', 5, 65000.00, 0, '1234', '2023-04-14 15:15:48'),
(17, 'Dilay', 'Yenihan', '90909090909', 6, 70000.00, 0, '1234', '2024-04-10 15:15:48'),
(18, 'Emel', 'Özkan', '21212121212', 7, 90000.00, 0, '1234', '2020-02-01 15:15:48'),
(19, 'Elif', 'Karaman', '50505050505', 8, 125000.00, 0, '1234', '2025-04-14 15:15:48'),
(20, 'Mustafa', 'Özkan', '60606060606', 9, 55000.00, 0, '1234', '2026-01-01 22:06:33'),
(21, 'Mustafa', 'Aydın', '70707070707', 7, 100000.00, 0, '1234', '2026-01-05 15:15:48'),
(22, 'Yağmur', 'Özkan', '10101010101', 4, 500000.00, 0, '1234', '2020-01-05 15:15:48'),
(23, 'Hira', 'Kalkanlı', '80808080808', 8, 115000.00, 0, '1234', '2025-04-14 15:15:48'),
(24, 'Melike', 'Arı', '14141414141', 8, 115000.00, 0, '1234', '2023-04-14 15:15:48'),
(25, 'Utku', 'Dağ', '99999999999', 7, 65000.00, 0, '1234', '2023-02-24 15:15:48');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `performances`
--

CREATE TABLE `performances` (
  `id` int NOT NULL,
  `personel_id` int DEFAULT NULL,
  `puan` int NOT NULL,
  `aciklama` text,
  `degerlendirme_tarihi` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `permissions`
--

CREATE TABLE `permissions` (
  `id` int NOT NULL,
  `personel_id` int DEFAULT NULL,
  `baslangic_tarihi` datetime DEFAULT NULL,
  `bitis_tarihi` datetime DEFAULT NULL,
  `aciklama` text,
  `durum` varchar(50) DEFAULT 'Onay Bekliyor'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `permissions`
--

INSERT INTO `permissions` (`id`, `personel_id`, `baslangic_tarihi`, `bitis_tarihi`, `aciklama`, `durum`) VALUES
(2, 15, '2026-01-11 22:22:34', '2026-01-13 22:22:34', 'hastalık', 'Onaylandı');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `salaries`
--

CREATE TABLE `salaries` (
  `id` int NOT NULL,
  `personel_id` int DEFAULT NULL,
  `ay` int DEFAULT NULL,
  `yil` int DEFAULT NULL,
  `tutar` decimal(10,2) DEFAULT NULL,
  `durum` varchar(50) DEFAULT 'Ödendi'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `salaries`
--

INSERT INTO `salaries` (`id`, `personel_id`, `ay`, `yil`, `tutar`, `durum`) VALUES
(2, 14, 1, 2025, 65000.00, 'Ödendi'),
(3, 15, 1, 2025, 85000.00, 'Ödendi'),
(4, 16, 1, 2025, 65000.00, 'Ödendi'),
(5, 17, 1, 2025, 70000.00, 'Ödendi'),
(6, 18, 1, 2025, 90000.00, 'Ödendi'),
(7, 19, 1, 2025, 125000.00, 'Ödendi'),
(8, 20, 1, 2025, 55000.00, 'Ödendi'),
(9, 21, 1, 2025, 70000.00, 'Ödendi'),
(10, 22, 1, 2025, 500000.00, 'Ödendi'),
(11, 23, 1, 2025, 115000.00, 'Ödendi'),
(12, 24, 1, 2025, 115000.00, 'Ödendi');

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `kullanici_adi` (`kullanici_adi`);

--
-- Tablo için indeksler `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`id`);

--
-- Tablo için indeksler `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`id`),
  ADD KEY `departman_id` (`departman_id`);

--
-- Tablo için indeksler `performances`
--
ALTER TABLE `performances`
  ADD PRIMARY KEY (`id`),
  ADD KEY `personel_id` (`personel_id`);

--
-- Tablo için indeksler `permissions`
--
ALTER TABLE `permissions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `personel_id` (`personel_id`);

--
-- Tablo için indeksler `salaries`
--
ALTER TABLE `salaries`
  ADD PRIMARY KEY (`id`),
  ADD KEY `personel_id` (`personel_id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `admins`
--
ALTER TABLE `admins`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Tablo için AUTO_INCREMENT değeri `departments`
--
ALTER TABLE `departments`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Tablo için AUTO_INCREMENT değeri `employees`
--
ALTER TABLE `employees`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- Tablo için AUTO_INCREMENT değeri `performances`
--
ALTER TABLE `performances`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- Tablo için AUTO_INCREMENT değeri `permissions`
--
ALTER TABLE `permissions`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Tablo için AUTO_INCREMENT değeri `salaries`
--
ALTER TABLE `salaries`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Dökümü yapılmış tablolar için kısıtlamalar
--

--
-- Tablo kısıtlamaları `employees`
--
ALTER TABLE `employees`
  ADD CONSTRAINT `employees_ibfk_1` FOREIGN KEY (`departman_id`) REFERENCES `departments` (`id`);

--
-- Tablo kısıtlamaları `performances`
--
ALTER TABLE `performances`
  ADD CONSTRAINT `performances_ibfk_1` FOREIGN KEY (`personel_id`) REFERENCES `employees` (`id`);

--
-- Tablo kısıtlamaları `permissions`
--
ALTER TABLE `permissions`
  ADD CONSTRAINT `permissions_ibfk_1` FOREIGN KEY (`personel_id`) REFERENCES `employees` (`id`) ON DELETE CASCADE;

--
-- Tablo kısıtlamaları `salaries`
--
ALTER TABLE `salaries`
  ADD CONSTRAINT `salaries_ibfk_1` FOREIGN KEY (`personel_id`) REFERENCES `employees` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
