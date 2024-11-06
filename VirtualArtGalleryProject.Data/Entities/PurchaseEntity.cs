using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryProject.Data.Entities
{
    public class PurchaseEntity:BaseEntity
    {
      
        public int ArtworkId { get; set; }
        public int ExhibitionId { get; set; }

        //Relational Property
        public ExhibitionEntity Exhibition { get; set; }
        public ArtworkEntity Artwork { get; set; }
    }
    public class PurchasConfiguration : BaseConfiguration<PurchaseEntity>
    {
        public override void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
         
            base.Configure(builder);
        }
    }
}
