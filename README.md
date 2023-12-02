# Aplikasi AMS (Assets/Licenses Management System)
Aplikasi AMS (Assets/Licenses Management System) adalah sebuah sistem manajemen aset dan lisensi yang dirancang sebagai backend menggunakan teknologi `.NET Core 7.0 API` tanpa antarmuka pengguna (UI). Aplikasi ini memiliki fokus pada manajemen aset serta pengelolaan lisensi dengan menggunakan `SQL Server` sebagai sistem manajemen basis data.

### Database design
berikut merupakan desain database pada aplikasi AMS:
![preview](https://github.com/robyiset/AMS_BE/blob/main/database_table_design.jpg)

## Fitur Utama
 - Manajemen Aset: Aplikasi ini memungkinkan pengguna untuk mengelola aset-aset perusahaan dengan lebih efisien, melacak informasi, status, dan detail aset secara terperinci.
 - Pengelolaan Lisensi: AMS memungkinkan pengguna untuk mencatat, mengatur, dan memantau lisensi yang dimiliki perusahaan, termasuk informasi tanggal kedaluwarsa dan penggunaan lisensi.
 - Manajemen User: Pada aplikasi AMS memiliki manajemen user untuk melakukan registrasi user agar dapat mengakses endpoints aplikasi AMS melalui `/auth/login`
 - Authentication & Authorization: AMS memiliki fitur keamanan menggunakan dengan pintu utama yang dapat diakses melalui `/auth/login` untuk mendapatkan `Bearer Token` serta memiliki autorisasi menggunakan token tersebut

## Teknologi Utama:
- .NET Core 7.0: Aplikasi dikembangkan menggunakan .NET Core 7.0, memastikan performa tinggi dan skalabilitas dalam pengelolaan aset dan lisensi.
- API: Didesain sebagai API, memungkinkan integrasi yang fleksibel dengan berbagai sistem dan antarmuka pengguna yang ada.
- SQL Server: Database menggunakan SQL Server sebagai sistem manajemen basis data untuk menyimpan dan mengelola informasi terkait aset dan lisensi.

## Cara Penggunaan:
 - Prasyarat: Pastikan sudah terpasang [.NET Core 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) dan `SQL Server` pada lingkungan pengembangan Anda.
 - Konfigurasi Database: Sesuaikan konfigurasi koneksi database pada `appsettings.json` dengan lingkungan pengembangan Anda.
 - Menjalankan Aplikasi: Jalankan aplikasi AMS pmenggunakan [VS Code](https://code.visualstudio.com/download), `Visual Studio 2022`, atau `terminal` dengan melakukan:
```bash
dotnet restore
dotnet build
dotnet run 
```
