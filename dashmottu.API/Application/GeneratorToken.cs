namespace dashmottu.API.Application
{
    public class GeneratorToken
    {
        public string GenerateToken(int id)
        {
            var rawToken = $"{id}-{DateTime.UtcNow.Ticks}-{Guid.NewGuid()}";
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(rawToken);
            return Convert.ToBase64String(tokenBytes);
        }
    }
}
