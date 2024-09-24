namespace HospitalWebApplication.Models
{
    public class Doctor:IEquatable<Doctor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        public int Experience { get; set; }

        public string Image { get; set; }

        public bool Equals(Doctor? other)
        {
            return  this.Id == other.Id;
        }
    }

}
