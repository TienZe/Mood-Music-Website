using PBL3.Models.Domain;

namespace PBL3.Models.DTO
{
	public class StoryItem
	{
		public Story Story { get; set; }
		public bool AlreadyBought { get; set; } = false;
	}
	
	public class ListenStoryModel
	{
		public Story Story { get; set; }
		public IEnumerable<StoryItem> StoryItems { get; set; }
	}
}
