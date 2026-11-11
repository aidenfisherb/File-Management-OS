namespace FileManagementOS.Shared;

// makes random fictional records.
// The phone number is NOT random: it marks the batch and position so we can check order.

public static class RecordGenerator
{
    private static readonly string[] FirstNames = ["Jane", "Sam", "Maria", "Liam", "Ava", "Noah", "Zoe", "Mia"];
    private static readonly string[] LastNames = ["Doe", "Lee", "Garcia", "Smith", "Nguyen", "Brown", "Patel", "Clark"];
    private static readonly string[] Streets = ["Oak St", "Maple Ave", "Pine Rd", "Cedar Ln"];

    // city, state, ZIP kept together; Boston and Newark ZIPs start with 0 to prove leading zeros survive
    private static readonly (string City, string State, string Zip)[] Places =
    [
        ("Boston", "MA", "02134"),
        ("Newark", "NJ", "07102"),
        ("Birmingham", "AL", "35229"),
        ("Denver", "CO", "80202"),
    ];

    public static List<Person> Generate(int count, int batch)
    {
        var random = Random.Shared;
        var people = new List<Person>();

        for (int i = 1; i <= count; i++)
        {
            var place = Places[random.Next(Places.Length)];

            people.Add(new Person(
                FirstName: FirstNames[random.Next(FirstNames.Length)],
                LastName: LastNames[random.Next(LastNames.Length)],
                DateOfBirth: $"{random.Next(1960, 2006)}-{random.Next(1, 13):00}-{random.Next(1, 29):00}",
                Phone: $"205-555-{batch}{i:000}",
                Street: $"{random.Next(1, 9999)} {Streets[random.Next(Streets.Length)]}",
                City: place.City,
                State: place.State,
                Zip: place.Zip));
        }

        return people;
    }
}
