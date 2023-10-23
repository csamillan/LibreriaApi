namespace LibreriaApi.Dtos
{
    public class Inventary
    {
        public int? Id { get; set; }

        public decimal Stock { get; set; }

        public int BookId { get; set; }

        public int BranchId { get; set; }

        public virtual Book? Book { get; set; }

        public virtual Branch? Branch { get; set; }
    }
}
