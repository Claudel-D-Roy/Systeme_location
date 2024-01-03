using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tp1_CoreApplication.Domain;
using Tp1_CoreApplication.Enums;

namespace Tp1_CoreApplication.Data
{
    public static class seed
    {
        public static readonly PasswordHasher<User> PASSWORD_HASHER = new();

        public static void Seed(this ModelBuilder builder)
        {
            //Role
            var adminRole = AddRole(builder, "Administrator");
            var commisRole = AddRole(builder, "Commis");
            var gerantRole = AddRole(builder, "Gérant");

            //User
            var adminUser1 = AddUser(builder, "administrator", "Qwerty123!");
            var commisUser1 = AddUser(builder, "commis123456", "Qwerty123!");
            var gerantUser1 = AddUser(builder, "gerant123456", "Qwerty123!");

            //Assign role to user
            AddUserToRole(builder, adminUser1, adminRole);
            AddUserToRole(builder, commisUser1, commisRole);
            AddUserToRole(builder, gerantUser1, gerantRole);

            //Add car 
            var car1 = AddCar(builder, 1, Status.Active, States.New, true, "ToyotaCorolla", "5648975", "4A6D5-3JD2S", "Toyota", "Corolla", 2018, "red", 100000, 15000, false, 1);

            var car2 = AddCar(builder, 2, Status.Active, States.Used, true, "HondaCivic", "1234567", "8J5S2-1HN3D", "Honda", "Civic", 2015, "blue", 80000, 12000, false, 2);

            var car3 = AddCar(builder, 3, Status.Inactive, States.New, false, "FordMustang", "9876543", "5G3H2-9KM1L", "Ford", "Mustang", 2020, "yellow", 5000, 30000, false, 3);

            var car4 = AddCar(builder, 4, Status.Active, States.New, true, "ChevroletMalibu", "2468135", "2D3J4-5LK6S", "Chevrolet", "Malibu", 2022, "black", 2000, 25000, true, 4);

            var car5 = AddCar(builder, 5, Status.Active, States.Used, true, "NissanAltima", "5432167", "6S2L5-4K3J2", "Nissan", "Altima", 2017, "silver", 60000, 14000, false, 5);

            var car6 = AddCar(builder, 6, Status.Inactive, States.Used, false, "BMW320i", "7896541", "1N3M2-7K5S4", "BMW", "320i", 2019, "white", 10000, 28000, true, 1);

            var car7 = AddCar(builder, 7, Status.Active, States.New, true, "MercedesBenzC-Class", "1357924", "9S4K2-3L6M5", "Mercedes-Benz", "C-Class", 2021, "gray", 500, 35000, true, 2);

            var car8 = AddCar(builder, 8, Status.Active, States.Used, true, "AudiA4", "2468135", "3M5L2-6K4J3", "Audi", "A4", 2016, "brown", 90000, 18000, false, 3);

            var car9 = AddCar(builder, 9, Status.Inactive, States.Used, false, "VolkswagenGolf", "7531592", "4L6K2-1M3N5", "Volkswagen", "Golf", 2014, "green", 120000, 10000, false, 1);

            var car10 = AddCar(builder, 10, Status.Active, States.New, true, "KiaSorento", "9513574", "2N5M4-8L3K1", "Kia", "Sorento", 2023, "purple", 100, 40000, true, 2);

            var car11 = AddCar(builder, 11, Status.Active, States.Used, true, "HyundaiElantra", "3571592", "7M5N4-1L3K5", "Hyundai", "Elantra", 2013, "orange", 150000, 8000, false, 2);

            var car12 = AddCar(builder, 12, Status.Active, States.New, true, "TeslaModel3", "1597532", "1M2N3-4L5K6", "Tesla", "Model 3", 2021, "silver", 1000, 45000, true, 1);

            var car13 = AddCar(builder, 13, Status.Active, States.Used, true, "SubaruOutback", "3698521", "5N6M7-8L9K1", "Subaru", "Outback", 2019, "blue", 35000, 25000, false, 4);

            var car14 = AddCar(builder, 14, Status.Inactive, States.Used, false, "MazdaCX-5", "2587413", "1L2K3-4M5N6", "Mazda", "CX-5", 2017, "red", 60000, 15000, false, 1);

            var car15 = AddCar(builder, 15, Status.Active, States.New, true, "JeepWrangler", "9517532", "6N5M4-3L2K1", "Jeep", "Wrangler", 2022, "green", 500, 35000, true, 2);

            var car16 = AddCar(builder, 16, Status.Active, States.Used, true, "ChevroletEquinox", "7531594", "9N8M7-6L5K4", "Chevrolet", "Equinox", 2016, "black", 80000, 18000, false, 1);

            var car17 = AddCar(builder, 17, Status.Inactive, States.New, false, "GMC Sierra", "3579514", "4N3M2-1L8K6", "GMC", "Sierra", 2015, "white", 120000, 10000, false, 1);




            //Add branches
            var branch1 = AddBranch(builder, 1, "CarDealerPlus", "123 avenue", "Smith Street", "Toronto", "Ontario", "M4B 1B3", "Canada", true);

            var branch2 = AddBranch(builder, 2, "CarDealerGo", "456 Rue de l'Électronique", "Rue Principale", "Montréal", "Québec", "H2X 1Y6", "Canada", true);

            var branch3 = AddBranch(builder, 3, "CarDealerPlus2", "789 Avenue du Design", "Rue Sainte-Catherine", "Montréal", "Québec", "H3B 1K9", "Canada", true);

            var branch4 = AddBranch(builder, 4, "CarDealerPlus3", "10 Rue de la Beauté", "Rue de la Mode", "Toronto", "Ontario", "M5H 1T1", "Canada", false);

            var branch5 = AddBranch(builder, 5, "OutdoorGear", "987 Rue des Aventuriers", "Rue du Plein Air", "Vancouver", "Colombie-Britannique", "V6B 5K3", "Canada", false);

        }

        private static void AddUserToRole(ModelBuilder builder, User applicationUser, IdentityRole<Guid> adminRole)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = applicationUser.Id,
                RoleId = adminRole.Id,
            });
        }

        private static IdentityRole<Guid> AddRole(ModelBuilder builder, string name)
        {
            var newRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = name,
                NormalizedName = name.ToUpper()
            };
            builder.Entity<IdentityRole<Guid>>().HasData(newRole);

            return newRole;
        }
        private static User AddUser(ModelBuilder builder,
         string userName, string password)
        {
            var newUser = new User(userName)
            {
                Id = Guid.NewGuid(),
                NormalizedUserName = userName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            newUser.PasswordHash = PASSWORD_HASHER.HashPassword(newUser, password);
            builder.Entity<User>().HasData(newUser);

            return newUser;
        }

        //Add branch
        private static Branch AddBranch(ModelBuilder builder, int id,
            string name, string streetNumber, string streetName, string city,
            string province, string postalCode, string country,
            bool activatedBranch)
        {
            var newBranch = new Branch()
            {
                Id = id,
                Name = name,
                StreetName = streetName,
                City = city,
                StreetNumber = streetNumber,
                ActiveBranch = activatedBranch,
                PostalCode = postalCode,
                Province = province,
                Country = country,
            };
            builder.Entity<Branch>().HasData(newBranch);

            return newBranch;
        }

        //Add car
        private static Car AddCar(ModelBuilder builder, int id, Status status, States state, bool available,
            string carName, string serialNumber, string registration,
            string brand, string model, int year, string color, uint kilometers,
            decimal EsimatedValue, bool activated, int BranchId)
        {
            var newCar = new Car()
            {
                ID = id,
                status = status,
                State = state,
                Available = available,
                CarName = carName,
                SerialNumber = serialNumber,
                Registration = registration,
                Brand = brand,
                Model = model,
                Year = year,
                Color = color,
                Kilometers = kilometers,
                EstimatedValue = EsimatedValue,
                Activated = activated,
                BranchId = BranchId
            };
            builder.Entity<Car>().HasData(newCar);

            return newCar;
        }

    }
}
