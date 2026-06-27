namespace Catalog.Core.Specs
{
    public class CatalogSpecParams
    {
        public int PageIndex { get; set; } = 1;
        public string? TypeId { get; set; }
        public string? BrandId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        private const int MaxPageSize = 50;
        private int _pageSize = 10;
        public int PageSize { get => _pageSize; set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
    }
}
