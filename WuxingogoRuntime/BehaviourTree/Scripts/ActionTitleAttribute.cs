
namespace wuxingogo.btFsm
{
    public class ActionTitleAttribute : System.Attribute
    {
        public string title = "";
        public ActionTitleAttribute( string title )
        {
            this.title = title;
        }
    }
}
