namespace FufelMarketBackend.Models
{
    public class City
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string TimeZone { get; set; }

        public required int AdvertisementId { get; set; }

        public required Advertisement Advertisement { get; set; }
    }
}
