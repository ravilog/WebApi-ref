
namespace Common.Library
{
    public enum Roles
    {
        Admin = 1,
        Agent = 2,
        Supplier=3,
        Analyzer=4
    }
    public class CommmonMessage
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string messageCode { get; set; }
        public object result { get; set; }
    }
}
