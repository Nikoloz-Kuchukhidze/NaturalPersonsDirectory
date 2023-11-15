using NaturalPersonsDirectory.Domain.Primitives;

namespace NaturalPersonsDirectory.Domain.Entities;

public sealed class City : Entity<int>
{
    public City()
    {
        
    }

    private  City(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = null!;

    public static City Create(string name)
    {
        return new City(name);
    }
}