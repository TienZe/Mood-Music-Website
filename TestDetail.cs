using PBL3.Models.Domain;

namespace PBL3.Models.DTO
{
    public class TestDetail
    {
        private List<AppUser> A;
        private static TestDetail _instance;

        public static TestDetail Instance { get { if (_instance == null) _instance = new TestDetail(); return _instance; } private set => _instance = value; }
        
        public TestDetail() { 
            A = new List<AppUser>();
            for (int i = 1; i <= 100; i++)
            {
                A.Add(new AppUser
                {
                    Id = i.ToString(),
                    Name = "Đặng Hoài Phương",
                    UserName = "Phuong1234",
                    Birthday = new DateTime(2000, 12, 31),
                    Email = "phuongitf@dut.udn.vn",
                    Gender = Gender.Male,
                    PhoneNumber = "999988880"
                });
            }
        }

        public List<AppUser> GetAll()
        {
            return A;
        }
    }
}
