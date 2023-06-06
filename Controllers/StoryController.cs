using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PBL3.Infrastructures;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class StoryController : Controller
    {
        private readonly IRepository<Story> repository;
        private readonly FileService fileService;
        private const int PageSize = 4;
        public StoryController(IRepository<Story> repoService, FileService fileService)
        {
            this.repository = repoService;
            this.fileService = fileService;
        }
        public IActionResult Index(int? pageIndex, string searchString)
        {
            // Server validation
            pageIndex = (pageIndex == null || pageIndex < 1) ? 1 : pageIndex;

            var listStories = repository.GetAll();

            // Tiếp tục mở rộng truy vấn
            if (!String.IsNullOrEmpty(searchString))
            {
                // Thực hiện Search
                listStories = listStories.Where(s => s.Name.Contains(searchString));
            }
            // Truyền filter hiện tại sang cho View
            ViewBag.SearchString = searchString ?? string.Empty;

            // Sắp xếp
            listStories = listStories.OrderBy(s => s.StoryId);

            // Phân trang kết quả
            return View(PaginatedList<Story>.CreateAsync(listStories, pageIndex.Value, PageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateStoryModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate hợp lệ, thực hiện thêm mới Story
                Story newStory = new Story()
                {
                    Name = model.Name!,
                    Author = model.Author!,
                    OneTimePrice = model.OneTimePrice,
                    LifeTimePrice = model.LifeTimePrice,
                    Description = model.Description!,
                };
                // Upload avatar
                try
                {
                    newStory.AvatarImage = fileService.UploadImage(model.AvatarImage!, "img/story");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(nameof(model.AvatarImage), e.Message);
                    return View(model);
                }
                // Upload audio
                try
                {
                    newStory.Source = fileService.UploadAudio(model.Audio!, "audio/story");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(nameof(model.Audio), e.Message);
                    return View(model);
                }

                repository.Add(newStory);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Story? story = repository.GetById(id);
            if (story == null) return NotFound();

            return View(new EditStoryModel() 
            {
                StoryId = story.StoryId,
                Name = story.Name,
                Author = story.Author,
                OneTimePrice = story.OneTimePrice,
                LifeTimePrice = story.LifeTimePrice,
                Description = story.Description,
                AudioFileName = story.Source,
                ImageFileName = story.AvatarImage
            });
        }
        [HttpPost]
        public IActionResult Edit(EditStoryModel model)
        {
            if (ModelState.IsValid)
            {
                Story? story = repository.GetById(model.StoryId);
                if (story == null) return NotFound();

                // Có thể upload lại image mới
                if (model.NewImage != null)
                {
                    string oldImage = story.AvatarImage;
                    // Upload new image
                    try
                    {
                        story.AvatarImage = fileService.UploadImage(model.NewImage, "img/story");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(nameof(model.NewImage), e.Message);
                        return View(model);
                    }
                    // Delete old audio
                    if (!String.IsNullOrEmpty(oldImage))
                    {
                        fileService.DeleteFile("img/story/" + oldImage);
                    }
                }

                // Có thể upload lại audio mới
                if (model.NewAudio != null)
                {
                    string oldAudio = story.Source;
                    // Upload new audio
                    try
                    {
                        story.Source = fileService.UploadAudio(model.NewAudio, "audio/story");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(nameof(model.NewAudio), e.Message);
                        return View(model);
                    }
                    // Delete old audio
                    if (!String.IsNullOrEmpty(oldAudio))
                    {
                        fileService.DeleteFile("audio/story/" + oldAudio);
                    }
                }
                // Cập nhật lại story
                story.Name = model.Name!;
                story.Author = model.Author!;
                story.OneTimePrice = model.OneTimePrice;
                story.LifeTimePrice = model.LifeTimePrice;
                story.Description = model.Description!;

                // Update
                repository.Update(story);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Story? story = repository.GetById(id);
            if (story != null)
            {
                // Xoá image tương ứng
                if (!String.IsNullOrEmpty(story.AvatarImage)) 
                {
                    fileService.DeleteFile("img/story/" + story.AvatarImage);
                }
                // Xóa audio tương ứng
                if (!String.IsNullOrEmpty(story.Source))
                {
                    fileService.DeleteFile("audio/story/" + story.Source);
                }
            }
            // Xóa story ra khỏi CSDL
            repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
