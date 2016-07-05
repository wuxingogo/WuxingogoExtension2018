using System.IO;

namespace wuxingogo.tools
{
    public class FileExtension
    {
        public static void Write( byte[] bytes, string path )
        {
            File.WriteAllBytes( path, bytes );
        }

        public static void Write( string contents, string path )
        {
            File.WriteAllText( path, contents );
        }
    }
}
