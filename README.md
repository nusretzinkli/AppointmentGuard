# 🏥 AppointmentGuard - N-Layer Architecture Appointment System

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Status](https://img.shields.io/badge/Status-Completed-success?style=for-the-badge)

**[ [English](#-english) | [Türkçe](#-türkçe) ]**

---

<a name="-english"></a>
## 🌐 English

AppointmentGuard is a secure and scalable appointment management system developed using enterprise-standard **N-Layer Architecture**.

> **Vision:** This project is not just a working application; it's a backend engineering study that implements **Concurrency**, **Unit Testing**, **Containerization**, and **Clean Architecture** principles.

---

## 📸 Project Preview (Demo)

### 1. Appointment Flow & SweetAlert2 Integration
User-friendly interface with dynamic time management and instant notifications.

| Feature | Preview |
| :---: | :---: |
| **System Login** | ![System Login](./assets/login.gif) |
| **Registration** | ![System Registration](./assets/register.jpg) |
| **Create Appointment** | ![Create Appointment](./assets/create_appointment.gif) |
| **Cancel Appointment** | ![Cancel Appointment](./assets/cancel_appointment.gif) |
| **Race Condition** | ![Race Condition](./assets/race_condition.gif) |

---

## 🏗 Architecture Structure (N-Layer Architecture)

The project is divided into layers according to the Separation of Concerns (SoC) principle.

![N-Tier Architecture](./assets/n-tier.jpg)

* **🧱 Core Layer:** Entities, DTOs, and Interfaces. No external dependencies.
* **💾 Data Layer:** Database operations, Migrations, and Repository Pattern implementation.
* **⚙️ Service Layer:** Business Logic, Validations (FluentValidation), and Mapping (AutoMapper).
* **🌐 API Layer:** RESTful services.
* **💻 Web Layer (MVC):** User interface.

---

## 🚀 Technical Features and Solutions

### 🔒 1. Security & Authentication
System security is configured according to industry standards.
* **Authentication:** Secure session management with ASP.NET Core **Cookie Authentication** mechanism.
* **Password Hashing:** User passwords are not stored as plain-text in the database; they are hashed and protected using the **BCrypt** algorithm.
* **Authorization:** Role-Based authorization infrastructure is available.

### 🔒 2. Race Condition Solution
When two users try to book an appointment with the same doctor simultaneously (within milliseconds), the system maintains data integrity.
* **Solution:** In addition to the validation layer, data consistency is guaranteed through **Double-Check Locking** and **Try-Catch** mechanism at the database commit time (`CommitAsync`).

### ⚡ 3. Performance and Scalability
* **Async/Await:** All I/O operations (Database access) are made asynchronous to prevent thread blocking, providing high concurrent request capacity.
* **Optimized LINQ:** Database queries are optimized over `IQueryable` to ensure only necessary data is fetched from the server.

### 🧪 4. Unit Testing
Critical business logic of the project has been tested using **xUnit** and **Moq**.
* ✅ Collision scenario verified with `RaceCondition_ShouldThrowUserFriendlyException` test.

---

## 🛠 Installation and Running

You can choose one of two methods to run the project.

### Option A: Quick Setup with Docker (Recommended) 🐳
You can launch the project with a single command without dealing with SQL Server installation.

1. Clone the project and navigate to the main directory.
2. Run this command in the terminal:
   ```bash
   docker-compose up --build
   ```
3. The application will start at **http://localhost:5000**.

### Option B: Manual Setup (Visual Studio) 🛠
1. Clone the project.
2. Edit the Connection String in the `appsettings.json` file according to your SQL Server information.
3. Create the database via **Package Manager Console**:
   ```powershell
   Update-Database -Project AppointmentGuard.Data -StartupProject AppointmentGuard.API
   ```
4. Start the project. **Seed Data** will be loaded automatically.

---

## 👤 Test Users (Admin/User)
The following users are pre-defined for quick testing:

| User | Email | Password |
| :--- | :--- | :--- |
| **User 1 (Merve)** | `merve@test.com` | `123456` |
| **User 2 (Cemal)** | `cemal@test.com` | `123456` |

---

### 📞 Contact & Links

<a href="https://www.linkedin.com/in/nusretzinkli">
  <img src="https://img.shields.io/badge/LinkedIn-Connect-blue?style=for-the-badge&logo=linkedin">
</a>

---

### 🤝 Contributing

If you'd like to contribute to the project:
1. Fork the repository
2. Create a new branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: Add amazing feature'`)
4. Push your branch (`git push origin feature/amazing-feature`)
5. Create a Pull Request

---

**⭐ Don't forget to star the project if you like it!**

---
---

<a name="-türkçe"></a>
## 🇹🇷 Türkçe

AppointmentGuard, kurumsal standartlara uygun **N-Katmanlı Mimari (N-Layer Architecture)** kullanılarak geliştirilmiş, güvenli ve ölçeklenebilir bir randevu yönetim sistemidir.

> **Vizyon:** Bu proje, sadece çalışan bir uygulama değil; **Concurrency (Eşzamanlılık)**, **Unit Testing**, **Containerization** ve **Clean Architecture** prensiplerini uygulayan bir Backend mühendislik çalışmasıdır.

---

## 📸 Proje Önizlemesi (Demo)

### 1. Randevu Alma Akışı & SweetAlert2 Entegrasyonu
Kullanıcı dostu arayüz ile dinamik saat yönetimi ve anlık bildirimler.

| Özellik | Önizleme |
| :---: | :---: |
| **Sisteme Giriş** | ![Sisteme Giriş](./assets/login.gif) |
| **Kayıt Olma** | ![Sisteme Kayıt Olma](./assets/register.jpg) |
| **Randevu Alma** | ![Randevu Alma](./assets/create_appointment.gif) |
| **Randevu İptali** | ![Randevu İptali](./assets/cancel_appointment.gif) |
| **Race Condition** | ![Race Condition](./assets/race_condition.gif) |

---

## 🏗 Mimari Yapı (N-Layer Architecture)

Proje, Sorumlulukların Ayrılığı (SoC) prensibine uygun olarak katmanlara ayrılmıştır.

![N-Tier Architecture](./assets/n-tier.jpg)

* **🧱 Core Layer:** Entity'ler, DTO'lar ve Arayüzler (Interfaces). Dışa bağımlılığı yoktur.
* **💾 Data Layer:** Veritabanı işlemleri, Migrations ve Repository Pattern implementasyonu.
* **⚙️ Service Layer:** İş mantığı (Business Logic), Validasyonlar (FluentValidation) ve Mapping (AutoMapper).
* **🌐 API Layer:** RESTful servisler.
* **💻 Web Layer (MVC):** Kullanıcı arayüzü.

---

## 🚀 Teknik Özellikler ve Çözümler

### 🔒 1. Güvenlik (Security & Auth)
Sistem güvenliği endüstri standartlarına uygun olarak yapılandırılmıştır.
* **Authentication:** ASP.NET Core **Cookie Authentication** mekanizması ile güvenli oturum yönetimi.
* **Password Hashing:** Kullanıcı şifreleri veritabanında düz metin (plain-text) olarak saklanmaz; **BCrypt** algoritması ile hash'lenerek korunur.
* **Authorization:** Role-Based (Rol Bazlı) yetkilendirme altyapısı mevcuttur.

### 🔒 2. Race Condition (Yarış Durumu) Çözümü
Aynı anda (milisaniyeler içinde) aynı doktora randevu almaya çalışan iki kullanıcı olduğunda, sistem veri bütünlüğünü korur.
* **Çözüm:** Validasyon katmanına ek olarak, veritabanı kayıt anında (`CommitAsync`) **Double-Check Locking** ve **Try-Catch** mekanizması ile veri tutarlılığı garanti edilir.

### ⚡ 3. Performans ve Ölçeklenebilirlik
* **Async/Await:** Tüm I/O operasyonları (Veritabanı erişimi) asenkron yapılarak thread bloklanması önlenmiş, yüksek eşzamanlı istek kapasitesi sağlanmıştır.
* **Optimized LINQ:** Veritabanı sorguları `IQueryable` üzerinden optimize edilerek sunucuya sadece gerekli verilerin çekilmesi sağlanmıştır.

### 🧪 4. Unit Testing (Birim Testleri)
Projenin kritik iş mantıkları **xUnit** ve **Moq** kullanılarak test edilmiştir.
* ✅ `RaceCondition_ShouldThrowUserFriendlyException` testi ile çakışma senaryosu doğrulanmıştır.

---

## 🛠 Kurulum ve Çalıştırma

Projeyi çalıştırmak için iki yöntemden birini seçebilirsiniz.

### Seçenek A: Docker ile Hızlı Kurulum (Önerilen) 🐳
SQL Server kurulumuyla uğraşmadan tek komutla projeyi ayağa kaldırabilirsiniz.

1. Projeyi klonlayın ve ana dizine gidin.
2. Terminalde şu komutu çalıştırın:
   ```bash
   docker-compose up --build
   ```
3. Uygulama **http://localhost:5000** adresinde yayına başlayacaktır.

### Seçenek B: Manuel Kurulum (Visual Studio) 🛠
1. Projeyi klonlayın.
2. `appsettings.json` dosyasındaki Connection String'i kendi SQL Server bilginize göre düzenleyin.
3. **Package Manager Console** üzerinden veritabanını oluşturun:
   ```powershell
   Update-Database -Project AppointmentGuard.Data -StartupProject AppointmentGuard.API
   ```
4. Projeyi başlatın. **Seed Data** otomatik yüklenecektir.

---

## 👤 Test Kullanıcısı (Admin/User)
Hızlı test için aşağıdaki kullanıcılar tanımlı gelmektedir:

| Kullanıcı | Email | Şifre |
| :--- | :--- | :--- |
| **User 1 (Merve)** | `merve@test.com` | `123456` |
| **User 2 (Cemal)** | `cemal@test.com` | `123456` |

---

### 📞 İletişim & Bağlantılar

<a href="https://www.linkedin.com/in/nusretzinkli">
  <img src="https://img.shields.io/badge/LinkedIn-Connect-blue?style=for-the-badge&logo=linkedin">
</a>

---

### 🤝 Katkıda Bulunma

Projeye katkıda bulunmak isterseniz:
1. Repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'feat: Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

---

**⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!**