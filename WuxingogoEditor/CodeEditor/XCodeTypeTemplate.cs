//
//  XCodeTypeTemplate.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using UnityEngine;
using System.Collections.Generic;
using wuxingogo.Code;

public class XCodeTypeTemplate : ScriptableObject
{
    [SerializeField]
    internal List<XCodeType> templates = new List<XCodeType>();
    [SerializeField]
    internal List<string> snippets = new List<string>();

    public void AddTemplate( Type type )
    {
        templates.Add( new XCodeType( type.AssemblyQualifiedName ) );
    }

    public XCodeType GetTemplate( Type type )
    {
        for( int pos = 0; pos < templates.Count; pos++ )
        {
            //  TODO loop in templates.Count
            if( templates[pos].Target.Equals( type ) )
            {
                return templates[pos];
            }
        }
        return null;
    }

    public string GetSnippets( int index )
    {
        if( snippets.Count > index )
        {
            return snippets[index];
        }
        return "";
    }

    public static void SelectType( Action<XCodeType> callback )
    {
        XCodeTemplateWindow window = XCodeTemplateWindow.Init( instance );
        window.currentAction = callback;
    }

    static XCodeTypeTemplate instance = null;

    public static XCodeTypeTemplate GetInstance()
    {
        if( instance == null )
        {
            string assetPath = XEditorSetting.TemplatesPath + "/" + "XCodeTypeTemplate.asset";
            instance = XAssetLoader.LoadAssetFromProject<XCodeTypeTemplate>( assetPath );
        }
        return instance;
    }
}