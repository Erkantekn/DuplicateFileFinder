# 📌 Duplicate File Finder

# EN #

Duplicate File Finder is a Windows desktop application that detects duplicate files on your computer and allows you to delete them safely.

This project originated from a personal need:

When I switched my Android phone, I took a backup and loaded it onto the new phone.  
Later, when I switched phones again, I loaded the old backup once more.  
As a result, thousands of duplicate copies of the same photos and videos were created.  
Since manual cleaning became impossible, I developed this application.

## 🎯 Purpose
To detect and clean duplicate files:
- Quickly
- Safely
- Without risking the user

## 🧩 Features
- ✅ Folder and subfolder scanning
- ✅ High performance with large files
- ✅ Hash-based comparison
- ✅ Grouping duplicate files
- ✅ Keeping the oldest file and deleting others
- ✅ Live progress status during scanning
- ✅ Detailed summary screen
- ✅ Pre-deletion confirmation system
- ✅ MVVM-like layered architecture

## 🖼️ Screenshots

### 1️⃣ Folder Selection
The user selects the main folder to be scanned.

<img width="1028" height="489" alt="Ekran görüntüsü 2026-01-21 224932" src="https://github.com/user-attachments/assets/ee9dcb01-c0ee-4f9a-99ff-b17c5135d214" />

### 2️⃣ Scanning Process
Files are read, grouped by size, and hashes are calculated.

<img width="1028" height="489" alt="Ekran görüntüsü 2026-01-21 225918" src="https://github.com/user-attachments/assets/ac328ecd-4c3c-4c65-bfa2-e265bf9c0dec" />

### 3️⃣ Summary Screen
Total scanned files, duplicate file count, and total size are displayed.

<img width="286" height="156" alt="Ekran görüntüsü 2026-01-21 230241" src="https://github.com/user-attachments/assets/c7607f2d-d2e2-4a15-8e1f-72f37e69fdd2" />

### 4️⃣ Detail Screen
Duplicate files are listed in groups.

<img width="708" height="436" alt="Ekran görüntüsü 2026-01-21 230308" src="https://github.com/user-attachments/assets/3e62d644-d452-47aa-954c-d46bbf16b9f8" />

### 5️⃣ Deletion Confirmation
The oldest file is preserved, other copies are deleted.

<img width="350" height="281" alt="Ekran görüntüsü 2026-01-21 230322" src="https://github.com/user-attachments/assets/0d8719a2-d083-4ad8-ab39-c7501024eaee" />

## 🧠 How the Application Works

### 🔍 Scanning Algorithm
1. Selected folders are scanned recursively
2. Files are first grouped by size
3. For files with the same size:
   - Hash is calculated (MD5 / XXHash)
   - Files with identical hashes are considered duplicates
4. The oldest file is preserved
5. Others are deleted

## ⚙️ Technologies Used

| Technology | Description |
|------------|-------------|
| .NET 8 | Main platform |
| Windows Forms | UI |
| C# | Backend |
| Task / async | Asynchronous operations |
| TreeView | Folder & file display |
| Dependency Injection | Layered architecture |
| Hash Algorithms | File comparison |

## 🧱 Project Architecture

**DuplicateFileFinder**
- **Application**
  - Services
  - Interfaces
  - DTOs
- **Domain**
  - Entities
- **Infrastructure**
  - FileSystem
  - Hashing
- **UI**
  - Forms
  - Views
  - Presenters

**Architecture Features:**
- ✔ Layers are independent from each other
- ✔ UI → Application → Domain directional dependency
- ✔ Testable structure

## 🚀 Performance Optimizations
- Parallel file scanning
- Hash is only applied to necessary files
- Duplicate check is first done by file size
- Unnecessary hash calculations are prevented
- UI thread does not lock up

## 🔐 Security Measures
- ✔ System files are skipped
- ✔ Locked files are automatically passed over
- ✔ No deletion without user confirmation
- ✔ The oldest file is always preserved

# TR #

Duplicate File Finder, bilgisayardaki yinelenen (duplicate) dosyaları tespit edip güvenli bir şekilde silmeye yarayan bir Windows masaüstü uygulamasıdır.

Bu proje kişisel bir ihtiyaçtan doğmuştur:

Android telefonumu değiştirirken yedek alıp yeni telefona yükledim.  
Daha sonra tekrar telefon değiştirdiğimde eski yedeği tekrar attım.  
Sonuç olarak aynı fotoğraf ve videolardan defalarca oluşmuş kopyalar meydana geldi.  
Manuel temizlemek imkânsız hale geldiği için bu uygulamayı geliştirdim.

## 🎯 Amaç
Aynı dosyaları:
- Hızlı
- Güvenli
- Kullanıcıyı riske atmadan

tespit edip temizlemek.

## 🧩 Özellikler
- ✅ Klasör ve alt klasör tarama
- ✅ Büyük dosyalarda yüksek performans
- ✅ Hash tabanlı karşılaştırma
- ✅ Aynı dosyaları gruplama
- ✅ En eski dosyayı koruyup diğerlerini silme
- ✅ Tarama sırasında canlı ilerleme durumu
- ✅ Detaylı özet ekranı
- ✅ Silme öncesi onay sistemi
- ✅ MVVM benzeri katmanlı mimari

## 🖼️ Ekran Görüntüleri

### 1️⃣ Klasör Seçimi
Kullanıcı taranacak ana klasörü seçer.

<img width="1028" height="489" alt="Ekran görüntüsü 2026-01-21 224932" src="https://github.com/user-attachments/assets/ee9dcb01-c0ee-4f9a-99ff-b17c5135d214" />


### 2️⃣ Tarama Süreci
Dosyalar okunur, boyutlarına göre gruplanır ve hash hesaplanır.

<img width="1028" height="489" alt="Ekran görüntüsü 2026-01-21 225918" src="https://github.com/user-attachments/assets/ac328ecd-4c3c-4c65-bfa2-e265bf9c0dec" />


### 3️⃣ Özet (Summary) Ekranı
Toplam taranan dosya sayısı, duplicate dosya sayısı ve toplam boyut gösterilir.

<img width="286" height="156" alt="Ekran görüntüsü 2026-01-21 230241" src="https://github.com/user-attachments/assets/c7607f2d-d2e2-4a15-8e1f-72f37e69fdd2" />


### 4️⃣ Detay Ekranı
Duplicate dosyalar gruplar halinde listelenir.

<img width="708" height="436" alt="Ekran görüntüsü 2026-01-21 230308" src="https://github.com/user-attachments/assets/3e62d644-d452-47aa-954c-d46bbf16b9f8" />


### 5️⃣ Silme Onayı
En eski dosya korunur, diğer kopyalar silinir.

<img width="350" height="281" alt="Ekran görüntüsü 2026-01-21 230322" src="https://github.com/user-attachments/assets/0d8719a2-d083-4ad8-ab39-c7501024eaee" />


## 🧠 Uygulama Nasıl Çalışır?

### 🔍 Tarama Algoritması
1. Seçilen klasörler recursive olarak taranır
2. Dosyalar önce boyutlarına göre gruplanır
3. Aynı boyuttaki dosyalar için:
   - Hash hesaplanır (MD5 / XXHash)
   - Hash'leri aynı olan dosyalar duplicate kabul edilir
4. En eski dosya korunur
5. Diğerleri silinir

## ⚙️ Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|-----------|----------|
| .NET 8 | Ana platform |
| Windows Forms | UI |
| C# | Backend |
| Task / async | Asenkron işlemler |
| TreeView | Klasör & dosya gösterimi |
| Dependency Injection | Katmanlı mimari |
| Hash Algorithms | Dosya karşılaştırma |

## 🧱 Proje Mimarisi
**DuplicateFileFinder**
- **Application**
  - Services
  - Interfaces
  - DTOs
- **Domain**
  - Entities
- **Infrastructure**
  - FileSystem
  - Hashing
- **UI**
  - Forms
  - Views
  - Presenters


**Mimari Özellikleri:**
- ✔ Katmanlar birbirinden bağımsız
- ✔ UI → Application → Domain yönlü bağımlılık
- ✔ Test edilebilir yapı

## 🚀 Performans Optimizasyonları
- Paralel dosya tarama
- Hash yalnızca gerekli dosyalara uygulanır
- Duplicate kontrolü önce dosya boyutuna göre yapılır
- Gereksiz hash hesaplamaları önlenir
- UI thread kilitlenmez

## 🔐 Güvenlik Önlemleri
- ✔ Sistem dosyaları atlanır
- ✔ Kilitli dosyalar otomatik pas geçilir
- ✔ Kullanıcı onayı olmadan silme yapılmaz
- ✔ En eski dosya her zaman korunur
