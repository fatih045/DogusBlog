


using DogusBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DogusBlog
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Categories.Any()) return;

            // Kategoriler
            var categories = new List<Category>
            {
                new() { Name = "Yazılım" },
                new() { Name = "Teknoloji" },
                new() { Name = "Sağlık" },
                new() { Name = "Eğitim" },
                new() { Name = "Seyahat" },
                new() { Name = "Kitap" },
                new() { Name = "Film" },
                new() { Name = "Girişimcilik" },
                new() { Name = "Kişisel Gelişim" },
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Tag'ler
            var tags = new List<Tag>
            {
                new() { Name = "C#" },   
                new() { Name = "ASP.NET Core" },   
                new() { Name = "Blazor" },
                new() { Name = "Entity Framework" },
                new() { Name = "Yapay Zeka" },
                new() { Name = "Sağlıklı Yaşam" },
                new() { Name = "Motivasyon" },
                new() { Name = "Okuma Alışkanlığı" },
                new() { Name = "Film Analizi" },
                new() { Name = "Girişimcilik" },
                new()  { Name = "Kişisel Gelişim" },
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();

            // Şifre hashleme
            var hasher = new PasswordHasher<User>();
            var users = new List<User>
            {
                new()
                {
                    Username = "Fatih Çınar",
                    Email = "fatih@example.com",
                    PasswordHash = hasher.HashPassword(new User(), "1234")
                },
                new()
                {
                    Username = "ayse demir",
                    Email = "ayse@example.com",
                    PasswordHash = hasher.HashPassword(new User(), "1234")
                },
                new()
                {
                    Username = "mehmet kara",
                    Email = "mehmet@example.com",
                    PasswordHash = hasher.HashPassword(new User(), "1234")
                },
                 new()
                {
                    Username = "testuser",
                    Email = "testuser@example.com",
                    PasswordHash = hasher.HashPassword(new User(), "1234")
                }

            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Blog Posts
            var blog1 = new Blog
            {
                Title = "Eğitimde Teknolojinin Rolü",
                Content = "Eğitimde teknolojinin kullanımı, öğretim süreçlerini dönüştürerek öğrenci merkezli bir öğrenme ortamı yaratmıştır. Akıllı tahtalar, online platformlar, interaktif uygulamalar ve sanal sınıflar gibi araçlar, öğrencilerin öğrenme sürecine aktif katılımını sağlar. Özellikle pandemi süreci, uzaktan eğitimin önemini gözler önüne sermiş ve birçok kurum bu alanda altyapılarını güçlendirmiştir. Öğretmenler ise artık sadece bilgi aktaran değil, aynı zamanda rehberlik eden bir rol üstlenmiştir. Bu gelişmeler sayesinde bireyselleştirilmiş öğrenme olanakları artmış, her öğrencinin kendi hızında öğrenmesi mümkün hale gelmiştir. Eğitim teknolojileri, öğretim materyallerinin çeşitliliğini artırarak hem öğretmenlerin işini kolaylaştırmakta hem de öğrencilerin ilgisini canlı tutmaktadır.",
                PublishDate = DateTime.Now.AddDays(-1),
                UserId = users[0].Id,
                CategoryId = categories.First(c => c.Name == "Eğitim").Id,
                ImagePath = "/uploads/blogs/blog1.png"
            };

            var blog2 = new Blog
            {
                Title = "Yapay Zeka ile Değişen Dünya",
                Content = "Yapay zeka, günümüz dünyasını hızla değiştiren en önemli teknolojilerden biridir. Sağlık, finans, ulaşım ve eğitim gibi pek çok sektörde yapay zekanın etkin kullanımı verimliliği artırmaktadır. Özellikle makine öğrenimi algoritmaları sayesinde veri analizi çok daha hızlı ve doğru hale gelmiştir. Örneğin, sağlık alanında hastalık teşhisinde kullanılan yapay zeka sistemleri doktorlara büyük kolaylık sağlamaktadır. Otomotiv sektöründe ise sürücüsüz araçlar bu teknolojinin ne kadar ileri gidebileceğinin bir göstergesidir. Yapay zekanın etik kullanımı ve veri gizliliği ise üzerinde düşünülmesi gereken önemli konular arasında yer almaktadır.",
                PublishDate = DateTime.Now.AddDays(-2),
                UserId = users[1].Id,
                CategoryId = categories.First(c => c.Name == "Teknoloji").Id,
                ImagePath = "/uploads/blogs/blog2.jpg"
            };

            var blog3 = new Blog
            {
                Title = "C# ile Nesne Tabanlı Programlamaya Giriş",
                Content = "C#, Microsoft tarafından geliştirilen modern, nesne tabanlı bir programlama dilidir. Özellikle .NET platformu üzerinde çalışan uygulamalar geliştirmek için tercih edilir. Nesne tabanlı programlama, yazılım geliştirmenin temel taşlarından biridir. Bu yaklaşım, kodun yeniden kullanılabilirliğini ve bakımını kolaylaştırır. C# dilinde sınıflar, nesneler, kalıtım, çok biçimlilik gibi kavramlar sıkça kullanılır. Programcılar bu yapı taşlarını öğrenerek daha sağlam ve modüler kodlar yazabilir. Visual Studio gibi güçlü geliştirme ortamları sayesinde C# ile uygulama geliştirmek oldukça verimlidir. C# günümüzde hem masaüstü hem de web uygulamaları geliştirme alanında yaygın olarak kullanılmaktadır.",
                PublishDate = DateTime.Now.AddDays(-3),
                UserId = users[2].Id,
                CategoryId = categories.First(c => c.Name == "Yazılım").Id,
                ImagePath = "/uploads/blogs/blog3.png"
            };

            var blog4 = new Blog
            {
                Title = "Sağlıklı Yaşam İçin Günlük Alışkanlıklar",
                Content = "Sağlıklı bir yaşam tarzı benimsemek, hem fiziksel hem de zihinsel sağlığımız üzerinde olumlu etkiler yaratır. Güne bir bardak su içerek başlamak, sabahları birkaç dakika egzersiz yapmak ve düzenli uyku alışkanlığı edinmek basit ama etkili adımlardır. Aynı zamanda dengeli beslenmek ve işlenmiş gıdalardan uzak durmak da oldukça önemlidir. Stres yönetimi için meditasyon ve nefes egzersizleri faydalı olabilir. Ayrıca düzenli olarak yapılan sağlık kontrolleri, olası hastalıkların erken teşhisinde büyük rol oynar. Teknolojik cihazlara bağımlılığı azaltmak ve doğayla vakit geçirmek de ruh sağlığı açısından oldukça faydalıdır. Bu alışkanlıklar bir bütün halinde uygulandığında yaşam kalitesi büyük ölçüde artar.",
                PublishDate = DateTime.Now.AddDays(-4),
                UserId = users[0].Id,
                CategoryId = categories.First(c => c.Name == "Sağlık").Id,
                ImagePath = "/uploads/blogs/blog4.png"
            };

            var blog5 = new Blog
            {
                Title = "Türkiye'de Gezilecek 10 Doğal Güzellik",
                Content = "Türkiye, doğal güzellikleriyle dünyada eşsiz bir konuma sahiptir. Kapadokya'nın peribacaları, Pamukkale'nin travertenleri, Fethiye'nin Ölüdeniz'i gibi yerler hem yerli hem yabancı turistlerin ilgisini çekmektedir. Ayrıca Karadeniz bölgesinde yer alan Ayder Yaylası ve Uzungöl doğaseverler için muhteşem alternatifler sunar. Likya Yolu gibi yürüyüş parkurları da macera tutkunları için oldukça caziptir. Nemrut Dağı’ndaki devasa heykeller, Kaçkar Dağları'nın zirvesi, Salda Gölü’nün eşsiz manzarası keşfedilmeye değerdir. Türkiye’nin dört bir yanındaki bu doğal zenginlikler, hem fotoğrafçılar hem de huzurlu bir kaçamak arayanlar için idealdir.",
                PublishDate = DateTime.Now.AddDays(-5),
                UserId = users[1].Id,
                CategoryId = categories.First(c => c.Name == "Seyahat").Id,
                ImagePath = "/uploads/blogs/blog5.jpg"
            };

            var blog6 = new Blog
            {
                Title = "2025 Yılında Okunması Gereken Kitaplar",
                Content = "Yeni yılda kitap okuma listesine eklenmesi gereken birçok eser bulunuyor. Özellikle kişisel gelişim, kurgu ve tarih kategorilerinde çıkan yeni kitaplar okuyucuların ilgisini çekiyor. Haruki Murakami'nin son romanı, Zülfü Livaneli'nin tarihi anlatıları, Yuval Noah Harari'nin analizleri bu yılın öne çıkanları arasında. Ayrıca yerli yazarlardan genç kalemlerin çıkardığı yeni eserler, edebiyat dünyasında yeni bir soluk yaratıyor. Dijital yayın platformlarında e-kitap seçeneklerinin artması da okuma alışkanlıklarını olumlu yönde etkiliyor. Kitap fuarları ve online etkinlikler, yazar-okur buluşmalarına olanak sağlıyor. Bu yıl, hem bilgi hem ilham kaynağı olabilecek pek çok kitap raflarda yerini aldı.",
                PublishDate = DateTime.Now.AddDays(-6),
                UserId = users[2].Id,
                CategoryId = categories.First(c => c.Name == "Kitap").Id,
                ImagePath = "/uploads/blogs/blog6.jpg"
            };

            var blog7 = new Blog
            {
                Title = "Girişimciliğin Altın Kuralları",
                Content = "Girişimcilik, sadece bir iş kurmak değil, aynı zamanda bir vizyonu gerçeğe dönüştürme sürecidir. Başarılı bir girişimci olmanın temelinde cesaret, azim ve yenilikçilik yatar. Pazar araştırması yapmak, hedef kitleyi iyi tanımak ve rakip analizi yapmak bu sürecin vazgeçilmez adımlarındandır. Ayrıca iyi bir iş planı hazırlamak ve finansal kaynakları etkin kullanmak da önemlidir. Başarısızlık durumunda pes etmemek ve ders çıkararak yola devam etmek girişimcilerin ortak özelliğidir. Girişimcilik ekosisteminde doğru network ağına sahip olmak, mentorluk almak ve kendini sürekli geliştirmek başarı şansını artırır. Günümüzde teknolojik girişimlerin yükselişi, yeni fırsatların kapısını aralamaktadır.",
                PublishDate = DateTime.Now.AddDays(-7),
                UserId = users[0].Id,
                CategoryId = categories.First(c => c.Name == "Girişimcilik").Id,
                ImagePath = "/uploads/blogs/blog7.jpg"
            };

            var blog8 = new Blog
            {
                Title = "Blazor ile Modern Web Uygulamaları",
                Content = "Blazor, C# ile tarayıcıda çalışan modern web uygulamaları geliştirmeyi mümkün kılan bir .NET teknolojisidir. JavaScript yerine C# ile yazılım geliştirme imkanı sunan Blazor, hem WebAssembly hem de Server taraflı modellerle çalışabilir. Blazor Server modeli daha hızlı geliştirme döngüsü sunarken, WebAssembly modeli daha bağımsız ve responsive uygulamalar sağlar. Komponent tabanlı mimarisiyle Angular ve React'e alternatif sunan Blazor, ASP.NET Core altyapısıyla entegre çalışır. Blazor, kodun yeniden kullanılabilirliğini artırırken, özellikle C# geliştiricileri için öğrenme sürecini kısaltır. Bu teknolojiyi kullanarak dashboard, form uygulamaları ve interaktif SPA'lar geliştirmek oldukça verimlidir.",
                PublishDate = DateTime.Now.AddDays(-8),
                UserId = users[1].Id,
                CategoryId = categories.First(c => c.Name == "Yazılım").Id,
                ImagePath = "/uploads/blogs/blog8.png"
            };

            var blog9 = new Blog
            {
                Title = "Sağlıklı Beslenme İçin 10 Altın Kural",
                Content = "Sağlıklı bir yaşamın temeli dengeli beslenmeden geçer. İşlenmiş gıdalardan uzak durmak, doğal ve taze ürünler tüketmek vücudun ihtiyaç duyduğu besinleri almasını sağlar. Günlük su tüketimini yeterli seviyede tutmak, öğün atlamadan düzenli beslenmek önemlidir. Sebze ve meyve tüketimi artırılmalı, rafine şeker ve tuz kullanımına dikkat edilmelidir. Öğünlerde protein, karbonhidrat ve yağ dengesine özen gösterilmeli, porsiyon kontrolü sağlanmalıdır. Ayrıca haftada en az üç gün fiziksel aktivite yapmak, sağlıklı beslenmenin etkisini artırır. Fast food alışkanlığını azaltmak, etiket okuma alışkanlığı kazanmak da bilinçli beslenmenin temel unsurlarındandır.",

                PublishDate = DateTime.Now.AddDays(-9),
                UserId = users[2].Id,
                CategoryId = categories.First(c => c.Name == "Sağlık").Id,
                ImagePath = "/uploads/blogs/blog9.jpg"
            };

            var blog10 = new Blog
            {
                Title = "Motivasyonun Gücü: Hedef Belirlemek",
                Content = "Motivasyon, bireyin harekete geçmesini sağlayan en önemli içsel güçlerden biridir. Hayatta anlamlı bir hedefe sahip olmak, kişinin kendine olan güvenini artırır ve odaklanmasını sağlar. Kısa ve uzun vadeli hedefler belirlemek, ilerlemenin ölçülmesini kolaylaştırır. Hedeflerin net, ölçülebilir ve ulaşılabilir olması başarı ihtimalini artırır. Ayrıca bu hedefleri yazmak ve sık sık gözden geçirmek, motivasyonun sürdürülebilirliğini sağlar. Çevrenin etkisi, olumlu alışkanlıklar ve düzenli öz değerlendirme motivasyonu artıran diğer faktörlerdir. Başarıya ulaşmak için sadece çalışmak değil, aynı zamanda doğru hedefe yönelmek gerekir.",
                PublishDate = DateTime.Now.AddDays(-10),
                UserId = users[1].Id,
                CategoryId = categories.First(c => c.Name == "Kişisel Gelişim").Id,
                ImagePath = "/uploads/blogs/blog10.jpg"
            };

            var blog11 = new Blog
            {
                Title = "Film Analizi: Inception ve Zaman Algısı",
                Content = "Christopher Nolan’ın başyapıtlarından Inception, zaman kavramı ve bilinçaltı üzerine kurgulanmış etkileyici bir bilim kurgu filmidir. Rüyaların içinde rüyalar konsepti, zamanın izafi yapısını çarpıcı bir şekilde gözler önüne serer. Filmde geçen her rüya katmanı, zamanın farklı hızlarda aktığı dünyalar sunar. Bu da izleyicide merak ve kafa karışıklığı uyandıran ama aynı zamanda düşünmeye sevk eden bir etki yaratır. Karakterlerin iç dünyaları, seçimleri ve bilinçaltındaki çatışmalar derinlikli biçimde işlenmiştir. Inception, sadece bir aksiyon filmi değil, aynı zamanda felsefi ve psikolojik yönleriyle de dikkat çeker. Zamanı farklı algılamamız, rüyaların gerçekliği sorgulaması filmi özel kılar.",
                PublishDate = DateTime.Now.AddDays(-11),
                UserId = users[0].Id,
                CategoryId = categories.First(c => c.Name == "Film").Id,
                ImagePath = "/uploads/blogs/blog11.jpg"
            };

            var blog12 = new Blog
            {
                Title = "Okuma Alışkanlığı Nasıl Kazanılır?",
                Content = "Okuma alışkanlığı, küçük yaşlardan itibaren kazandırılması gereken önemli bir beceridir. Bu alışkanlık sadece akademik başarıyı değil, aynı zamanda analitik düşünme, hayal gücü ve empati yeteneğini de geliştirir. Okuma köşeleri oluşturmak, günlük kısa süreli okuma saatleri belirlemek bu süreci kolaylaştırır. Ayrıca bireyin ilgi alanına yönelik kitaplar seçilmesi, okuma isteğini artırır. Aile içinde birlikte kitap okuma saatleri planlamak, çocuklara iyi bir örnek oluşturur. Dijital dikkat dağınıklığına karşı kitap okuma ortamı dikkatli seçilmelidir. Kütüphanelere gitmek, okuma yarışmalarına katılmak, kitap kulüpleri oluşturmak da okuma kültürünü destekler. Alışkanlık haline gelen okuma, bireyin hayat boyu gelişimine katkı sağlar.",
                PublishDate = DateTime.Now.AddDays(-12),
                UserId = users[2].Id,
                CategoryId = categories.First(c => c.Name == "Kitap").Id,
                ImagePath = "/uploads/blogs/blog12.jpg"
            };

            // Add all blogs to context
            context.Blogs.AddRange(blog1, blog2, blog3, blog4, blog5, blog6, blog7, blog8, blog9, blog10, blog11, blog12);
            context.SaveChanges();

            // BlogTag
            context.BlogTags.AddRange(
                // Blog 1 - Eğitimde Teknolojinin Rolü
                new BlogTag { BlogId = blog1.Id, TagId = tags.First(t => t.Name == "ASP.NET Core").Id },
                new BlogTag { BlogId = blog1.Id, TagId = tags.First(t => t.Name == "Blazor").Id },

                // Blog 2 - Yapay Zeka ile Değişen Dünya
                new BlogTag { BlogId = blog2.Id, TagId = tags.First(t => t.Name == "Yapay Zeka").Id },
                new BlogTag { BlogId = blog2.Id, TagId = tags.First(t => t.Name == "Girişimcilik").Id },

                // Blog 3 - C# ile Nesne Tabanlı Programlamaya Giriş
                new BlogTag { BlogId = blog3.Id, TagId = tags.First(t => t.Name == "C#").Id },
                new BlogTag { BlogId = blog3.Id, TagId = tags.First(t => t.Name == "Entity Framework").Id },

                // Blog 4 - Sağlıklı Yaşam İçin Günlük Alışkanlıklar
                new BlogTag { BlogId = blog4.Id, TagId = tags.First(t => t.Name == "Sağlıklı Yaşam").Id },
                new BlogTag { BlogId = blog4.Id, TagId = tags.First(t => t.Name == "Motivasyon").Id },

                // Blog 5 - Türkiye'de Gezilecek 10 Doğal Güzellik
                new BlogTag { BlogId = blog5.Id, TagId = tags.First(t => t.Name == "Girişimcilik").Id },
                new BlogTag { BlogId = blog5.Id, TagId = tags.First(t => t.Name == "Motivasyon").Id },

                // Blog 6 - 2025 Yılında Okunması Gereken Kitaplar
                new BlogTag { BlogId = blog6.Id, TagId = tags.First(t => t.Name == "Okuma Alışkanlığı").Id },
                new BlogTag { BlogId = blog6.Id, TagId = tags.First(t => t.Name == "Film Analizi").Id },

                // Blog 7 - Girişimciliğin Altın Kuralları
                new BlogTag { BlogId = blog7.Id, TagId = tags.First(t => t.Name == "Girişimcilik").Id },
                new BlogTag { BlogId = blog7.Id, TagId = tags.First(t => t.Name == "Motivasyon").Id },

                // Blog 8 - Blazor ile Modern Web Uygulamaları
                new BlogTag { BlogId = blog8.Id, TagId = tags.First(t => t.Name == "Blazor").Id },
                new BlogTag { BlogId = blog8.Id, TagId = tags.First(t => t.Name == "ASP.NET Core").Id },

                // Blog 9 - Sağlıklı Beslenme İçin 10 Altın Kural
                new BlogTag { BlogId = blog9.Id, TagId = tags.First(t => t.Name == "Sağlıklı Yaşam").Id },
                new BlogTag { BlogId = blog9.Id, TagId = tags.First(t => t.Name == "Motivasyon").Id },

                // Blog 10 - Motivasyonun Gücü
                new BlogTag { BlogId = blog10.Id, TagId = tags.First(t => t.Name == "Motivasyon").Id },
                new BlogTag { BlogId = blog10.Id, TagId = tags.First(t => t.Name == "Kişisel Gelişim").Id },

                // Blog 11 - Inception ve Zaman Algısı
                new BlogTag { BlogId = blog11.Id, TagId = tags.First(t => t.Name == "Film Analizi").Id },
                new BlogTag { BlogId = blog11.Id, TagId = tags.First(t => t.Name == "Okuma Alışkanlığı").Id },

                // Blog 12 - Okuma Alışkanlığı Nasıl Kazanılır?
                new BlogTag { BlogId = blog12.Id, TagId = tags.First(t => t.Name == "Okuma Alışkanlığı").Id },
                new BlogTag { BlogId = blog12.Id, TagId = tags.First(t => t.Name == "Motivasyon").Id }
            );
            context.SaveChanges();

            // Tüm blog yorumlarını tek bir AddRange içinde ekle
            context.Comments.AddRange(
                // Blog 1 - Eğitimde Teknolojinin Rolü
                new Comment { BlogId = blog1.Id, UserId = users[0].Id, Content = "Akıllı tahtaların eğitimdeki etkisini bizzat deneyimledim, harika bir yazı!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog1.Id, UserId = users[1].Id, Content = "Uzaktan eğitimle ilgili daha fazla örnek verilse daha da iyi olurdu.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog1.Id, UserId = users[2].Id, Content = "Eğitim yazılımlarının gelişimi hakkında güzel bir perspektif sunmuşsunuz.", CreatedAt = DateTime.Now },

                // Blog 2 - Yapay Zeka ile Değişen Dünya
                new Comment { BlogId = blog2.Id, UserId = users[1].Id, Content = "Junior geliştiriciler için çok faydalı öneriler var, teşekkürler!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog2.Id, UserId = users[2].Id, Content = "Mentorluk konusuna değinmeniz çok değerli.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog2.Id, UserId = users[0].Id, Content = "Teknolojik gelişmelere ayak uydurma fikri çok güzel aktarılmış.", CreatedAt = DateTime.Now },

                // Blog 3 - C# ile Nesne Tabanlı Programlamaya Giriş
                new Comment { BlogId = blog3.Id, UserId = users[2].Id, Content = "Code First yaklaşımı bu kadar net anlatmanız çok hoşuma gitti.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog3.Id, UserId = users[0].Id, Content = "Migration kavramını sonunda tam olarak anladım, teşekkürler!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog3.Id, UserId = users[1].Id, Content = "DbContext kullanımını örneklerle açıklamanız harikaydı.", CreatedAt = DateTime.Now },

                // Blog 4 - Sağlıklı Yaşam İçin Günlük Alışkanlıklar
                new Comment { BlogId = blog4.Id, UserId = users[0].Id, Content = "Listede sevdiğim kitaplar var, zevkinize sağlık!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog4.Id, UserId = users[1].Id, Content = "Kitap türlerine göre ayırmanız harika olmuş.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog4.Id, UserId = users[2].Id, Content = "Yeni okuma listem buradan çıktı diyebilirim.", CreatedAt = DateTime.Now },

                // Blog 5 - Türkiye'de Gezilecek 10 Doğal Güzellik
                new Comment { BlogId = blog5.Id, UserId = users[1].Id, Content = "Record structs özelliği en çok hoşuma giden oldu!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog5.Id, UserId = users[2].Id, Content = "Örnek kodlarla anlatım çok öğreticiydi.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog5.Id, UserId = users[0].Id, Content = "Yeni sürümde dikkat edilmesi gereken noktaları netleştirmişsiniz.", CreatedAt = DateTime.Now },

                // Blog 6 - 2025 Yılında Okunması Gereken Kitaplar
                new Comment { BlogId = blog6.Id, UserId = users[2].Id, Content = "Yapay zekanın sağlık alanındaki etkileri çok etkileyici.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog6.Id, UserId = users[0].Id, Content = "Etik konulara değinilmesi çok iyi olmuş.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog6.Id, UserId = users[1].Id, Content = "AI teknolojisinin hayatımızı nasıl şekillendirdiğine dair güzel bir yazı.", CreatedAt = DateTime.Now },

                // Blog 7 - Girişimciliğin Altın Kuralları
                new Comment { BlogId = blog7.Id, UserId = users[1].Id, Content = "Girişimcilik ruhu çok iyi açıklanmış, ilham verici!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog7.Id, UserId = users[2].Id, Content = "Pazar analizi konusunda daha fazla detay güzel olurdu.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog7.Id, UserId = users[0].Id, Content = "Yenilikçilik konusuna değinmeniz çok hoşuma gitti.", CreatedAt = DateTime.Now },

                // Blog 8 - Blazor ile Modern Web Uygulamaları
                new Comment { BlogId = blog8.Id, UserId = users[2].Id, Content = "Blazor'un farklarını kısa ve öz anlatmışsınız, elinize sağlık!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog8.Id, UserId = users[0].Id, Content = "WebAssembly kısmı özellikle dikkatimi çekti.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog8.Id, UserId = users[1].Id, Content = "Yeni başlayanlar için çok öğretici bir yazı olmuş.", CreatedAt = DateTime.Now },

                // Blog 9 - Sağlıklı Beslenme İçin 10 Altın Kural
                new Comment { BlogId = blog9.Id, UserId = users[0].Id, Content = "Beslenme konusunda çok pratik bilgiler var, teşekkürler!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog9.Id, UserId = users[1].Id, Content = "Etiket okuma konusu önemli bir farkındalık.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog9.Id, UserId = users[2].Id, Content = "Alışkanlıkları değiştirmek zor ama bu yazı motive edici.", CreatedAt = DateTime.Now },

                // Blog 10 - Motivasyonun Gücü
                new Comment { BlogId = blog10.Id, UserId = users[2].Id, Content = "Hedef koyma konusunda eksiklerimi fark ettim, teşekkürler!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog10.Id, UserId = users[0].Id, Content = "Yazıyı okuduktan sonra hemen hedef listemi güncelledim!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog10.Id, UserId = users[1].Id, Content = "Kısa vadeli hedeflerin gücünü unutmamak lazım.", CreatedAt = DateTime.Now },

                // Blog 11 - Inception ve Zaman Algısı
                new Comment { BlogId = blog11.Id, UserId = users[1].Id, Content = "Inception gibi filmleri analiz eden içerikleri çok seviyorum!", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog11.Id, UserId = users[2].Id, Content = "Zaman kavramını çok iyi işlemişsiniz, elinize sağlık.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog11.Id, UserId = users[0].Id, Content = "Film analizlerine devam etmeniz harika olur!", CreatedAt = DateTime.Now },

                // Blog 12 - Okuma Alışkanlığı Nasıl Kazanılır?
                new Comment { BlogId = blog12.Id, UserId = users[0].Id, Content = "Kitap kulüpleri fikri gerçekten ilham verici.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog12.Id, UserId = users[2].Id, Content = "Çocuklara yönelik öneriler çok faydalı.", CreatedAt = DateTime.Now },
                new Comment { BlogId = blog12.Id, UserId = users[1].Id, Content = "Dijital dikkat dağınıklığına karşı güzel taktikler var.", CreatedAt = DateTime.Now }
            );

            context.SaveChanges();
        }
    }
}