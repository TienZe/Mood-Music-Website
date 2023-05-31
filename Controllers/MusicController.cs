﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
	public class MusicController : Controller
	{
		private readonly ISongRepository songRepository;
		private readonly IEmotionRepository emotionRepository;
		public MusicController(ISongRepository songService, IEmotionRepository emotionService) 
		{
			this.songRepository = songService;
			this.emotionRepository = emotionService;
		}
		public IActionResult Index(int? selectEmotion)
		{
			var listSongs = new List<Song>();
            if (selectEmotion != null)
			{
                // Cần lọc bài hát theo emotion được chọn
                Emotion? emotion = emotionRepository.GetByIdWithRelatedSongs(selectEmotion.Value);
                if (emotion == null) return NotFound();
				listSongs = emotion.Songs;
            }
			// Truyền select list emotion sang View
			var selectListEmotion = emotionRepository.GetAll().Select(e => new SelectListItem()
			{
				Text = e.Name,
				Value = e.EmotionId.ToString(),
				Selected = e.EmotionId == selectEmotion
				
			});
			ViewBag.SelectListEmotion = selectListEmotion;
			
			return View("EmotionSong", listSongs);
		}
	}
}