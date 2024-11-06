using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryProject.Data.Context;

namespace VirtualArtGalleryProject.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VirtualArtGalleryProjectDbContext _context;
        private IDbContextTransaction _transaction;
        public UnitOfWork(VirtualArtGalleryProjectDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransaction()//Veri tabanında işlem sırasında aşamalıkayıt yaparak işlem güvenliğini tanımlamak için tanımlıyoruz
        {
            _transaction=await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()//Hata alınmadığında kayıt işleminin tamamlanmasını sağlar
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            //Garbege Collector silme işlemi yapılacağı zaman  ilk önce burdan başlar 
        }

        public async Task RollBackTransaction()//Hata oluştuğunda bu zamana kadar kayıt edilen verileri siler ve başa döner
        {
            await _transaction.RollbackAsync();
        }

        public Task<int> SaveChangesAsync()
        {
          return _context.SaveChangesAsync();
        }
    }
}
