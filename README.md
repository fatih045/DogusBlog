# 📘 DogusBlog - Teknik Dokümantasyon

## 🧱 Proje Hakkında

**DogusBlog**, ASP.NET Core MVC mimarisi ile geliştirilmiş bir blog yönetim sistemidir.  
Kullanıcılar blog yazabilir, bloglara yorum yapabilir ve etiketler/kategoriler aracılığıyla içeriklere erişebilir.

---

## 🔧 Kullanılan Teknolojiler

- ASP.NET Core MVC  
- Entity Framework Core (Code-First)  
- SQL Server  
- Bootstrap (UI)  
- Cookie Tabanlı Authentication  
- Repository + Service Katmanı  
- Razor View Engine  
- Visual Studio IDE  

---

## 📁 Katmanlar

### 1. **Models**
Veritabanı tablolarına karşılık gelen Entity sınıfları:
- `User`
- `Blog`
- `Category`
- `Tag`
- `Comment`
- `BlogTag` (Blog ile Tag arasında many-to-many ilişki)

---

### 2. **Data**
- `ApplicationDbContext` : EF Core veritabanı context sınıfı  
- `SeedData.cs` : Başlangıç verilerini (kategori, kullanıcı, blog vs.) oluşturmak için

---

### 3. **Repositories**
- `IGenericRepository<T>` ve `GenericRepository<T>` : Temel CRUD işlemleri  
- `IBlogRepository`, `IUserRepository` gibi interface’ler ile entity'e özel işlemler  
- `BlogRepository` gibi sınıflarda Include ile ilişkili veriler (örneğin Kategori, Kullanıcı) çekilebilir

---

### 4. **Services**
- `IBlogService`, `IUserService` gibi interface’ler ve ilgili servis implementasyonları  
- Repository’ler aracılığıyla veriyi işler ve Controller katmanına iletir  

---

### 5. **Controllers**
- `BlogController` : Blog listeleme ve detay görüntüleme  
- `AuthController` : Login, Register, Logout işlemleri  
- `HomeController` : Ana sayfa ve yönlendirmeler  

---

### 6. **Views (Razor)**
- `/Views/Blog/Index.cshtml`, `Detail.cshtml`  
- `/Views/Auth/Login.cshtml`, `Register.cshtml`  
- `/Views/Shared/_Layout.cshtml` : Tüm sayfalarda ortak navbar, footer yapısı  

---

## 🔐 Authentication (Kimlik Doğrulama)

- Kullanıcılar `Register` işlemiyle kayıt olur  
- Şifreler SHA256 algoritması ile hash'lenerek veritabanına kaydedilir  
> ⚠️ *Güvenlik açısından SHA256 kullanımı önerilmez, yerine ASP.NET Identity tercih edilmelidir*

- `Login` işlemi sırasında kullanıcı doğrulanır ve `Claims` oluşturularak `HttpContext.SignInAsync()` ile cookie yazılır  
- `Logout` işlemi `HttpContext.SignOutAsync()` ile yapılır  
- `"MyCookieAuth"` adıyla bir `AuthenticationScheme` tanımlıdır  

---

## 💾 Seed Data (Başlangıç Verileri)

`SeedData.cs` içerisinde proje ilk çalıştırıldığında aşağıdaki veriler oluşturulur:

- 3 Kategori: **Yazılım**, **Kitap**, **Film**  
- 3 Etiket: **ASP.NET**, **Entity Framework**, **C#**  
- 1 Kullanıcı  
- 1 Blog  
- 1 Yorum  
- Blog ile Tag arasındaki ilişki: `BlogTags`

> ⚠️ *Seed işlemleri sırasında Id gibi identity alanlara elle değer verildiği için dikkatli olunmalıdır*

---

## 🧠 Geliştirici Notu

Bu proje, MVC yapısını, katmanlı mimariyi ve cookie tabanlı kimlik doğrulama sistemini örnekleyerek pratik yapma amacı taşımaktadır.  
Modern projelerde ASP.NET Identity ve daha gelişmiş güvenlik mekanizmaları tercih edilmelidir.

---

