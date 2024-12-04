# ArcheroCase
 
Saygıdeğer Happy Hour Games Jürisi,

Herkese merhaba!

Archero Case Study projesini başarıyla tamamlamış bulunmaktayım. Bu proje sürecinde yaptıklarımı ve proje içeriğini sizlerle paylaşmaktan mutluluk duyuyorum. 
Öncelikle, oyun ayarları ve projeyi nereden oynayabileceğiniz hakkında bilgi vermek isterim.

İçerikleri anlatmadan önce projede oyun ayarları ile nerden oynayabilirsiniz biraz bunun hakkında bilgi vermek istiyorum.
   - (Assets)
        -  (_GameFolders) 
            - (Datas)
               - **[Character Data]** -> Karakterimizin MoveSpeed, RotationSpeed ve her ne kadar kullanmasakta Health değerleri bulunuyor.
                  - (Abilities) -> Yeteneklerimiz bulunuyor.
                    
                     - **[Arrow Bounce]** -> Arrow Bounce Count değerimiz var. Bu değer yetenek açık olduğunda kaç kere başka düşmanlara sekeceğini belirliyor.
                       
                     - **[Arrow Count Per Attack]** -> İsmindende anlayacağımız üzere attack başı kaç tane ok atacağımızı belirliyoruz.
                       
                     - **[Attack Speed Boost]** -> Burada DefaultAttackSpeed ve AttackSpeedMultiplier değişkenlerimiz var.
                       Multiplier değişkeni yetenek aktiften Mevcut Attack Speed'i kaç ile çarpacağını belirliyor.
                    
                     - **[BurnDamage]** -> İçerisinde BurnDamage ve BurnDuration değerleri mevcut. Okumuzun çarptığı dummy'e  kaç saniye boyunca her saniyede ne kadar damage vurmasını belirliyoruz.
                       
                     - **[Rage Mode]** -> Burada bütün yeteneklerin çarpanları var. Rage mode aktif olduğunda yetenekleri kaç ile çarpmamız gerektiğini belirliyoruz.
                    
   - Hiyerarşi'de **[Managers] -> Game Manager** üzerinde FindNearestSystem üzerinden en yakındaki dummy'i hangi yol ile bulacağını belirleyebilirsiniz.

**Kullanılan Design Patternler**
- Strategy Pattern
  - Yetenek sisteminde (Abilities) kullanıldı.
- Factory Pattern
  - Dummy nesnelerini oluşturmak için kullanıldı.
- State Pattern
  - Oyun duraklatma (Pause System) işlemlerinde uygulandı.
- Observer Pattern
  - Oyun olaylarının yönetimi için (GameEventManager) kullanıldı.
- Singleton Pattern

**Object Pooling**
Performans iyileştirmesi amacıyla şu nesnelerde Object Pooling kullanıldı:
   - Dummies
   - Arrows

**Nearest Find Dummy**
- En yakındaki düşmanı bulmak için 2 yöntem **[Linq - MySystem]** kullanılıyor. 

**NOTLAR :**
Projede GameManager içinde **[CameraOrthographicSizeSet]** metodu start'ın içinde. Eğer oyun içinde Game ekranından cihaz değiştirme işlemi yapacaksanız **[CameraOrthographicSizeSet]** bu metodu Update'in içine almayı unutmayın :)


Proje boyunca öğrendiklerimi uygulamak ve bu çalışmayı sizlere sunmak benim için büyük bir keyifti.
İlginiz ve zamanınız için teşekkür ederim.

Saygılarımla,
**[Ömer Faruk KARA]**
