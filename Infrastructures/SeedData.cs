using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;

namespace PBL3.Infrastructures
{
    public class SeedData
    {
        private readonly AppDbContext context;
        private readonly IUserService userService;
        public SeedData(AppDbContext context, IUserService userService) 
        {
            this.context = context;
            this.userService = userService;
        }
        public async Task SeedExampleData()
        {
            // Seed member
            if ((await userService.GetUsersInRoleAsync(Role.Member)).Count() == 0)
            {
                AppUser newUser1 = new AppUser()
                {
                    Name = "Nguyễn Đình Anh Tiến",
                    PhoneNumber = "0905123456",
                    UserName = "nguyendinhanhtien@gmail.com",
                    Email = "nguyendinhanhtien@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser1, "111111");
                AppUser newUser2 = new AppUser()
                {
                    Name = "Lương Công Thịnh",
                    PhoneNumber = "0905123456",
                    UserName = "luongcongthinh@gmail.com",
                    Email = "luongcongthinh@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser2, "111111");
                AppUser newUser3 = new AppUser()
                {
                    Name = "Nguyễn Đức Nhật Long",
                    PhoneNumber = "0905123456",
                    UserName = "nguyenducnhatlong@gmail.com",
                    Email = "nguyenducnhatlong@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser3, "111111");
                AppUser newUser4 = new AppUser()
                {
                    Name = "Ngô Duy Tân",
                    PhoneNumber = "0905123456",
                    UserName = "ngoduytan@gmail.com",
                    Email = "ngoduytan@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser4, "111111");
                AppUser newUser5 = new AppUser()
                {
                    Name = "Nguyễn Nho Tuấn",
                    PhoneNumber = "0905123456",
                    UserName = "nguyennhotuan@gmail.com",
                    Email = "nguyennhotuan@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser5, "111111");
                AppUser newUser6 = new AppUser()
                {
                    Name = "Phan Văn Tài",
                    PhoneNumber = "0905123456",
                    UserName = "phanvantai@gmail.com",
                    Email = "phanvantai@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser6, "111111");
                AppUser newUser7 = new AppUser()
                {
                    Name = "Bùi Trường Linh",
                    PhoneNumber = "0905123456",
                    UserName = "buitruonglinh@gmail.com",
                    Email = "buitruonglinh@gmail.com",
                    Birthday = new DateTime(2003, 09, 16),
                    Gender = Gender.Male,
                    Point = 1000,
                    RegisterDay = DateTime.Now,
                };
                await userService.Register(newUser7, "111111");
            }
            
            // Seed Stories
            if (context.Stories.Count() == 0)
            {
                Story story1 = new Story()
                {
                    Name = "Cinderella",
                    Author = "Charles Perrault",
                    OneTimePrice = 10,
                    LifeTimePrice = 100,
                    Description = "Câu chuyện về cô bé xinh đẹp bị ngược đãi bởi mẹ kế và hai chị em cùng cha khác mẹ, nhưng sau đó được một phù thủy và một đôi giày thủy tinh giúp đỡ để gặp hoàng tử.",
                    Source = "audio_test_56a69d01-56f2-427f-a877-47f6391afe6e.mp3",
                    AvatarImage = "default_6866c37f-383a-489f-89ee-081324bb577a.jpg"
                };
                Story story2 = new Story()
                {
                    Name = "The Little Mermaid",
                    Author = "The Little Mermaid",
                    OneTimePrice = 10,
                    LifeTimePrice = 100,
                    Description = "Câu chuyện kể về một nàng tiên cá tên Ariel, nguyện cống hiến cả cuộc sống của mình để được sống trên cạn và gặp hoàng tử mà cô yêu thích.",
                    Source = "audio_test_18510383-a623-4eca-9fb4-ce1c9d276465.mp3",
                    AvatarImage = "default_3308e9db-1f2c-4dd4-95ab-7903c5591b97.jpg"
                };
                Story story3 = new Story()
                {
                    Name = "The Ugly Duckling",
                    Author = "Hans Christian Andersen",
                    OneTimePrice = 10,
                    LifeTimePrice = 100,
                    Description = "Câu chuyện về một con vịt xấu xí bị coi thường bởi các con vịt khác nhưng sau đó phát triển thành một con thiên nga xinh đẹp.",
                    Source = "audio_test_de3849c8-8b13-47ec-8f94-2b134032ae41.mp3",
                    AvatarImage = "default_c45c36c2-61d9-4fe4-a144-b7d867cd9af0.jpg"
                };
                Story story4 = new Story()
                {
                    Name = "Snow White and the Seven Dwarfs",
                    Author = "Brothers Grimm",
                    OneTimePrice = 10,
                    LifeTimePrice = 100,
                    Description = "Câu chuyện kể về Snow White, một cô gái xinh đẹp bị ám sát bởi mẹ kế ghen tị và được cứu sống bởi bảy chú lùn.",
                    Source = "audio_test_b6a621fd-b354-4fbc-ae7d-c9bef74058f2.mp3",
                    AvatarImage = "default_8b9e2d26-4ddb-47bd-a648-73f9804b7ef3.jpg"
                };
                Story story5 = new Story()
                {
                    Name = "The Princess and the Pea",
                    Author = "Hans Christian Andersen",
                    OneTimePrice = 10,
                    LifeTimePrice = 100,
                    Description = "Câu chuyện về một công chúa xinh đẹp, được kiểm tra tính cách của mình thông qua việc ngủ trên một cái hạt đậu.",
                    Source = "audio_test_f3f16750-86ce-4a7f-8fb8-4c5f744ee69a.mp3",
                    AvatarImage = "default_662d0f7f-e764-4c4d-a3cf-336c891d0fbd.jpg"
                };
                Story story6 = new Story()
                {
                    Name = "Peter Pan",
                    Author = " J.M. Barrie",
                    OneTimePrice = 10,
                    LifeTimePrice = 100,
                    Description = "Câu chuyện xoay quanh chàng trai Peter Pan, người không bao giờ lớn lên, và cuộc phiêu lưu của anh ta cùng với Wendy và ba đứa trẻ khác ở Neverland.",
                    Source = "audio_test_6200fc4d-36d2-46ae-9730-4f45cc29ea8f.mp3",
                    AvatarImage = "default_4c47ac50-6ddc-45bd-83ce-cd238df0cdfb.jpg"
                };

                context.Stories.AddRange(story1, story2, story3, story4, story5, story6);
                context.SaveChanges();
            }

            // Seed emotions, genres and songs
            if (context.Emotions.Count() == 0 && context.Genres.Count() == 0 && context.Songs.Count() == 0)
            {
                var listEmotions = new List<Emotion>()
                {
                    new Emotion() { Name = "Angry" }, // tuc gian
                    new Emotion() { Name = "Anxious" }, // lo au
                    new Emotion() { Name = "Dreamy" }, // mo mong
                    new Emotion() { Name = "Energetic" }, // manh liet
                    new Emotion() { Name = "Happy" }, // hanh phuc
                    new Emotion() { Name = "Lonely" }, // co don
                    new Emotion() { Name = "Mournful" }, // buon rau
                    new Emotion() { Name = "Nostalgic" }, // hoai niem
                    new Emotion() { Name = "Optimistic" }, // lac quan
                    new Emotion() { Name = "Sorrowful" }, // buon rau
                };
                context.Emotions.AddRange(listEmotions);

                var listGenres = new List<Genre>()
                {
                    new Genre() { Name = "Pop" },
                    new Genre() { Name = "Alternative Rock" },
                    new Genre() { Name = "Ambient" },
                    new Genre() { Name = "Blues" },
                    new Genre() { Name = "Classical" },
                    new Genre() { Name = "Country" },
                    new Genre() { Name = "Dance" },
                    new Genre() { Name = "Electronic" },
                    new Genre() { Name = "Folk song" }
                };
                context.Genres.AddRange(listGenres);

                Song song1 = new Song()
                {
                    Name = "Buông đôi tay nhau ra",
                    Artist = "Sơn Tùng MTP",
                    Source = "Buong-Doi-Tay-Nhau-Ra-Son-Tung-M-TP_226759c2-ae8c-42d7-9541-8d165310fd9f.mp3",
                };
                song1.Emotions.AddRange(new Emotion[] { listEmotions[3], listEmotions[5] });
                song1.Genres.Add(listGenres[0]);

                Song song2 = new Song()
                {
                    Name = "Bật Tình Yêu Lên",
                    Artist = "Tăng Duy Tân - Hòa Minzy",
                    Source = "Bat-Tinh-Yeu-Len-Tang-Duy-Tan-Hoa-Minzy_454a8489-3758-4ae6-864e-462ca6e1bbdf.mp3",
                };
                song2.Emotions.AddRange(new Emotion[] { listEmotions[2], listEmotions[4] });
                song2.Genres.Add(listGenres[0]);

                Song song3 = new Song()
                {
                    Name = "Đường tôi chở em về",
                    Artist = "Bùi Trường Linh",
                    Source = "Đường tôi chở em về_43e1be84-5b3f-4d42-a08a-f96f30876558.mp3",
                };
                song3.Emotions.AddRange(new Emotion[] { listEmotions[2], listEmotions[7] });
                song3.Genres.Add(listGenres[0]);

                context.Songs.AddRange(song1, song2, song3);
                context.SaveChanges();
            }
            
            // Seed OrderTypes
            if (context.OrderTypes.Count() == 0)
            {
                OrderType ot1 = new OrderType()
                {
                    Name = OrderType.Type.Onetime
                };
                OrderType ot2 = new OrderType()
                {
                    Name = OrderType.Type.Lifetime
                };
                context.OrderTypes.AddRange(ot1, ot2);
                context.SaveChanges();
            }
        }
    }
}
