using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryProject.Data.Entities
{
    public class ExhibitionArtworkEntity:BaseEntity
    {//Bu tablo Many Many tabloları birleştirmek için oluşturuluyor
        public int ExhibitionId { get; set; }
        public int ArtworkId { get; set; }

        //Relational Property

        public ExhibitionEntity Exhibition { get; set; }
        public ArtworkEntity Artwork { get; set; }

    }
    public class ExhibitionArtworkConfiguration : BaseConfiguration<ExhibitionArtworkEntity>
    {
        public override void Configure(EntityTypeBuilder<ExhibitionArtworkEntity> builder)
        {
            builder.Ignore(x => x.Id);//Pirmary Key i siliyoruz
            builder.HasKey( "ExhibitionId","ArtworkId");//Composide Key oluştuma 

           
            base.Configure(builder);
        }
    }
}
