using PBL3.Models.Domain;

namespace PBL3.Repositories.Abstract
{
	public interface ISongRepository : IRepository<Song>
	{
		Song? GetByIdWithRelatedEntity(int id);
		IQueryable<Song> GetAllWithRelatedGenreAndEmotion();
		void SetRelatedEmotion(Song song, IEnumerable<int> emotionIds);
		void SetRelatedGenre(Song song, IEnumerable<int> genreIds);
	}
}
