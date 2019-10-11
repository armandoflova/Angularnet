namespace webapi.Models
{
    public class Like
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; } 
        public virtual Usuario Liker { get; set; }
        public virtual Usuario Likee { get; set; }
    }
}