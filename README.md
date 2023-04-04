# Sovos Invoicing Assesment

HTTP API üzerinden aldığı fatura bilgisini Hangfire aracılığıyla sisteme işleyip mail bilgisi gönderir.

## Proje nasıl çalıştırılır?

Projenin root klasöründe `docker compose up --build` komutu ile proje docker üzerinde ayağa kaldırılır.

Tarayıcı üzerinden http://localhost:8000/swagger/index.html üzerinden api operasyonları gerçekleştirilir.

http://localhost:8000/hangfire üzerinden background dashboard görüntülenebilir.

appsettings.json içerisinde yer alan `Notifications` ve `Mail` section'ları üzerinden mail ayarları yapılıp fatura aktarım işleminden sonra mail gönderimi gerçekleştirilir.