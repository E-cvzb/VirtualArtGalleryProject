using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryProject.Data.Entities
{
    public class ExhibitionEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Date { get; set; }

        //Relatipnal Property
        public ICollection<ExhibitionArtworkEntity> ExhibitionArtwork { get; set; }
        public ICollection<PurchaseEntity> Purchase { get; set; }
    }
    public class ExhibitionConfiguration : BaseConfiguration<ExhibitionEntity>
    {
        public override void Configure(EntityTypeBuilder<ExhibitionEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            base.Configure(builder);
        }

    }
}
