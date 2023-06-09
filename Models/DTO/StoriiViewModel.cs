using PBL3.Models.Domain;

namespace PBL3.Models.DTO
{
	public class StoryItem
	{
		public Story Story { get; set; }
		public bool AlreadyBought { get; set; } = false;
	}
	public class StoriiViewModel
	{
		public IEnumerable<StoryItem> StoryItems { get; set; }
		public decimal UserPoint { get; set; }
	}
	public class ListenStoryModel : StoriiViewModel
	{
		public Story Story { get; set; }
	}
}
