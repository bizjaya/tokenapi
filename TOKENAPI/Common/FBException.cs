namespace TOKENAPI.Common
{
    [Serializable]
    public class FBException : Exception
    {
        public string Code = "";
        public new string Message = "";

        public FBException()
        {

        }

        public FBException(string code, string message) : base(String.Concat(code, ":", message))
        {
            Code = code;
            this.Message = message;
        }

    }
}
