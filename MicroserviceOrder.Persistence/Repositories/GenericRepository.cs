using MicroserviceOrder.Application.Contracts.Repositories;
using MicroserviceOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Persistence.Repositories
{
    public class GenericRepository<TId, TEntity>(AppDbContext context) // veritabani baglantisini constructorla iceri aliyoz burda
     : IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId> // Application katmanından (iç katman) IGenericReposirory implemnte ediliyor
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>(); // tabloyu degiskene atiyoz her seferinde yazmamak icin

        protected AppDbContext Context = context; // contexti diger yerlerde de kullanmak icin korumali sakliyoz


        public Task<bool> AnyAsync(TId id) // idye gore veri varmi yokmu ona bakior
        {
            return _dbSet.AnyAsync(x => x.Id.Equals(id)); // dbden idleri karsilastirip sonucu firlatiyor. x uzerınden arama yapıldı 
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) // buda yolladigimiz sarta gore varmi yokmu diye bakiyor
        {
            return _dbSet.AnyAsync(predicate); // gonderdigimiz filtreyi dbde kontrol ediyor
        }

        public Task<List<TEntity>> GetAllAsync() // tablodaki herseyi liste olarak getiriyo bize
        {
            return _dbSet.ToListAsync(); // değişkeni listeye cevirip asenkron yolluyor
        }

        public Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize) // sayfali sekilde veri getirmeye yariyor
        {
            // 1,10 => 1..10
            // 2,10 => 11..20
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(); // su kadarini atla su kadarini al diyoruz
        }

        public ValueTask<TEntity?> GetByIdAsync(TId id) // idye gore tek bir satir veri cekmek icin //valuetask daha performanslı task turu
        {
            return _dbSet.FindAsync(id); // veritabaninda o idli elemani buluyor
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) // IQueryable, veritabanı sorgularının en performanslı şekilde çalışmasını sağlayan bir yapıdır.
        {
            return _dbSet.Where(predicate); // filtreyi dbye gonderilcek sekilde ayarliyor
        }

        public void Add(TEntity entity) // yeni veri eklemek icin siraya aliyor
        {
            _dbSet.Add(entity); // tabloya ekleme komutunu veriyor
        }

        public void Update(TEntity entity) // var olan veriyi guncellemek icin isaretliyor
        {
            _dbSet.Update(entity); // dbset uzerinden guncelleme yapiyor
        }

        public void Remove(TEntity entity) // veriyi silmek icin listeye ekliyor
        {
            _dbSet.Remove(entity); // silme islemini baslatiyor
        }
    }
    /*readonly, bir değişkene sadece tanımlandığı anda veya sınıfın constructor (yapıcı) metodu içinde 
     * değer atanabilmesini sağlayan bir anahtar kelimedir.
     * Bir kez değer atandıktan sonra, programın çalışma süresi boyunca o değer değiştirilemez.*/
}
