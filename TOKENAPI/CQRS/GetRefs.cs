using TOKENAPI.Common;

namespace TOKENAPI.CQRS
{
    public class GetRefs : FBPageQuery
    {
        public long? RefId { get; set; }
        public int Level { get; set; }
    }
}
