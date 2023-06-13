namespace Dialogue
{
    public class DialoguesFileFormatter
    {
        private string _delimeter = "_";

        public string GetKey(string pointName, string sceneName)
        {
            return pointName + _delimeter + sceneName;
        }
    }
}
