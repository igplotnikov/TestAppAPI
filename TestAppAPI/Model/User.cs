namespace TestAppAPI
{
    public class User
    {
        public User(int id)
        {
            Id = id;
        }
        public int Id { get; }
        //internal logic omitted

        public override bool Equals(object user)
        {
            return this.Id == ((User)user).Id;
        }
    }
}