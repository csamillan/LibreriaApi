namespace LibreriaApi.Dtos
{
    public class SaveBook
    {
        public string? Title { get; set; }

        public string? Autor { get; set; }

        public decimal Price { get; set; }

        public int EditorialId { get; set; }
    }
}
