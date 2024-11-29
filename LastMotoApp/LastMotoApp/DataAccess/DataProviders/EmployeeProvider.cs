using LastMotoApp.DataAccess.Data.Entities;

namespace LastMotoApp.DataAccess.DataProviders;

public class EmployeeProvider : IEmployeeProvider
{
    public List<Employee> GenerateSampleEmployees()
    {
        return new List<Employee>
        {
            new() {Id =  1, FirstName = "Chris", LastName = "Evans"},
            new() {Id =  2, FirstName = "Elizabeth", LastName = "Olsen"},
            new() {Id =  3, FirstName = "Scarlett", LastName = "Johansson"},
            new() {Id =  4, FirstName = "Robert", LastName = "Downey Jr"},
            new() {Id =  5, FirstName = "Chris", LastName = "Hemsworth"},
            new() {Id =  6, FirstName = "Emily", LastName = "van Camp"},
            new() {Id =  7, FirstName = "Chris", LastName = "Pratt"},
            new() {Id =  8, FirstName = "Lily", LastName = "Collins"},
            new() {Id =  9, FirstName = "Paul", LastName = "Walker"},
            new() {Id =  10, FirstName = "Vin", LastName = "Diesel"},
            new() {Id =  11, FirstName = "Jordana", LastName = "Brewster"},
            new() {Id =  12, FirstName = "Tyres", LastName = "Gibson"},
            new() {Id =  13, FirstName = "Chris", LastName = "Bridges"},
            new() {Id =  14, FirstName = "Dwayne", LastName = "Johnson"},
            new() {Id =  15, FirstName = "Ben", LastName = "Affleck"},
            new() {Id =  16, FirstName = "Emilia", LastName = "Clarke"},
            new() {Id =  17, FirstName = "Gal", LastName = "Gadot"},
            new() {Id =  18, FirstName = "Henry", LastName = "Cavill"},
            new() {Id =  19, FirstName = "Hayley", LastName = "Atwell"},
            new() {Id =  20, FirstName = "Cobie", LastName = "Smulders"},
            new() {Id =  21, FirstName = "Zendaya", LastName = "Coleman"},
            new() {Id =  22, FirstName = "Natalie", LastName = "Portman"},
            new() {Id =  23, FirstName = "Jessica", LastName = "Alba"},
            new() {Id =  24, FirstName = "Tom", LastName = "Hiddleston"},
            new() {Id =  25, FirstName = "Chadwick", LastName = "Boseman"},
            new() {Id =  26, FirstName = "Tom", LastName = "Holland"},
            new() {Id =  27, FirstName = "Evangeline", LastName = "Lilly"},
            new() {Id =  28, FirstName = "Benedict", LastName = "Cumberbatch"},
            new() {Id =  29, FirstName = "Brie", LastName = "Larson"},
            new() {Id =  30, FirstName = "Margot", LastName = "Robbie"},
            new() {Id =  31, FirstName = "Natalie", LastName = "Emmanuel"},
            new() {Id =  32, FirstName = "Amber", LastName = "Heard"},
            new() {Id =  33, FirstName = "Jason", LastName = "Statham"},
            new() {Id =  34, FirstName = "Jason", LastName = "Momoa"},
            new() {Id =  35, FirstName = "Sung", LastName = "Kang"},
            new() {Id =  36, FirstName = "Elsa", LastName = "Pataky"},
            new() {Id =  37, FirstName = "Lucas", LastName = "Black"},
        };
    }
}
