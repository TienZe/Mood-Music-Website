using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Repositories.Implementation
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext context;
		public UserRepository(AppDbContext context)
		{	
			this.context = context;
		}
		
		public void SetRelatedStory(AppUser user, IEnumerable<int> storyIds)
		{
            // Cần load các related stories lên
            LoadRelatedStories(user);

            if (user.Stories.Count() == 0)
			{
				user.Stories.AddRange(context.Stories.Where(story => storyIds.Contains(story.StoryId)));
				return;
			}
			// Các stories thuộc user hiện tại
			var exitsStoryIds = user.Stories.Select(story => story.StoryId);
			// Các stories cần xóa
			var deletedStoryIds = exitsStoryIds.Where(id => !storyIds.Contains(id));
			user.Stories.RemoveAll(story => deletedStoryIds.Contains(story.StoryId));
			// Các stories cần thêm
			var insertStoryIds = storyIds.Where(id => !exitsStoryIds.Contains(id));
			var insertStory = context.Stories.Where(story => insertStoryIds.Contains(story.StoryId));
			user.Stories.AddRange(insertStory);
			context.SaveChanges();

			// Thông thường sẽ dấn đến thay đổi state của entity thuộc bảng trung gian
			// , có thể đc thêm added hoặc bị xóa đi deleted
		}
		public void AddRelatedStory(AppUser user, Story story)
		{
			// Cần load các related stories lên
			LoadRelatedStories(user);
			// Entity story sử dụng generate key :
			// + Nếu ko khóa -> Added
			// + Nếu có khóa -> Unchanged
			// + Đã tồn tại trong user ? -> cần check
			if (user.Stories.FirstOrDefault(s => s.StoryId == story.StoryId) == null)
			{
				user.Stories.Add(story);
				context.SaveChanges();
			}
		}
		public void LoadRelatedStories(AppUser user)
		{
			context.Entry(user).Collection(u => u.Stories).Load();
		}
		public virtual void LogChangeTracker()
		{
			context.ChangeTracker.DetectChanges();
			Console.WriteLine("Tracked entities at current request : ");
			Console.WriteLine(context.ChangeTracker.DebugView.LongView);
		}
	}
}
