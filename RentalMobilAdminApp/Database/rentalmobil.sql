
CREATE DATABASE IF NOT EXISTS rentalmobil;
USE rentalmobil;

CREATE TABLE admin (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50),
    password VARCHAR(50)
);

CREATE TABLE mobil (
    id INT AUTO_INCREMENT PRIMARY KEY,
    merk VARCHAR(100),
    tipe VARCHAR(100),
    tahun INT,
    harga_sewa DECIMAL(10,2),
    status ENUM('Tersedia','Disewa','Rusak')
);

CREATE TABLE pelanggan (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nama VARCHAR(100),
    alamat TEXT,
    no_ktp VARCHAR(20),
    no_sim VARCHAR(20),
    telp VARCHAR(20)
);

CREATE TABLE transaksi (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_mobil INT,
    id_pelanggan INT,
    tanggal_sewa DATE,
    tanggal_kembali DATE,
    total DECIMAL(10,2),
    denda DECIMAL(10,2),
    FOREIGN KEY (id_mobil) REFERENCES mobil(id),
    FOREIGN KEY (id_pelanggan) REFERENCES pelanggan(id)
);
