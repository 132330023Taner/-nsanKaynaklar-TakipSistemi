-- Personel Takip Sistemi - Temiz Veritabanı Şeması
-- Bu dosya sadece tabloları oluşturur, veri içermez.
-- Varsayılan admin veya departmanları eklemek için dosyanın sonundaki yorum satırlarını kullanabilirsiniz.

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+03:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `personeltakip`
--

-- --------------------------------------------------------

--
-- Tablo yapısı: `admins`
--

CREATE TABLE `admins` (
  `id` int NOT NULL,
  `kullanici_adi` varchar(50) DEFAULT NULL,
  `sifre` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tablo yapısı: `departments`
--

CREATE TABLE `departments` (
  `id` int NOT NULL,
  `ad` varchar(100) NOT NULL,
  `aciklama` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tablo yapısı: `employees`
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

-- --------------------------------------------------------

--
-- Tablo yapısı: `performances`
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
-- Tablo yapısı: `permissions`
--

CREATE TABLE `permissions` (
  `id` int NOT NULL,
  `personel_id` int DEFAULT NULL,
  `baslangic_tarihi` datetime DEFAULT NULL,
  `bitis_tarihi` datetime DEFAULT NULL,
  `aciklama` text,
  `durum` varchar(50) DEFAULT 'Onay Bekliyor'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tablo yapısı: `salaries`
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
-- İndeksler ve Anahtarlar
--

-- `admins`
ALTER TABLE `admins`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `kullanici_adi` (`kullanici_adi`);

-- `departments`
ALTER TABLE `departments`
  ADD PRIMARY KEY (`id`);

-- `employees`
ALTER TABLE `employees`
  ADD PRIMARY KEY (`id`),
  ADD KEY `departman_id` (`departman_id`);

-- `performances`
ALTER TABLE `performances`
  ADD PRIMARY KEY (`id`),
  ADD KEY `personel_id` (`personel_id`);

-- `permissions`
ALTER TABLE `permissions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `personel_id` (`personel_id`);

-- `salaries`
ALTER TABLE `salaries`
  ADD PRIMARY KEY (`id`),
  ADD KEY `personel_id` (`personel_id`);

--
-- AUTO_INCREMENT Ayarları
--

ALTER TABLE `admins` MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
ALTER TABLE `departments` MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
ALTER TABLE `employees` MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
ALTER TABLE `performances` MODIFY `id` int NOT NULL AUTO_INCREMENT;
ALTER TABLE `permissions` MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
ALTER TABLE `salaries` MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- İlişkiler (Constraints)
--

ALTER TABLE `employees`
  ADD CONSTRAINT `employees_ibfk_1` FOREIGN KEY (`departman_id`) REFERENCES `departments` (`id`);

ALTER TABLE `performances`
  ADD CONSTRAINT `performances_ibfk_1` FOREIGN KEY (`personel_id`) REFERENCES `employees` (`id`);

ALTER TABLE `permissions`
  ADD CONSTRAINT `permissions_ibfk_1` FOREIGN KEY (`personel_id`) REFERENCES `employees` (`id`) ON DELETE CASCADE;

ALTER TABLE `salaries`
  ADD CONSTRAINT `salaries_ibfk_1` FOREIGN KEY (`personel_id`) REFERENCES `employees` (`id`) ON DELETE CASCADE;

COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

-- -----------------------------------------------------
-- VARSAYILAN VERİLER (İsteğe bağlı - Yorum satırlarını kaldırarak kullanabilirsiniz)
-- -----------------------------------------------------

-- Varsayılan Admin Hesabı (Kullanıcı adı: admin, Şifre: 1234)
-- INSERT INTO `admins` (`id`, `kullanici_adi`, `sifre`) VALUES (1, 'admin', '1234');

-- Varsayılan Departmanlar
-- INSERT INTO `departments` (`id`, `ad`, `aciklama`) VALUES
-- (1, 'Yönetim', 'Yöneticiler'),
-- (2, 'İnsan Kaynakları', 'Personel işlemleri'),
-- (3, 'Muhasebe', 'Finans işlemleri'),
-- (4, 'Bilgi İşlem', 'Yazılım ve Donanım işlemleri');
