using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oil.Dal.Mappings;
using Oil.Domain.Entity.Entities;
using System;

namespace Oil.Dal
{
    public class OilDbContext: IdentityDbContext<ApplicationUser>
    {
        public OilDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Well> Wells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new ShopMap());
            modelBuilder.ApplyConfiguration(new FieldMap());
            modelBuilder.ApplyConfiguration(new WellMap());
            modelBuilder.ApplyConfiguration(new WellTypeMap());

            var company1 = new Company { Id = 1, Name = "ПАО \"Газпром\"", ShortName = "ГПН" };
            var company2 = new Company { Id = 2, Name = "ПАО \"Лукойл\"", ShortName = "ЛК" };
            var company3 = new Company { Id = 3, Name = "ПАО \"БашНефть\"", ShortName = "БН" };
            var company4 = new Company { Id = 4, Name = "ЗАО \"Турсунт\"", ShortName = "ТУР" };

            modelBuilder.Entity<Company>().HasData(company1, company2, company3, company4);


            var shop1 = new Shop { Id = 1, CompanyId = company2.Id, Name = "ЦДНГ-1" };
            var shop2 = new Shop { Id = 2, CompanyId = company2.Id, Name = "ЦДНГ-2" };
            var shop3 = new Shop { Id = 3, CompanyId = company1.Id, Name = "ЦДНГ-10" };
            var shop4 = new Shop { Id = 4, CompanyId = company1.Id, Name = "ЦДНГ-11" };
            var shop5 = new Shop { Id = 5, CompanyId = company3.Id, Name = "ЦДНГ-1" };
            var shop6 = new Shop { Id = 6, CompanyId = company4.Id, Name = "ЦДНГ-1" };

            modelBuilder.Entity<Shop>().HasData(shop1, shop2, shop3, shop4, shop5, shop6);

            var field1 = new Field { Id = 1, CompanyId = company1.Id, Name = "Яхлинское м." };
            var field2 = new Field { Id = 2, CompanyId = company1.Id, Name = "Тальниковое м." };
            var field3 = new Field { Id = 3, CompanyId = company1.Id, Name = "Шушминское м." };

            var field4 = new Field { Id = 4, CompanyId = company2.Id, Name = "Тугровское м." };
            var field5 = new Field { Id = 5, CompanyId = company2.Id, Name = "Шаимское м." };
            var field6 = new Field { Id = 6, CompanyId = company2.Id, Name = "Сыморьяхское м." };

            var field7 = new Field { Id = 7, CompanyId = company4.Id, Name = "Толумское м." };
            var field8 = new Field { Id = 8, CompanyId = company4.Id, Name = "Ловинское м." };


            modelBuilder.Entity<Field>().HasData(field1, field2, field3, field4, field5, field6, field7, field8);

            var wellType1 = new WellType { Id = 1, Name = "Нефтяная" };
            var wellType2 = new WellType { Id = 2, Name = "Нагнетательная" };
            var wellType3 = new WellType { Id = 3, Name = "Водозаборная" };

            modelBuilder.Entity<WellType>().HasData(wellType1, wellType2, wellType3);

            //var well1 = new Well { Id = 1, FieldId = field1.Id, Name = "10010", ShopId = shop3.Id };
            //var well2 = new Well { Id = 2, FieldId = field1.Id, Name = "100Р",  ShopId = shop3.Id };
            //var well3 = new Well { Id = 3, FieldId = field2.Id, Name = "101Р",  ShopId = shop4.Id };
            //var well4 = new Well { Id = 4, FieldId = field4.Id, Name = "1В", ShopId = shop1.Id };
            //var well5 = new Well { Id = 5, FieldId = field4.Id, Name = "456", ShopId = shop2.Id };
            //var well6 = new Well { Id = 6, FieldId = field5.Id, Name = "111",  ShopId = shop1.Id };
            //var well7 = new Well { Id = 7, FieldId = field5.Id, Name = "20089", ShopId = shop1.Id };
            //var well8 = new Well { Id = 8, FieldId = field6.Id, Name = "49",  ShopId = shop2.Id };
            //var well9 = new Well { Id = 9, FieldId = field7.Id, Name = "1011",  ShopId = shop6.Id };
            //var well10 = new Well { Id = 10, FieldId = field8.Id, Name = "2345",  ShopId = shop6.Id };
            //var well11 = new Well { Id = 11, FieldId = field8.Id, Name = "99Н",  ShopId = shop6.Id };

            var well1 = new Well { Id = 1, FieldId = field1.Id, Name = "10010", CompanyId = company1.Id, ShopId = shop3.Id, Altitude = 100, ZabI = 1300, ZabF = 1315, WellTypeId = wellType1.Id };
            var well2 = new Well { Id = 2, FieldId = field1.Id, Name = "100Р", CompanyId = company1.Id, ShopId = shop3.Id, Altitude = 55, ZabI = 1988, ZabF = 2002, WellTypeId = wellType2.Id };
            var well3 = new Well { Id = 3, FieldId = field2.Id, Name = "101Р", CompanyId = company1.Id, ShopId = shop4.Id, Altitude = 232, ZabI = 1911, ZabF = 2002, WellTypeId = wellType2.Id };
            var well4 = new Well { Id = 4, FieldId = field4.Id, Name = "1В", CompanyId = company2.Id, ShopId = shop1.Id, Altitude = 1, ZabI = 1911, ZabF = 2002, WellTypeId = wellType2.Id };
            var well5 = new Well { Id = 5, FieldId = field4.Id, Name = "456", CompanyId = company2.Id, ShopId = shop2.Id, Altitude = 23, ZabI = 1922, ZabF = 2002, WellTypeId = wellType2.Id };
            var well6 = new Well { Id = 6, FieldId = field5.Id, Name = "111", CompanyId = company2.Id, ShopId = shop1.Id, Altitude = 33, ZabI = 1933, ZabF = 2002, WellTypeId = wellType2.Id };
            var well7 = new Well { Id = 7, FieldId = field5.Id, Name = "20089", CompanyId = company2.Id, ShopId = shop1.Id, Altitude = 44, ZabI = 1944, ZabF = 2002, WellTypeId = wellType2.Id };
            var well8 = new Well { Id = 8, FieldId = field6.Id, Name = "49", CompanyId = company2.Id, ShopId = shop2.Id, Altitude = 55, ZabI = 1955, ZabF = 2002, WellTypeId = wellType2.Id };
            var well9 = new Well { Id = 9, FieldId = field7.Id, Name = "1011", CompanyId = company4.Id, ShopId = shop6.Id, Altitude = 66, ZabI = 1966, ZabF = 2002, WellTypeId = wellType2.Id };
            var well10 = new Well { Id = 10, FieldId = field8.Id, Name = "2345", CompanyId = company4.Id, ShopId = shop6.Id, Altitude = 77, ZabI = 1966, ZabF = 2002, WellTypeId = wellType2.Id };
            var well11 = new Well { Id = 11, FieldId = field8.Id, Name = "99Н", CompanyId = company4.Id, ShopId = shop6.Id, Altitude = 88, ZabI = 1977, ZabF = 2002, WellTypeId = wellType2.Id };

            modelBuilder.Entity<Well>().HasData(well1, well2, well3, well4, well5, well6, well7, well8, well9, well10, well11);
        }
    }
}
