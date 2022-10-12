namespace TOKENAPI.Common
{
    public class FBPageQuery
    {
        public int PageNo
        {
            get { return _PageNo; }
            set { _PageNo = (value > 0) ? value : defPageNo; }
        }
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = (value <= defPageSize && value > 0) ? value : defPageSize; }
        }
        public byte? EXC { get; set; }
        public long? PID { get; set; }
        public string SortBy { get; set; } = "Id";
        public string SortDir { get; set; } = "asc";

        public string? SearchBy { get; set; } = "";

        public string SearchTxt { get; set; } = "";

        public int PageOff { get { return PageNo <= 0 ? 0 : (PageNo - 1) * PageSize; } }
        public int ResCount { get; set; } = 0;

        private int defPageSize = 30;
        private int _PageSize = 30;
        private int _PageNo = 1;
        private int defPageNo = 1;



    }
}
