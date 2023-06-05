using Microsoft.EntityFrameworkCore;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Repositories.Implementation
{
	public class SongRepository : Repository<Song>, ISongRepository
	{
		public SongRepository(AppDbContext context) : base(context) { }

		public Song? GetByIdWithRelatedEntity(int id)
		{
			Song? song = GetById(id);
			if (song == null) return null;

			// 1 instance của AppDbContext được tạo ra mỗi request nên song đã được tracking
			// Lấy EntityEntry của song và thực hiện Explicit Loading
			context.Entry(song).Collection(s => s.Emotions).Load();
			context.Entry(song).Collection(s => s.Genres).Load();
			return song;
		}
		public IQueryable<Song> GetAllWithRelatedGenreAndEmotion()
		{
			return context.Songs.Include(s => s.Genres).Include(s => s.Emotions);
		}
		public void SetRelatedEmotion(Song song, IEnumerable<int> emotionIds)
		{
			if (song.Emotions.Count() == 0)
			{
				song.Emotions.AddRange(context.Emotions.Where(e => emotionIds.Contains(e.EmotionId)));
				return;
			}
			// Các emotion ở song hiện tại
			var exitsEmotionIds = song.Emotions.Select(e => e.EmotionId);
			// Các emotino cần xóa
			var deletedEmotionIds = exitsEmotionIds.Where(id => !emotionIds.Contains(id));
			song.Emotions.RemoveAll(e => deletedEmotionIds.Contains(e.EmotionId));
			// Các emotion cần thêm
			var insertEmotionIds = emotionIds.Where(id => !exitsEmotionIds.Contains(id));
			var insertEmotion = context.Emotions.Where(e => insertEmotionIds.Contains(e.EmotionId));
			song.Emotions.AddRange(insertEmotion);

			// Vì các emotion và genre đc tracking (unchanged) (1 dbcontext cho cả 1 request)
			// nên sau đó nếu thực hiện Add(song) thì emotion và genre vẫn unchanged
			// Chỉ có entity của bảng trung gian quan hệ n-n thay đổi, có thể Added hoặc Deleted
		}
		public void SetRelatedGenre(Song song, IEnumerable<int> genreIds)
		{
			if (song.Genres.Count() == 0)
			{
				song.Genres.AddRange(context.Genres.Where(g => genreIds.Contains(g.GenreId)));
				return;
			}
			// Các genre ở song hiện tại
			var exitsGenreIds = song.Genres.Select(e => e.GenreId);
			// Các genre cần xóa
			var deletedGenreIds = exitsGenreIds.Where(id => !genreIds.Contains(id));
			song.Genres.RemoveAll(g => deletedGenreIds.Contains(g.GenreId));
			// Các genre cần thêm
			var insertGenreIds = genreIds.Where(id => !exitsGenreIds.Contains(id));
			var insertGenre = context.Genres.Where(g => insertGenreIds.Contains(g.GenreId));
			song.Genres.AddRange(insertGenre);
		}
	}
}
