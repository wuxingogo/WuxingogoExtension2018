using UnityEngine;
using System.Collections;
using UnityEditor;

public class XEditorSetting : XBaseWindow
{
    public static string author = "Wuxingogo";
    public static string mail = "52111314ly@gmail.com";
    /// <summary>
    /// file name, author, mail and time.
    /// </summary>
    public static string codeFileHeader = "//{0}\n" + 
        "//\n" + 
        "//Author:\n" +
        "//\t\t{1} {2}\n" +
        "//\n" +
        "//\n" + 
        "//\t\tCopyright (c) {3} \n" +
        "//\n" + 
        "//\tYou should have received a copy of the GNU Lesser General Public License" +
        "along with this program.\n" + 
        "//\tIf not, see <http://www.gnu.org/licenses/>.\n";
        
    public static string ProjectPath = Application.dataPath + "/WuxingogoExtension";
    [MenuItem( "Wuxingogo/Wuxingogo XEditorSetting" )]
    static void init()
    {
        XEditorSetting window = (XEditorSetting)EditorWindow.GetWindow(typeof(XEditorSetting));
    }

    public override void OnXGUI()
    {
        if (CreateSpaceButton("Save Asset"))
        {
            XResources.GetInstance().SaveAll();
        }
    }
}
