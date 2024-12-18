﻿namespace LastMotoApp.ApplicationServices.Components.CsvReader.Models;

public class Manufacturer
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Country}, {Year}";
    }
}
