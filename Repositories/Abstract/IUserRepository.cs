using PBL3.Models.Domain;

namespace PBL3.Repositories.Abstract
{
	public interface IUserRepository
	{
		void AddRelatedStory(AppUser user, Story story);
		void LoadRelatedStories(AppUser user);
	}
}
