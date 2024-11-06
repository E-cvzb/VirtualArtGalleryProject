using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryProject.Data.Context;
using VirtualArtGalleryProject.Data.Entities;

namespace VirtualArtGalleryProject.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly VirtualArtGalleryProjectDbContext _context;
        private readonly DbSet<TEntity> _set;
        public Repository(VirtualArtGalleryProjectDbContext context)
        {
            _context = context;
            _set =_context.Set<TEntity>();
        }
        public void Add(TEntity entity)//Veri tabanına veri ekleme metotu
        {
           entity.CreateDate = DateTime.Now;
            _set.Add(entity);
        }

        public void Delete(TEntity entity,bool softDelete = true)
        {
            if (softDelete == true)//Veri tabanından soft delete yapma metotu
            {
                entity.ModifiedDate = DateTime.Now;
                entity.IsDelete = true;
                _set.Update(entity);
            }else//Veri tabanına hard delete
            {
                _set.Remove(entity);
            }
           
        }

        public void Delete(int id)//Id verildiğinde delete işlemi
        {
            var entity = _set.Find(id);
            Delete(entity);//Üstte tanımladığımız metota gönderiyoruz
            
        }

        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _set.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return predicate is null ? _set : _set.Where(predicate);
        }

        public TEntity GetById(int id)//Id verilen değeri yakalamak için metot 
        {
            return _set.Find(id);
        }

        public void Update(TEntity entity)//Update işlemi
        {
            entity.ModifiedDate = DateTime.Now;
            _set.Update(entity);

        }
    }
}
