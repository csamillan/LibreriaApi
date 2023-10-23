namespace LibreriaApi.Dtos
{
    public class Book
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Autor { get; set; }

        public decimal Price { get; set; }

        public int EditorialId { get; set; }

        public virtual Editorial? Editorial { get; set; }
    }
}
