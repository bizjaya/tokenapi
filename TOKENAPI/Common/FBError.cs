namespace TOKENAPI.Common
{
    public class FBError
    {
        public FBError()
        {

        }

        public string Type { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string InnerMsg { get; set; }
        public string Lang { get; set; }
        public List<string> Errors { get; set; }
    }
}
