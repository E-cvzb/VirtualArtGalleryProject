using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryProject.Data.Entities
{
    public class ArtworkEntity:BaseEntity
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int  UserId { get; set; }
        
        

        //Relational Property
        public UserEntity User { get; set; }
        public PurchaseEntity Purchase { get; set; }
        public ICollection<ExhibitionArtworkEntity> ExhibitionArtwork { get; set; }

    }
    public class ArtworkConfiguration : BaseConfiguration<ArtworkEntity>
    {
        public override void Configure(EntityTypeBuilder<ArtworkEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            base.Configure(builder);
        }
    }
}
