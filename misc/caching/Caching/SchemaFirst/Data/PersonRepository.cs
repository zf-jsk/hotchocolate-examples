namespace Demo.Data;

public class PersonRepository
{
    public List<User> _persons = new List<User>
    {
        new User(){id= 1,name= "Amanda" },
        new User(){id= 2,name= "Jon" },
        new User(){id= 3,name= "Chloe" },
        new User(){id=4, name="Bill" }
    };

    public IEnumerable<User> GetUsers()
    {
        return _persons;
    }
}
