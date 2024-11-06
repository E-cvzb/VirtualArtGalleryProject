using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryProject.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDelete { get; set; }

    }
    public abstract class BaseConfiguration<TEntity>:IEntityTypeConfiguration<TEntity>
        where TEntity:BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x=>x.IsDelete==false);
            builder.Property(x => x.ModifiedDate)
                .IsRequired(false);
        }

     }

}
