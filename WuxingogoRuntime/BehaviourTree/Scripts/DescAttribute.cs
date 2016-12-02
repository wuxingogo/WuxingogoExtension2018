
namespace wuxingogo.btFsm
{
    public class DescAttribute : System.Attribute
    {
        public string title = "";
        public DescAttribute( string title )
        {
            this.title = title;
        }
    }
}
