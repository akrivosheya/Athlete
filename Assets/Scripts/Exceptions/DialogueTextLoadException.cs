using System;

namespace Exceptions
{
    public class DialogueTextLoadException : Exception
    {
        public DialogueTextLoadException(string fileName, Exception exception) :
        base("Can't load text from " + fileName + ".\n" + exception)
        {
        }
    }
}
