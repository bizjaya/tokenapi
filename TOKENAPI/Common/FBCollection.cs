namespace TOKENAPI.Common
{
    public class FBCollection<T>
    {

        public List<T> Data { get; set; }
        public FBPaging Paging { get; set; }
        public FBError Error { get; set; }

        public FBCollection(IEnumerable<T> collection)
        {
            Data = collection.ToList();
        }

        public FBCollection(List<T> collection)
        {
            Data = collection;
        }
        public FBCollection(IEnumerable<T> collection, int pageNo, int pageSize, long resCount)
        {
            Data = collection.ToList();
            Paging = new FBPaging { PageNo = pageNo, PageSize = pageSize, ResCount = resCount, PageCount = (int)Math.Ceiling((decimal)resCount / pageSize) };
        }
        public FBCollection(List<T> collection, int pageNo, int pageSize, long resCount)
        {
            Data = collection;
            Paging = new FBPaging { PageNo = pageNo, PageSize = pageSize, ResCount = resCount, PageCount = (int)Math.Ceiling((decimal)resCount / pageSize) };

        }


        // public ActionResult GetActionResult(Func<ODCollection<T>, bool> criteria = null);
    }
}
