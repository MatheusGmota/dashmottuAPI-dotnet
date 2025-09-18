namespace dashmottu.API.Domain.Model
{
    public class PaginacaoModel<T>
    {
        public required T Data { get; set; }
        public int Deslocamento { get; set; }
        public int Limite { get; set; }
        public int Total { get; set; }
        
    }
}
