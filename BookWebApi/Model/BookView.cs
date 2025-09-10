namespace BookWebApi.Model
{
    public class BookView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberofPeople { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }

    }
}
