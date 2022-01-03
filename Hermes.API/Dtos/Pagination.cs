using Core.entities;

namespace Hermes.API.Dtos
{
    public class Pagination<T> where T : class
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<T>? Data { get; set; }

        public Pagination(int count, int pageSize, int pageIndex, IEnumerable<T> data)
        {
            Count = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;    
        }
    }
}
