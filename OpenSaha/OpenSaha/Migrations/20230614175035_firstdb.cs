using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSaha.Migrations
{
    /// <inheritdoc />
    public partial class firstdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SehirListes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SehirAdi = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SehirListes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "YonetimTablosus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SahaSayisi = table.Column<int>(type: "int", nullable: false),
                    YoneticiSayisi = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YonetimTablosus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IlceListes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IlceAdi = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SehirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlceListes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlceListes_SehirListes_SehirId",
                        column: x => x.SehirId,
                        principalTable: "SehirListes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Eslesmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EvSahibi = table.Column<int>(type: "int", nullable: false),
                    Deplasman = table.Column<int>(type: "int", nullable: true),
                    Sahalar = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MacTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AktifSure = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OnayDurum = table.Column<int>(type: "int", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eslesmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eslesmes_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kullanicis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Isim = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Soyisim = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefon = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DogumTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Puan = table.Column<double>(type: "double", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: true),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicis_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Resim1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resim2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Yazi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ButonYazi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ButonLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sliders_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KullaniciPuanIslemleris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    IslemTipi = table.Column<int>(type: "int", nullable: false),
                    Ucret = table.Column<double>(type: "double", nullable: false),
                    Puan = table.Column<double>(type: "double", nullable: true),
                    OdemeTipi = table.Column<int>(type: "int", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: true),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciPuanIslemleris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KullaniciPuanIslemleris_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciPuanIslemleris_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sahas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SehirId = table.Column<int>(type: "int", nullable: false),
                    IlceId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aciklama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AcilisSaat = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KapanisSaat = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    YirmiDortSaat = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ozellik = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ucret = table.Column<double>(type: "double", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    SahaTipi = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sahas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sahas_IlceListes_IlceId",
                        column: x => x.IlceId,
                        principalTable: "IlceListes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sahas_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sahas_SehirListes_SehirId",
                        column: x => x.SehirId,
                        principalTable: "SehirListes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sahas_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Takims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    SehirId = table.Column<int>(type: "int", nullable: false),
                    IlceId = table.Column<int>(type: "int", nullable: false),
                    Kadro = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Takims_IlceListes_IlceId",
                        column: x => x.IlceId,
                        principalTable: "IlceListes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Takims_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Takims_SehirListes_SehirId",
                        column: x => x.SehirId,
                        principalTable: "SehirListes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cafes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Baslik = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fiyat = table.Column<double>(type: "double", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Stoklu = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SahaId = table.Column<int>(type: "int", nullable: false),
                    Barkod = table.Column<long>(type: "bigint", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cafes_Sahas_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cafes_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ekipmanlars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SahaId = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resim = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aciklama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ucret = table.Column<double>(type: "double", nullable: false),
                    Adet = table.Column<double>(type: "double", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipmanlars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ekipmanlars_Sahas_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ekipmanlars_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rezervasyons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SahaId = table.Column<int>(type: "int", nullable: false),
                    RandevuBaslangic = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RandevuBitis = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    EslesmeId = table.Column<int>(type: "int", nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervasyons_Eslesmes_EslesmeId",
                        column: x => x.EslesmeId,
                        principalTable: "Eslesmes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rezervasyons_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervasyons_Sahas_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervasyons_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SahaResims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SahaId = table.Column<int>(type: "int", nullable: false),
                    Resim = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Baslik = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahaResims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SahaResims_Sahas_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SahaResims_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TakimTakvims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TakimId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    SahaId = table.Column<int>(type: "int", nullable: false),
                    TarihBaslangic = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TarihBitis = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakimTakvims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakimTakvims_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakimTakvims_Sahas_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakimTakvims_Takims_TakimId",
                        column: x => x.TakimId,
                        principalTable: "Takims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StokTakips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CafeId = table.Column<int>(type: "int", nullable: false),
                    Islem = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BirimFiyat = table.Column<double>(type: "double", nullable: false),
                    Barkod = table.Column<long>(type: "bigint", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokTakips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokTakips_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokTakips_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CafeTakips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RezervasyonId = table.Column<int>(type: "int", nullable: false),
                    CafeId = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Ucret = table.Column<double>(type: "double", nullable: false),
                    Barkod = table.Column<long>(type: "bigint", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeTakips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CafeTakips_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CafeTakips_Rezervasyons_RezervasyonId",
                        column: x => x.RezervasyonId,
                        principalTable: "Rezervasyons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CafeTakips_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EkipmanRezervasyons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RezervasyonId = table.Column<int>(type: "int", nullable: false),
                    EkipmanId = table.Column<int>(type: "int", nullable: false),
                    Ucret = table.Column<double>(type: "double", nullable: false),
                    Adet = table.Column<double>(type: "double", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkipmanRezervasyons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EkipmanRezervasyons_Ekipmanlars_EkipmanId",
                        column: x => x.EkipmanId,
                        principalTable: "Ekipmanlars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkipmanRezervasyons_Rezervasyons_RezervasyonId",
                        column: x => x.RezervasyonId,
                        principalTable: "Rezervasyons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkipmanRezervasyons_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Odemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SahaUcret = table.Column<double>(type: "double", nullable: true),
                    KafeUcret = table.Column<double>(type: "double", nullable: true),
                    EkipmanUcret = table.Column<double>(type: "double", nullable: true),
                    RezervasyonId = table.Column<int>(type: "int", nullable: false),
                    OdemeTipleri = table.Column<int>(type: "int", nullable: false),
                    SahaId = table.Column<int>(type: "int", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: true),
                    Act = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odemes_Rezervasyons_RezervasyonId",
                        column: x => x.RezervasyonId,
                        principalTable: "Rezervasyons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odemes_Sahas_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odemes_YonetimTablosus_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "YonetimTablosus",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cafes_SahaId",
                table: "Cafes",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cafes_YoneticiId",
                table: "Cafes",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_CafeTakips_CafeId",
                table: "CafeTakips",
                column: "CafeId");

            migrationBuilder.CreateIndex(
                name: "IX_CafeTakips_RezervasyonId",
                table: "CafeTakips",
                column: "RezervasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_CafeTakips_YoneticiId",
                table: "CafeTakips",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipmanlars_SahaId",
                table: "Ekipmanlars",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipmanlars_YoneticiId",
                table: "Ekipmanlars",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_EkipmanRezervasyons_EkipmanId",
                table: "EkipmanRezervasyons",
                column: "EkipmanId");

            migrationBuilder.CreateIndex(
                name: "IX_EkipmanRezervasyons_RezervasyonId",
                table: "EkipmanRezervasyons",
                column: "RezervasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_EkipmanRezervasyons_YoneticiId",
                table: "EkipmanRezervasyons",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Eslesmes_YoneticiId",
                table: "Eslesmes",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_IlceListes_SehirId",
                table: "IlceListes",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciPuanIslemleris_KullaniciId",
                table: "KullaniciPuanIslemleris",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciPuanIslemleris_YoneticiId",
                table: "KullaniciPuanIslemleris",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicis_YoneticiId",
                table: "Kullanicis",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Odemes_RezervasyonId",
                table: "Odemes",
                column: "RezervasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Odemes_SahaId",
                table: "Odemes",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_Odemes_YoneticiId",
                table: "Odemes",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyons_EslesmeId",
                table: "Rezervasyons",
                column: "EslesmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyons_KullaniciId",
                table: "Rezervasyons",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyons_SahaId",
                table: "Rezervasyons",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyons_YoneticiId",
                table: "Rezervasyons",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_SahaResims_SahaId",
                table: "SahaResims",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_SahaResims_YoneticiId",
                table: "SahaResims",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sahas_IlceId",
                table: "Sahas",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sahas_KullaniciId",
                table: "Sahas",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Sahas_SehirId",
                table: "Sahas",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Sahas_YoneticiId",
                table: "Sahas",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sliders_YoneticiId",
                table: "Sliders",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_StokTakips_CafeId",
                table: "StokTakips",
                column: "CafeId");

            migrationBuilder.CreateIndex(
                name: "IX_StokTakips_YoneticiId",
                table: "StokTakips",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Takims_IlceId",
                table: "Takims",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Takims_KullaniciId",
                table: "Takims",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Takims_SehirId",
                table: "Takims",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_TakimTakvims_KullaniciId",
                table: "TakimTakvims",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_TakimTakvims_SahaId",
                table: "TakimTakvims",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_TakimTakvims_TakimId",
                table: "TakimTakvims",
                column: "TakimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CafeTakips");

            migrationBuilder.DropTable(
                name: "EkipmanRezervasyons");

            migrationBuilder.DropTable(
                name: "KullaniciPuanIslemleris");

            migrationBuilder.DropTable(
                name: "Odemes");

            migrationBuilder.DropTable(
                name: "SahaResims");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "StokTakips");

            migrationBuilder.DropTable(
                name: "TakimTakvims");

            migrationBuilder.DropTable(
                name: "Ekipmanlars");

            migrationBuilder.DropTable(
                name: "Rezervasyons");

            migrationBuilder.DropTable(
                name: "Cafes");

            migrationBuilder.DropTable(
                name: "Takims");

            migrationBuilder.DropTable(
                name: "Eslesmes");

            migrationBuilder.DropTable(
                name: "Sahas");

            migrationBuilder.DropTable(
                name: "IlceListes");

            migrationBuilder.DropTable(
                name: "Kullanicis");

            migrationBuilder.DropTable(
                name: "SehirListes");

            migrationBuilder.DropTable(
                name: "YonetimTablosus");
        }
    }
}
