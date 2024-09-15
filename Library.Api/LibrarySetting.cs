namespace Library.Api
{
    public class LibrarySetting
    {
        public const string SectionName = "LibrarySetting";
        public int BorrowMaxDay { get; set; }
        public int ReserveMaxHour { get; set; }
    }
}
