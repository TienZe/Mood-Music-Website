using Microsoft.AspNetCore.Mvc.Rendering;

namespace PBL3.Models.DTO
{
    public class MakingOrderPopupModel
    {
        public string ItemClassName { get; set; }
        public IEnumerable<SelectListItem> SelectListOrderType { get; set; }
    }
}
