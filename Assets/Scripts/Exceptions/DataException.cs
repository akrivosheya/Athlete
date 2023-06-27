namespace Exceptions
{
    public class DataException : System.Exception
    {
        public DataException(string filePath) : base("Can't load data from " + filePath)
        {
        }
        
        public DataException(int index, int count) : base("Wrong index: index has to be more than 0 and less than " + (count - 1) + ", but it is " + index)
        {
        }
    }
}
