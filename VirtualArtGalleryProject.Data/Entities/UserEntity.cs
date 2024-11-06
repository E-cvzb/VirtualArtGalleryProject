using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryProject.Data.Enums;

namespace VirtualArtGalleryProject.Data.Entities
{
    public class UserEntity:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserType UserType { get; set; }

        //Relational Property
        public ICollection<ArtworkEntity> Artwork { get; set; }

    }
    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.LastName) 
                .IsRequired()
                .HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
