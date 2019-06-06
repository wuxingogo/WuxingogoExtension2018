//
// XEditorSetting.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2016 ly-user
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Globalization;
using System.Linq;
using wuxingogo.tools;
using wuxingogo.Editor;
using wuxingogo.Runtime;


public class XEditorSetting : XMonoBehaviourEditor
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
	
    public string PluginName = "Plugins/WuxingogoExtension";
	public static string PluginPath{
		get{
			return projectPath;
		}
	}

	public static string TemplatesPath{
		get{
			return XFileUtils.CombinePath(PluginPath,"Templates");
		}
	}
	public static string relativePath{
		get{
			return FileUtil.GetProjectRelativePath(PluginPath);
		}
	}
	public static string ProjectPath
    {
        get
        {
            return XFileUtils.GetAbsolutePath(Application.dataPath, "..");
        }
    }

	public static CultureInfo CultureInfo = new CultureInfo("en-US");

	[X]
	void SaveAssets()
	{
		XResources.SaveAll();
	}
	[X]
	void PersistentDataPath()
	{
		EditorUtility.RevealInFinder(Application.persistentDataPath);
	}
	[X]
	void TemporaryCachePath()
	{
		EditorUtility.RevealInFinder(Application.temporaryCachePath);
	}
	[X]
	void DataPath()
	{
		EditorUtility.RevealInFinder(Application.dataPath);
	}
	[X]
	void StreamingAssetsPath()
	{
		EditorUtility.RevealInFinder(Application.streamingAssetsPath);
	}

	[X]
	public float timeScale
	{
		get { return Time.timeScale; }
		set { Time.timeScale = value; }
	}

	[X]
	public int targetFrameRate
	{
		get { return Application.targetFrameRate; }
		set { Application.targetFrameRate = value; }
	}

	public static string projectPath
	{
		get { return EditorPrefs.GetString( "Plugin_Path", "Assets/Plugin/WuxingogoExtension/" ); }
		set
		{
			EditorPrefs.SetString( "Plugin_Path", value );
		}
	}

	[MenuItem( "Wuxingogo/Tools/Set Plugin path" )]
	static void SetPath()
	{
		var folder = EditorUtility.OpenFolderPanel( "Path", projectPath, "WuxingogoExtension" );
		XLogger.Log( folder );
		projectPath = folder;
		XResources.InitTexture();
	}
	
	[MenuItem( "Wuxingogo/Setting/Toggle Runtime Log" )]
	static void EnableLogRuntime()
	{
		XLogger.EnableLog = !XLogger.EnableLog;
		Debug.Log("Log Enable : {0}.".StringFormat(XLogger.EnableLog));
	}
	[MenuItem( "Wuxingogo/Setting/Toggle Define Log" )]
	static void EnableLogStatic()
	{

		var defineSymbols = CurrentBuildDefine;
		
		if (defineSymbols.Contains(XLogger.PREDEFINE) == false)
		{
			defineSymbols = defineSymbols +";"+ XLogger.PREDEFINE;
			Debug.Log("Log Enable : {0}.".StringFormat(true));
		}
		else
		{
			defineSymbols = defineSymbols.Replace(XLogger.PREDEFINE, "");
			Debug.Log("Log Enable : {0}.".StringFormat(false));
		}
		//XLogger.Log(defineSymbols+ "======"+ CurrentBuildDefine);
		CurrentBuildDefine = defineSymbols;
		
	}
	[X]
	public static string CurrentBuildDefine
	{
		get
		{
			var buildTarget = EditorUserBuildSettings.activeBuildTarget;
			var targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
			var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

			return defineSymbols;
		}
		set
		{
			var buildTarget = EditorUserBuildSettings.activeBuildTarget;
			var targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
			
			PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, value);
		}
		
	}
	
	[InitializeOnLoad]
	public class Autorun
	{
		static Autorun()
		{
			EditorApplication.update += RunOnce;
		}
 
		static void RunOnce()
		{
			EditorApplication.update -= RunOnce;
			//XStyles.InitBuildinStyle();
			
		}
	}
}
