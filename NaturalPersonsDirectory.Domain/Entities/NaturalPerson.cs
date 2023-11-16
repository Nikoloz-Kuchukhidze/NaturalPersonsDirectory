using NaturalPersonsDirectory.Domain.Enums;
using NaturalPersonsDirectory.Domain.Primitives;

namespace NaturalPersonsDirectory.Domain.Entities;

public sealed class NaturalPerson : Entity<long>, IAuditableEntity
{
    private readonly List<NaturalPersonRelation> _relations = new();
    private readonly List<Phone> _phones = new();

    private NaturalPerson()
    {
        
    }

    private NaturalPerson(
        string firstName,
        string lastName,
        Gender gender,
        string personalNumber,
        DateOnly birthDate,
        int cityId)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        PersonalNumber = personalNumber;
        BirthDate = birthDate;
        CityId = cityId;
    }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Gender Gender { get; private set; }
    public string PersonalNumber { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; }
    public string? Image { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTimeOffset CreatedOn { get; private init; }
    public DateTimeOffset? UpdatedOn { get; private set; }

    public int? CityId { get; private set; }
    public City? City { get; private set; }
    public IReadOnlyCollection<NaturalPersonRelation> Relations => _relations.AsReadOnly();
    public IReadOnlyCollection<Phone> Phones => _phones.AsReadOnly();

    public static NaturalPerson Create(
        string firstName,
        string lastName,
        Gender gender,
        string personalNumber,
        DateOnly birthDate,
        int cityId,
        IEnumerable<Phone> phones,
        IEnumerable<NaturalPersonRelation>? relations)
    {
        return new NaturalPerson(
            firstName,
            lastName,
            gender,
            personalNumber,
            birthDate,
            cityId)
            .AddPhones(phones)
            .AddRelations(relations);
    }

    public void Update(
        string? firstName,
        string? lastName,
        Gender? gender,
        DateOnly? birthDate)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            FirstName = firstName!;
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            LastName = lastName!;
        }

        if (gender.HasValue)
        {
            Gender = gender.Value;
        }

        if (birthDate.HasValue)
        {
            BirthDate = birthDate.Value;
        }
    }

    public void AddRelation(NaturalPerson naturalPerson, RelationType relationType)
    {
        

        _relations.Add(
            new NaturalPersonRelation(
                relationType,
                this,
                naturalPerson));
    }

    public void AddImage(string image)
    {
        Image = image;
    }

    public void Delete()
    {
        IsActive = false;
        _relations.Clear();
        _phones.Clear();
        Image = null;
    }

    public void UpdateCity(int cityId)
    {
        CityId = cityId;
    }

    public void UpdatePersonalNumber(string personalNumber)
    {
        PersonalNumber = personalNumber;
    }

    public void UpdatePhones(IEnumerable<Phone> phones)
    {
        _phones.Clear();
        _phones.AddRange(phones);
    } 

    private NaturalPerson AddPhones(IEnumerable<Phone> phones)
    {
        _phones.AddRange(phones);

        return this;
    }

    private NaturalPerson AddRelations(IEnumerable<NaturalPersonRelation>? relations)
    {
        _relations.AddRange(relations ?? Enumerable.Empty<NaturalPersonRelation>());

        return this;
    }
}