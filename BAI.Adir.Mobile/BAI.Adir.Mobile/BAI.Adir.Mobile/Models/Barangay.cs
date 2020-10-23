namespace BAI.Adir.Mobile.Models
{
    public class Barangay
    {
        public int BarangayId { get; set; }

       
        public string Name { get; set; }

       
        public int MunicipalityId { get; set; }
        public virtual Municipality Municipality { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}