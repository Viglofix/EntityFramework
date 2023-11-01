using System.ComponentModel.DataAnnotations;

namespace DataBase.Models;

public class User
{
    public int id { get; set; }
    public required string first_name { get; set; }
    public required string last_name { get; set; }
    public char gender { get; set; }
    public required string address { get; set; }
    public required string phone_no { get; set; }
    public required string email { get; set; }
    public required string password { get; set; }
    public virtual ICollection<Rental>? Rentals { get; set; }

}