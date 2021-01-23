namespace TinyCRM.Domain.Entities
{
    public abstract class Person
    {
        public enum PersonType
        {
            Natural,
            Legal
        };

        public int Id { get; private set; }

        public PersonType Type { get; private set; }

        public string IdDocument { get; protected set; }

        public Address Address { get; set; }

        public abstract void SetIdDocument(string value);
    }
}
