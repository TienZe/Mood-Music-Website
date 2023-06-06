using Microsoft.EntityFrameworkCore;

namespace PBL3.Models.DTO
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int FirstItemIndex { get; private set; }
       
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            FirstItemIndex = (PageIndex - 1) * PageSize + 1;
            this.AddRange(items);
        }

        // Tạo PaginatedList - danh sách các đối tượng đã được phân trang
        // Truyền source IQueryable để có thể thực hiện query tối ưu ngay trên CSDL
        public static PaginatedList<T> CreateAsync(IEnumerable<T> source
            , int pageIndex, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
