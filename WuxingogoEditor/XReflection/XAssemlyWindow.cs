//XAssemlyWindow.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 12/15/2015 20:50:27 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.


using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System;
using Object = UnityEngine.Object;


public class XAssemlyWindow : XBaseWindow
{

	bool isInit = true;

	Assembly[] assemblies = null;

	Type[] types = null;

	Type currentType = null;

	[MenuItem( "Wuxingogo/Reflection/Wuxingogo XAssemlyWindow " )]
	static void Init()
	{
		InitWindow<XAssemlyWindow>();
	}

	public override void OnXGUI()
	{
		base.OnXGUI();

		DoButton( "Clean", () => isInit = true );

		if( isInit ) {
			InitAllAssemlies();
			isInit = false;
		} else if( null != assemblies ) {
			for( int pos = 0; pos < assemblies.Length; pos++ ) {
				//  TODO loop in Length
				if( CreateSpaceButton( assemblies[pos].FullName ) ) {
					SelectionAssembly( assemblies[pos] );
					return;
				}
			}
		} else if( null != types ) {
			for( int pos = 0; pos < types.Length; pos++ ) {
				//  TODO loop in Length
				if( CreateSpaceButton( types[pos].FullName ) ) {
					SelectionType( types[pos] );
					return;
				}
			}
		} else if( null != currentType ) {
			DoButton( "FindObjectByType", ()=> Selection.objects = Object.FindObjectsOfType( currentType ) );
		}
	}

	void InitAllAssemlies()
	{
		assemblies = AppDomain.CurrentDomain.GetAssemblies();
	}

	void SelectionAssembly(Assembly assembly)
	{
		types = assembly.GetTypes();
		assemblies = null;
	}

	void SelectionType(Type type)
	{
		currentType = type;
		types = null;
	}

	void FindObjectByType()
	{
		Selection.objects = Object.FindObjectsOfType( currentType );
	}

	void FindGameObjectByType()
	{
//		Selection.gameObjects = Object.FindObjectsOfType( currentType ) ;

	}


}
