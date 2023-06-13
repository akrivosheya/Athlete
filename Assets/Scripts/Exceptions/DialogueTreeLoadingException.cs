namespace Exceptions
{
    public class DialogueTreeLoadingException : System.Exception
    {
        public DialogueTreeLoadingException(string fileName, string mes) :
        base("Can't load dialogue tree from " + fileName + ": " + mes)
        {
        }
    }
}
