namespace VirtualPet.Domain.Pet;

public class Pet
{
    private Guid Id { get; }
    private Guid OwnerId { get; }
    private PetName Name { get; }
    private PetAge Age { get; }
    private PetStats Stats { get; }

    private Pet(Guid id, Guid ownerId, PetName name, PetAge age, PetStats stats)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Age = age;
        Stats = stats;
    }

    public static Pet Create(Guid id, Guid ownerId, string name)
    {
        return new(id, ownerId, PetName.Create(name), PetAge.Create(0), new PetStats(50, 50, 50, 100));
    }

    public PetDto ToDto()
    {
        return new PetDto(Id, OwnerId, Name.Value, Age.Value, Stats.Hunger, Stats.Happiness, Stats.Energy, Stats.Health);
    }
}

internal record PetName
{
    public const int MaxLength = 50;
    public string Value { get; }

    private PetName(string value)
    {
        Value = value;
    }

    public static PetName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(value));
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"Name cannot be longer than {MaxLength} characters.", nameof(value));
        }

        return new(value);
    }
}

internal record PetAge
{
    public int Value { get; }

    private PetAge(int value)
    {
        Value = value;
    }

    public static PetAge Create(int value)
    {
        return value < 0 ? throw new ArgumentException("Age cannot be negative.", nameof(value)) : new(value);
    }
}

internal record PetStats(int Hunger, int Happiness, int Energy, int Health);


public record PetDto(Guid Id, Guid OwnerId, string Name, int Age, int Hunger, int Happiness, int Energy, int Health);