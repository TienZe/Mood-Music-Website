using PBL3.Models.Domain;

namespace PBL3.Infrastructures
{
    public static class SeedData
    {
        public static void SeedExampleData(AppDbContext context)
        {
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
                    Source = "cf626382-f2d0-4d4a-b51b-da790c3ca1de_Buong-Doi-Tay-Nhau-Ra-Son-Tung-M-TP.mp3",
                };
                song1.Emotions.AddRange(new Emotion[] { listEmotions[3], listEmotions[5] });
                song1.Genres.Add(listGenres[0]);

                Song song2 = new Song()
                {
                    Name = "Bật Tình Yêu Lên",
                    Artist = "Tăng Duy Tân - Hòa Minzy",
                    Source = "05118e2b-8336-4a2e-bf8f-749c75534d35_Bat-Tinh-Yeu-Len-Tang-Duy-Tan-Hoa-Minzy.mp3",
                };
                song2.Emotions.AddRange(new Emotion[] { listEmotions[2], listEmotions[4] });
                song2.Genres.Add(listGenres[0]);

                Song song3 = new Song()
                {
                    Name = "Đường tôi chở em về",
                    Artist = "Bùi Trường Linh",
                    Source = "075961c8-cbd1-4171-85d0-0fab2d38ba7e_Đường tôi chở em về.mp3",
                };
                song3.Emotions.AddRange(new Emotion[] { listEmotions[2], listEmotions[7] });
                song3.Genres.Add(listGenres[0]);

                context.Songs.AddRange(song1, song2, song3);
                context.SaveChanges();
            }
        }
    }
}
