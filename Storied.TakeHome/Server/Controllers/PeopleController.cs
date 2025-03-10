using Microsoft.AspNetCore.Mvc;
using Storied.TakeHome.Server.Helpers;
using Storied.TakeHome.Server.Models;

namespace Storied.TakeHome.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly List<Option> _people =
        [
            new() { Id = Guid.Parse("b4cd5643-dfae-5a6a-9f03-ec13ec73a653"), Label = "Jane Doe (1877-1941)" },
            new() { Id = Guid.Parse("c32b846d-134e-5cd0-b465-07f252c9616b"), Label = "Oliver Smith (-1910)" },
            new() { Id = Guid.Parse("e385e979-38dd-51b6-86e7-db1f6457adcf"), Label = "Emily R Atkinson (Living)" },
            new() { Id = Guid.NewGuid(), Label = "Thomas Wright (1902-1985)" },
            new() { Id = Guid.NewGuid(), Label = "Alice Johnson (1954-2020)" },
            new() { Id = Guid.NewGuid(), Label = "Samuel Henderson (Living)" },
            new() { Id = Guid.NewGuid(), Label = "Margaret Wilson (1890-1967)" }
        ];

        [HttpGet("all")]
        public ActionResult<IEnumerable<Option>> GetAll()
        {
            return Ok(_people);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Option>> Search([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
                return Ok(_people);

            var filtered = _people.FindAll(p =>
                p.Label.Contains(term, StringComparison.OrdinalIgnoreCase));

            return Ok(filtered);
        }
        
        [HttpGet("{familyTreeId:guid}")]
        public ActionResult<IEnumerable<Option>> GetPeopleByFamily(Guid familyTreeId)
        {
            var familyTreeData = SampleDataGenerator.GenerateFamilyTreeData();
            if (!familyTreeData.TryGetValue(familyTreeId, out var people))
            {
                return NotFound("Family tree not found.");
            }

            var dropdownItems = people.Select(p => new Option
            {
                Id = p.Id,
                Label = $"{p.GivenName} {p.Surname} {GetLifeSpan(p)}"
            }).ToList();

            return Ok(dropdownItems);
        }
        
        private static string GetLifeSpan(Person person)
        {
            if (person is { BirthDate: not null, DeathDate: not null })
            {
                return $"({person.BirthDate.Value.Year}-{person.DeathDate.Value.Year})";
            }
            switch (person.BirthDate)
            {
                case null when person.DeathDate.HasValue:
                    return $"(-{person.DeathDate.Value.Year})";
                case null when !person.DeathDate.HasValue:
                    return "(Living)";
            }

            if (!person.BirthDate.HasValue || person.DeathDate.HasValue) return string.Empty;
            var birthYear = person.BirthDate.Value.Year;
            var currentYear = DateTime.Now.Year;
            
            return currentYear - birthYear < 120 ? "(Living)" : $"({birthYear}-)";
        }
    }
}
