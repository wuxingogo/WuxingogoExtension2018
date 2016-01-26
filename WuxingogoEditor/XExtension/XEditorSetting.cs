using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

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
	public static string TemplatesPath = Application.dataPath + "/WuxingogoExtension/Templates";
	public static string relativePath = FileUtil.GetProjectRelativePath(ProjectPath);
	private bool isShowIcons = false;
	
    [MenuItem( "Wuxingogo/Wuxingogo XEditorSetting" )]
    static void init()
    {
		InitWindow<XEditorSetting>();
    }

    public override void OnXGUI()
    {
        if (CreateSpaceButton("Save Asset"))
        {
            XResources.SaveAll();
        }
        
		DoButton("ShowAllIcon", ()=> isShowIcons = !isShowIcons);
		
		if(isShowIcons) ShowAllIcon();
    }
    
    public void ShowAllIcon(){
		//鼠标放在按钮上的样式
		foreach (MouseCursor item in Enum.GetValues(typeof(MouseCursor)))
		{
//			GUILayout.Button(Enum.GetName(typeof(MouseCursor), item));
			DoButton(Enum.GetName(typeof(MouseCursor),item), ()=> Debug.Log(item.ToString()));
			EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), item);
			GUILayout.Space(10);
		}
		
		
		//内置图标
//		for(int i =0; i< XResources.GetInstance().IconNames.Length; i+=8)
//		{
//			GUILayout.BeginHorizontal();
//			for (int j =0; j < 8; j++)
//			{
//				int index = i + j;
//				if(index < XResources.GetInstance().IconNames.Length){
//					string btnName = XResources.GetInstance().IconNames[index];
//					GUIContent content = EditorGUIUtility.IconContent(btnName);
//					AddButton(content, ()=> Debug.Log(btnName.ToString()), GUILayout.Width(50), GUILayout.Height(30));
//				}
//					
//			}
//			GUILayout.EndHorizontal();
//		}
    }
}
