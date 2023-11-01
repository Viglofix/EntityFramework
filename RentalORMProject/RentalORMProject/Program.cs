using DataBase.Models;
using DataBase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace RentalORMProject;

internal class Entites
{
    public IEnumerable<Rental> GetEntites()
    {
        return new List<Rental>() {
            new Rental()
            { 
                end_date= DateTime.Now,
                start_date= DateTime.Now,
                total_cost= 0,
                User = new User() {
                    first_name="Karol",
                    last_name="Budziak",
                    address="Kamienica 342",
                    email="karol@gmail.com",
                    gender='m',
                    password="budzix12",
                    phone_no="992992992",
                },
                user_id=1
            },
            new Rental()
            {
                end_date= DateTime.Now,
                start_date= DateTime.Now,
                total_cost= 0,
                User = new User() {
                    first_name="Karol",
                    last_name="Budziak",
                    address="Kamienica 342",
                    email="karol@gmail.com",
                    gender='m',
                    password="budzix12",
                    phone_no="992992992",
                },
                user_id=2
            },
            new Rental()
            { 
                end_date= DateTime.Now,
                start_date= DateTime.Now,
                total_cost= 0,
                User = new User() {
                    first_name="Karol",
                    last_name="Budziak",
                    address="Kamienica 342",
                    email="karol@gmail.com",
                    gender='m',
                    password="budzix12",
                    phone_no="992992992",
                },
                user_id=3
            },
            new Rental()
            {
                end_date= DateTime.Now,
                start_date= DateTime.Now,
                total_cost= 0,
                User = new User() {
                    first_name="Karol",
                    last_name="Budziak",
                    address="Kamienica 342",
                    email="karol@gmail.com",
                    gender='m',
                    password="budzix12",
                    phone_no="992992992",
                },
                user_id=4
            },
        };
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<RentalDbContext>(
             options => options.UseNpgsql("Server=localhost;Database=RentalORM;Port=5432;User Id=viglofix;Password=Hujbert12"))
            .BuildServiceProvider();

        using (var db = serviceProvider.CreateScope())
        {
            var dbConnection = db.ServiceProvider.GetRequiredService<RentalDbContext>();

           // dbConnection.Rentals.AddRange(new Entites().GetEntites());

            var query = dbConnection.Database.ExecuteSqlRaw(@"SELECT NEXTVAL('""Rental_Sequence""')");
                
              /*   var SelectRental = dbConnection.Rentals.ToList()?
                .Select(obj =>
                {
                    obj.id = query++;
                    return obj;
                });
                dbConnection.UpdateRange(SelectRental!); */

            
          


            dbConnection.SaveChanges();
        }

      /*  using(var db = serviceProvider.CreateScope())
        {
            var dbConnection = db.ServiceProvider.GetRequiredService<RentalDbContext>();
            var queryOne = @"ALTER TABLE ""Rentals"" DROP CONSTRAINT ""FK_Rentals_Users_user_id""";
            var query = @"ALTER TABLE ""Users"" DROP CONSTRAINT ""PK_Users""";

            dbConnection.Database.ExecuteSqlRaw(queryOne);
            dbConnection.Database.ExecuteSqlRaw(query);

            dbConnection.SaveChanges();
        } */

        /* using(var db = serviceProvider.CreateScope())
         {
             var db_context = db.ServiceProvider.GetRequiredService<RentalDbContext>();

             db_context.Database.EnsureCreated();

            db_context.Rentals.AddRange(new Entites().GetEntites());

             var sortedTable = db_context.Users
                 .OrderBy(x => x.id)
                 .ToList();
             var firstItem = sortedTable.First().id;
             var mappedQuery = sortedTable
                 .Select(obj =>
                 { 
                     obj.id = firstItem++;
                     return obj;
                 }); 


             db_context.Users.UpdateRange(mappedQuery); 

             db_context.SaveChanges();
         } */
     } 
    }

