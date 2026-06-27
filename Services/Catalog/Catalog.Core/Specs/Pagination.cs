namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public int _pageSize { get; set; }
        public int _pageIndex { get; set; }
        public int _count { get; set; }
        public IReadOnlyList<T> _data { get; set; }

#pragma warning disable CS8618
        public Pagination() { }
        public Pagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data)
        {
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _count = count;
            _data = data;
        }
    }
}
