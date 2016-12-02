
namespace wuxingogo.btFsm
{
    public class StateTitleAttribute : System.Attribute
    {
        public string title = "";
        public StateTitleAttribute( string title )
        {
            this.title = title;
        }
    }
}
