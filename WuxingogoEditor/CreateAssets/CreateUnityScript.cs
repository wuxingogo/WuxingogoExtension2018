//  CREATEUNITYSCRIPT
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 wuxingogo
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------------
// 2015/10/14 
// ------------------------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class CreateUnityScript : XBaseEditor{
    
    
    [MenuItem("Assets/Create/Wuxingogo/No Exten Class", false, 100)]
    public static void CreateFile(){
        
        string path = EditorUtility.SaveFilePanel("Create A Object", XEditorSetting.ProjectPath, "NewEditor.cs", "cs");
        if (path == "")
            return;
        
        path = FileUtil.GetProjectRelativePath(path); 
        
        FileInfo file = new FileInfo(path);           
        StreamWriter sw = file.AppendText(); 
		
        string fileName = file.Name;
        string className = file.Name.Substring(0, file.Name.Length - 3);  
         
        string codeHeader = WriteHeader(file.Name);
        string codeUs = WriteUseNameSpace("UnityEngine", "System.Collections");
        string codeClass = WriteExtendClass(className);
        
        sw.Write(codeHeader + codeUs + codeClass);           
        sw.Dispose();  
        
        AssetDatabase.SaveAssets(); 
        AssetDatabase.Refresh(); 
    }
    //  Author:
    //       ${wuxingogo} <52111314ly@gmail.com>
    // ------------------------------------------------------------------------------
    // 2015/10/14  Generate using namespace
    // ------------------------------------------------------------------------------
    public static string WriteUseNameSpace(params string[] namespaces){
        string temp = "\n";
        for(int pos = 0; pos < namespaces.Length; pos++){
            temp += "using " + namespaces[pos] + ";\n";
        }
        return temp;
        
    }
    public static string WriteExtendClass(string className, params string[] inherit){
        if(inherit.Length > 0)
            return string.Format("\npublic class {0} : {1}{{\n\n}}", className, inherit);
        else
            return string.Format("\npublic class {0}{{\n\n}}", className);
    }
    
    public static string WriteHeader(string fileName){
        return string.Format(XEditorSetting.codeFileHeader, fileName, 
                            XEditorSetting.author,
                            XEditorSetting.mail, System.DateTime.Now);
        
    }
}

