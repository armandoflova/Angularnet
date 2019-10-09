namespace webapi.Models
{
    public class Like
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; } 
        public Usuario Liker { get; set; }
        public Usuario Likee { get; set; }
    }
}