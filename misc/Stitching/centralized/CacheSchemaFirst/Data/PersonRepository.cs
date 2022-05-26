namespace Demo.Data;

public class PersonRepository
{
    public List<Person> _persons = new List<Person>
    {
        new Person(){Id= 1,Name= "Amanda" },
        new Person(){Id= 2,Name= "Jon" },
        new Person(){Id= 3,Name= "Chloe" },
        new Person(){Id=4, Name="Bill" }
    };

    public IEnumerable<Person> GetPersons()
    {
        return _persons;
    }
}
