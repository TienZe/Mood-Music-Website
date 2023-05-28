using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Repositories.Implementation
{
    public class EmotionRepository : Repository<Emotion>, IEmotionRepository
    {
        public EmotionRepository(AppDbContext context) : base(context)
        {}

        public Emotion? GetByIdWithRelatedSongs(int id)
        {
            Emotion? emotion = context.Emotions.Find(id);
            if (emotion == null) return null;
            // Load relared song
            context.Entry(emotion).Collection(e => e.Songs).Load();
            return emotion;
        }
    }
}
