namespace Core.Domain.DTOs
{
    public class FavoriteDTO
    {
        public FavoriteDTO(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
