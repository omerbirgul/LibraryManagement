# Library Management System

Bu proje, kütüphane yönetim sistemi olarak tasarlanmış ve geliştirilmiştir. Proje, modern yazılım geliştirme teknikleri kullanılarak hem backend hem de frontend olarak iki bölümden oluşmaktadır.

## Genel Tanıtım ve Özellikler

- Kitap ekleme, güncelleme ve silme
- Kullanıcı yönetimi
- Kitap ödünç alma ve iade işlemleri

## Backend

Projenin backend tarafı .NET çok katmanlı mimari ile geliştirilmiştir. Backend katmanı dört ana katmandan oluşmaktadır: Core, Data, Service ve API.

### Core Katmanı

Core katmanı, servis interface'leri, entity'ler, DTO'lar, generic repository interface'i ve unit of work interface'ini içermektedir. Bu katman, projenin en temel yapı taşlarını barındırır ve diğer katmanlar tarafından kullanılmak üzere genel işlevsellik sağlar.

- **Servis Interface'leri:** İş mantığına dair servislerin tanımlandığı interface'lerdir.
- **Entity'ler:** Veritabanı tablolarına karşılık gelen sınıflardır.
- **DTO'lar:** Veri transfer nesneleri, veri iletimi için kullanılır.
- **Generic Repository Interface'i:** Temel CRUD operasyonlarının tanımlandığı genel repository interface'idir.
- **Unit of Work Interface'i:** Birden fazla repository'nin tek bir işlemde birleştirilmesini sağlar.

### Data Katmanı

Data katmanı, veritabanı implementasyonlarını, repository ve unit of work implementasyonlarını içerir. Bu katman veritabanı ile iletişimi sağlar ve veritabanı işlemlerinin gerçekleştirilmesinden sorumludur.

- **Database Implementasyonları:** Veritabanı bağlantısı ve ayarlarının yapıldığı sınıflar.
- **Repository Implementasyonları:** Generic repository interface'inin implementasyonları.
- **Unit of Work Implementasyonu:** Unit of work pattern'inin implementasyonu.

### Service Katmanı

Service katmanı, iş mantığı ve token oluşturma sınıflarını içerir. Bu katman, iş kurallarının uygulandığı ve verilerin işlenip kontrol edildiği katmandır.

- **Business Logic:** İş kurallarının ve mantığının uygulandığı sınıflar.
- **Token Oluşturma:** Kullanıcı kimlik doğrulaması için token oluşturma işlemleri.

### API Katmanı

API katmanı, endpoint'lerin tanımlandığı ve dış dünyaya açıldığı katmandır. Bu katman, istemcilerin (client) backend ile iletişim kurmasını sağlar.

- **Endpoint'ler:** İstemcilerin erişebileceği API endpoint'leri.

### Teknik Detaylar

- **Dependency Injection (DI):** Projede bağımlılıkların yönetimi için dependency injection kullanılmıştır. Bu sayede kodun test edilmesi ve yönetilmesi kolaylaşmıştır.
- **Unit of Work Pattern:** Birden fazla repository'nin tek bir işlemde yönetilmesini sağlar. Bu pattern, veri tutarlılığını ve bütünlüğünü korumak için kullanılmıştır.

Backend tarafında kullanılan bu mimari ve teknikler, projenin ölçeklenebilir, yönetilebilir ve sürdürülebilir olmasını sağlamaktadır.


## Frontend

Projenin frontend tarafı, modern ve kullanıcı dostu bir arayüz sağlamak için .NET MVC kullanılarak tasarlanmıştır. Bu bölüm, sadece backend API'lerine isteklerde bulunur ve herhangi bir business logic içermez. 

### Özellikler

- **Kullanıcı Dostu Arayüz:** Kolay navigasyon ve etkileşimli tasarım.
- **API Entegrasyonu:** Backend endpoint'lerine yapılan istekler ile hızlı ve güvenilir veri akışı.
- **JWT Tabanlı Güvenlik:** Kullanıcı yetkilendirme ve kimlik doğrulama işlemleri, JWT token kullanılarak sağlanır.

### Teknik Detaylar

- **MVC Yapısı:** Model-View-Controller yapısı ile kodun modüler, okunabilir ve bakımı kolay hale getirilmesi.
- **Bootstrap:** Modern ve şık kullanıcı arayüzü bileşenleri için Bootstrap kullanımı.

Frontend tarafında kullanılan bu teknolojiler ve yaklaşımlar, kullanıcı deneyimini en üst düzeye çıkarmak ve uygulamanın performansını artırmak için özenle seçilmiştir.