using PBL3.Models.Domain;

namespace PBL3.Repositories.Abstract
{
    public interface IEmotionRepository : IRepository<Emotion>
    {
        Emotion? GetByIdWithRelatedSongs(int id);
    }
}
