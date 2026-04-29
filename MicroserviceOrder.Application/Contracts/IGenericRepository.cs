using MicroserviceOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Application.Contracts
{
    // TId: Kimlik tipi (int, Guid), TEntity: BaseEntity'den türeyen sınıflar için genel arayüz
    public interface IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
    { 
        public Task<bool> AnyAsync(TId id); // Verilen ID'ye sahip bir kaydın veritabanında olup olmadığını kontrol eder

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);// Belirli bir şarta uyan herhangi bir kaydın varlığını kontrol eder

        Task<List<TEntity>> GetAllAsync();   // Tablodaki tüm verileri asenkron olarak liste halinde getirir /asenkron şekilde tasktan dolayı

        Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);// Verileri belirli sayfa numarası ve sayfa boyutuyla parçalar halinde getirir (Sayfalama) . asenkron şekilde tasktan dolayı 

        ValueTask<TEntity?> GetByIdAsync(TId id);// Verilen ID'ye sahip kaydı bulur; yoksa null döner (Bellek yönetimi için ValueTask kullanır)

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);// Belirtilen filtreye uygun verileri henüz veritabanından çekmeden sorgu olarak hazırlar

        void Add(TEntity entity);// Yeni bir veriyi veritabanına eklenmek üzere takip listesine (context) dahil eder

        void Update(TEntity entity);// Mevcut bir verinin içeriğini güncellemek üzere işaretler

        void Remove(TEntity entity);// Belirtilen veriyi veritabanından silmek üzere işaretler
    }

    /*ValueTask, aslında Task ile aynı işi yapar (asenkron bir operasyonu temsil eder) 
     * ancak performans ve bellek yönetimi açısından kritik bir farkı vardır.
     * En basit haliyle: Task bir referans tipidir (class), ValueTask ise bir değer tipidir (struct).
     * ValueTask: Belleğin "Stack" bölgesinde tutulur. 
     * Nesne oluşturma maliyeti neredeyse sıfırdır ve Garbage Collector'a yük bindirmez.*/
}
