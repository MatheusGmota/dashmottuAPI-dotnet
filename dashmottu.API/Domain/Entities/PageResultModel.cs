namespace dashmottu.API.Domain.Entities
{
    public class PageResultModel<T>
    {
        public required T Data { get; set; }
        public int Deslocamento { get; set; }
        public int Limite { get; set; }
        public int Total { get; set; }
        
    }
}
