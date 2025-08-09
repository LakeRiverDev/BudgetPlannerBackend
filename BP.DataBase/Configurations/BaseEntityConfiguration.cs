//using BP.DataBase.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace BP.DataBase.Configurations
//{
//    public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity<Guid>>
//    {        
//        public void Configure(EntityTypeBuilder<BaseEntity<Guid>> builder)
//        {
//            builder.Property(p => p.CreatedOn)
//                .HasColumnType("timestamp with time zone");

//            builder.Property(p => p.UpdatedOn)
//                .HasColumnType("timestamp with time zone");
//        }
//    }
//}
