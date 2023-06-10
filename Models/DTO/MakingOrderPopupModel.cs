using Microsoft.AspNetCore.Mvc.Rendering;

namespace PBL3.Models.DTO
{
    public class MakingOrderPopupModel
    {
        public string ItemClassName { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> SelectListOrderType { get; set; }
        public decimal UserPoint { get; set; }
    }
}
