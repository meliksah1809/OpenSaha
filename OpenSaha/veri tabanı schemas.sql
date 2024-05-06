-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: opensaha
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cafes`
--

DROP TABLE IF EXISTS `cafes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cafes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Baslik` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Fiyat` double NOT NULL,
  `Adet` int NOT NULL,
  `Tarih` datetime(6) NOT NULL,
  `GuncellemeTarih` datetime(6) NOT NULL,
  `Stoklu` tinyint(1) NOT NULL,
  `SahaId` int NOT NULL,
  `Barkod` bigint NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Cafes_SahaId` (`SahaId`),
  KEY `IX_Cafes_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Cafes_Sahas_SahaId` FOREIGN KEY (`SahaId`) REFERENCES `sahas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Cafes_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cafetakips`
--

DROP TABLE IF EXISTS `cafetakips`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cafetakips` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RezervasyonId` int NOT NULL,
  `CafeId` int NOT NULL,
  `Adet` int NOT NULL,
  `Ucret` double NOT NULL,
  `Barkod` bigint NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_CafeTakips_CafeId` (`CafeId`),
  KEY `IX_CafeTakips_RezervasyonId` (`RezervasyonId`),
  KEY `IX_CafeTakips_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_CafeTakips_Cafes_CafeId` FOREIGN KEY (`CafeId`) REFERENCES `cafes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CafeTakips_Rezervasyons_RezervasyonId` FOREIGN KEY (`RezervasyonId`) REFERENCES `rezervasyons` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CafeTakips_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ekipmanlars`
--

DROP TABLE IF EXISTS `ekipmanlars`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ekipmanlars` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SahaId` int NOT NULL,
  `Baslik` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Resim` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Aciklama` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Ucret` double NOT NULL,
  `Adet` double NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Ekipmanlars_SahaId` (`SahaId`),
  KEY `IX_Ekipmanlars_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Ekipmanlars_Sahas_SahaId` FOREIGN KEY (`SahaId`) REFERENCES `sahas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Ekipmanlars_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ekipmanrezervasyons`
--

DROP TABLE IF EXISTS `ekipmanrezervasyons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ekipmanrezervasyons` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RezervasyonId` int NOT NULL,
  `EkipmanId` int NOT NULL,
  `Ucret` double NOT NULL,
  `Adet` double NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_EkipmanRezervasyons_EkipmanId` (`EkipmanId`),
  KEY `IX_EkipmanRezervasyons_RezervasyonId` (`RezervasyonId`),
  KEY `IX_EkipmanRezervasyons_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_EkipmanRezervasyons_Ekipmanlars_EkipmanId` FOREIGN KEY (`EkipmanId`) REFERENCES `ekipmanlars` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_EkipmanRezervasyons_Rezervasyons_RezervasyonId` FOREIGN KEY (`RezervasyonId`) REFERENCES `rezervasyons` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_EkipmanRezervasyons_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `eslesmes`
--

DROP TABLE IF EXISTS `eslesmes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `eslesmes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EvSahibi` int NOT NULL,
  `Deplasman` int DEFAULT NULL,
  `Sahalar` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MacTarihi` datetime(6) NOT NULL,
  `OlusturmaTarihi` datetime(6) NOT NULL,
  `AktifSure` datetime(6) NOT NULL,
  `OnayDurum` int NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Eslesmes_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Eslesmes_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ilcelistes`
--

DROP TABLE IF EXISTS `ilcelistes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ilcelistes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IlceAdi` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SehirId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_IlceListes_SehirId` (`SehirId`),
  CONSTRAINT `FK_IlceListes_SehirListes_SehirId` FOREIGN KEY (`SehirId`) REFERENCES `sehirlistes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=974 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kullanicipuanislemleris`
--

DROP TABLE IF EXISTS `kullanicipuanislemleris`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kullanicipuanislemleris` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `KullaniciId` int NOT NULL,
  `IslemTipi` int NOT NULL,
  `Ucret` double NOT NULL,
  `Puan` double DEFAULT NULL,
  `OdemeTipi` int NOT NULL,
  `YoneticiId` int DEFAULT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_KullaniciPuanIslemleris_KullaniciId` (`KullaniciId`),
  KEY `IX_KullaniciPuanIslemleris_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_KullaniciPuanIslemleris_Kullanicis_KullaniciId` FOREIGN KEY (`KullaniciId`) REFERENCES `kullanicis` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_KullaniciPuanIslemleris_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kullanicis`
--

DROP TABLE IF EXISTS `kullanicis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kullanicis` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Isim` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Soyisim` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Telefon` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DogumTarihi` datetime(6) DEFAULT NULL,
  `Puan` double DEFAULT NULL,
  `UserType` int NOT NULL,
  `YoneticiId` int DEFAULT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Kullanicis_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Kullanicis_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `odemes`
--

DROP TABLE IF EXISTS `odemes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `odemes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SahaUcret` double DEFAULT NULL,
  `KafeUcret` double DEFAULT NULL,
  `EkipmanUcret` double DEFAULT NULL,
  `RezervasyonId` int NOT NULL,
  `OdemeTipleri` int NOT NULL,
  `SahaId` int NOT NULL,
  `YoneticiId` int DEFAULT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Odemes_RezervasyonId` (`RezervasyonId`),
  KEY `IX_Odemes_SahaId` (`SahaId`),
  KEY `IX_Odemes_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Odemes_Rezervasyons_RezervasyonId` FOREIGN KEY (`RezervasyonId`) REFERENCES `rezervasyons` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Odemes_Sahas_SahaId` FOREIGN KEY (`SahaId`) REFERENCES `sahas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Odemes_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rezervasyons`
--

DROP TABLE IF EXISTS `rezervasyons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rezervasyons` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SahaId` int NOT NULL,
  `RandevuBaslangic` datetime(6) NOT NULL,
  `RandevuBitis` datetime(6) NOT NULL,
  `KullaniciId` int NOT NULL,
  `EslesmeId` int DEFAULT NULL,
  `Durum` int NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Rezervasyons_EslesmeId` (`EslesmeId`),
  KEY `IX_Rezervasyons_KullaniciId` (`KullaniciId`),
  KEY `IX_Rezervasyons_SahaId` (`SahaId`),
  KEY `IX_Rezervasyons_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Rezervasyons_Eslesmes_EslesmeId` FOREIGN KEY (`EslesmeId`) REFERENCES `eslesmes` (`Id`),
  CONSTRAINT `FK_Rezervasyons_Kullanicis_KullaniciId` FOREIGN KEY (`KullaniciId`) REFERENCES `kullanicis` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Rezervasyons_Sahas_SahaId` FOREIGN KEY (`SahaId`) REFERENCES `sahas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Rezervasyons_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `saharesims`
--

DROP TABLE IF EXISTS `saharesims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `saharesims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SahaId` int NOT NULL,
  `Resim` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Baslik` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_SahaResims_SahaId` (`SahaId`),
  KEY `IX_SahaResims_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_SahaResims_Sahas_SahaId` FOREIGN KEY (`SahaId`) REFERENCES `sahas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_SahaResims_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sahas`
--

DROP TABLE IF EXISTS `sahas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sahas` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SehirId` int NOT NULL,
  `IlceId` int NOT NULL,
  `KullaniciId` int NOT NULL,
  `Baslik` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Aciklama` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `AcilisSaat` datetime(6) NOT NULL,
  `KapanisSaat` datetime(6) NOT NULL,
  `YirmiDortSaat` tinyint(1) NOT NULL,
  `Ozellik` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Ucret` double NOT NULL,
  `YoneticiId` int NOT NULL,
  `SahaTipi` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sahas_IlceId` (`IlceId`),
  KEY `IX_Sahas_KullaniciId` (`KullaniciId`),
  KEY `IX_Sahas_SehirId` (`SehirId`),
  KEY `IX_Sahas_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Sahas_IlceListes_IlceId` FOREIGN KEY (`IlceId`) REFERENCES `ilcelistes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Sahas_Kullanicis_KullaniciId` FOREIGN KEY (`KullaniciId`) REFERENCES `kullanicis` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Sahas_SehirListes_SehirId` FOREIGN KEY (`SehirId`) REFERENCES `sehirlistes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Sahas_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sehirlistes`
--

DROP TABLE IF EXISTS `sehirlistes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sehirlistes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SehirAdi` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sliders`
--

DROP TABLE IF EXISTS `sliders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sliders` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Resim1` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Resim2` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Yazi` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ButonYazi` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ButonLink` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sliders_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_Sliders_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stoktakips`
--

DROP TABLE IF EXISTS `stoktakips`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stoktakips` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CafeId` int NOT NULL,
  `Islem` int NOT NULL,
  `Adet` int NOT NULL,
  `Tarih` datetime(6) NOT NULL,
  `BirimFiyat` double NOT NULL,
  `Barkod` bigint NOT NULL,
  `YoneticiId` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_StokTakips_CafeId` (`CafeId`),
  KEY `IX_StokTakips_YoneticiId` (`YoneticiId`),
  CONSTRAINT `FK_StokTakips_Cafes_CafeId` FOREIGN KEY (`CafeId`) REFERENCES `cafes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_StokTakips_YonetimTablosus_YoneticiId` FOREIGN KEY (`YoneticiId`) REFERENCES `yonetimtablosus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `takims`
--

DROP TABLE IF EXISTS `takims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `takims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `KullaniciId` int NOT NULL,
  `SehirId` int NOT NULL,
  `IlceId` int NOT NULL,
  `Kadro` int NOT NULL,
  `Baslik` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Takims_IlceId` (`IlceId`),
  KEY `IX_Takims_KullaniciId` (`KullaniciId`),
  KEY `IX_Takims_SehirId` (`SehirId`),
  CONSTRAINT `FK_Takims_IlceListes_IlceId` FOREIGN KEY (`IlceId`) REFERENCES `ilcelistes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Takims_Kullanicis_KullaniciId` FOREIGN KEY (`KullaniciId`) REFERENCES `kullanicis` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Takims_SehirListes_SehirId` FOREIGN KEY (`SehirId`) REFERENCES `sehirlistes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `takimtakvims`
--

DROP TABLE IF EXISTS `takimtakvims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `takimtakvims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TakimId` int NOT NULL,
  `KullaniciId` int NOT NULL,
  `SahaId` int NOT NULL,
  `TarihBaslangic` datetime(6) NOT NULL,
  `TarihBitis` datetime(6) NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TakimTakvims_KullaniciId` (`KullaniciId`),
  KEY `IX_TakimTakvims_SahaId` (`SahaId`),
  KEY `IX_TakimTakvims_TakimId` (`TakimId`),
  CONSTRAINT `FK_TakimTakvims_Kullanicis_KullaniciId` FOREIGN KEY (`KullaniciId`) REFERENCES `kullanicis` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TakimTakvims_Sahas_SahaId` FOREIGN KEY (`SahaId`) REFERENCES `sahas` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TakimTakvims_Takims_TakimId` FOREIGN KEY (`TakimId`) REFERENCES `takims` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `yonetimtablosus`
--

DROP TABLE IF EXISTS `yonetimtablosus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `yonetimtablosus` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SahaSayisi` int NOT NULL,
  `YoneticiSayisi` int NOT NULL,
  `Act` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1001 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-06 13:10:35
