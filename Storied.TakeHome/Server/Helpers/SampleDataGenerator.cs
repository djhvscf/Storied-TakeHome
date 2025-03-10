using Bogus;
using Storied.TakeHome.Server.Models;
using Person = Storied.TakeHome.Server.Models.Person;

namespace Storied.TakeHome.Server.Helpers;

public static class SampleDataGenerator
{
    public static Dictionary<Guid, List<Person>> GenerateFamilyTreeData()
    {
        var familyTreeId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var familyTreeId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var data = new Dictionary<Guid, List<Person>>();

        var personFaker = new Faker<Person>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.GivenName, f => f.Name.FirstName())
            .RuleFor(p => p.Surname, f => f.Name.LastName())
            .RuleFor(p => p.Gender, f => f.PickRandom<Gender>())
            .RuleFor(p => p.BirthLocation, f => f.Address.City())
            .RuleFor(p => p.DeathLocation, f => f.Address.City());

        var person1 = personFaker.Clone()
            .RuleFor(p => p.BirthDate, f => f.Date.Past(150, DateTime.Now.AddYears(-100)))
            .RuleFor(p => p.DeathDate, (f, p) => f.Date.Between(p.BirthDate.Value.AddYears(30), DateTime.Now.AddYears(-10)))
            .Generate();

        var person2 = personFaker.Clone()
            .RuleFor(p => p.BirthDate, f => null)
            .RuleFor(p => p.DeathDate, f => f.Date.Past(100))
            .Generate();

        var person3 = personFaker.Clone()
            .RuleFor(p => p.BirthDate, f => null)
            .RuleFor(p => p.DeathDate, f => null)
            .Generate();

        var person4 = personFaker.Clone()
            .RuleFor(p => p.BirthDate, f => f.Date.Past(100))
            .RuleFor(p => p.DeathDate, f => null)
            .Generate();

        var person5 = personFaker.Clone()
            .RuleFor(p => p.BirthDate, f => f.Date.Past(150, DateTime.Now.AddYears(-120)))
            .RuleFor(p => p.DeathDate, f => null)
            .Generate();

        var family1 = new List<Person> { person1, person2, person3, person4, person5 };
        data.Add(familyTreeId1, family1);

        var family2 = personFaker.Generate(5);
        data.Add(familyTreeId2, family2);

        return data;
    }
}