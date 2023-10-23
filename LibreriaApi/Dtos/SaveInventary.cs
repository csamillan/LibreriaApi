namespace LibreriaApi.Dtos
{
    public class SaveInventary
    {
        public decimal Stock { get; set; }

        public int BookId { get; set; }

        public int BranchId { get; set; }
    }
}
