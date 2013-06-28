namespace DapperDemo
{
    public class Dog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string OwnerName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, Age: {2}, OwnerName: {3}", Id, Name, Age, OwnerName);
        }
    }
}