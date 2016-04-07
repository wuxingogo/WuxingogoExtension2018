//
//  Style.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.




using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using System;
using wuxingogo.Fsm;


namespace FsmEditor
{


	public class Styles
	{
		private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

		public static GUIStyle BackgroudStyle {
			get {
				if( sprites.Count == 0 ) {
					Load();
				}
				string key = "background";
				if( EditorGUIUtility.isProSkin )
					key += "_p";
				return sprites[key].style;
			}
		}

		private static void Load()
		{
			sprites = new Dictionary<string, Sprite>();
			var textures = StateMachineMakerResources.LoadAssetBundleResources( "Images", typeof( Texture2D ) );
        
			foreach( Texture2D texture in textures ) {
				texture.hideFlags = HideFlags.DontSave;
				sprites.Add( texture.name, new Sprite( texture ) );
			}
		}

   

		public static GUIStyle GetStateStyle(StateColor stateColor, bool on)
		{
			if( sprites.Count == 0 ) {
				Load();
			}
       
			string key = stateColor.ToString().ToLower();

			if( on )
				key += " on";
			if( EditorGUIUtility.isProSkin )
				key += "_p";
			return sprites[key].style;
		}

		private class Sprite
		{
			public GUIStyle style;

			public Sprite(Texture2D texture)
			{
				style = new GUIStyle();
				style.border = new RectOffset( 11, 11, 11, 15 );
				style.padding = new RectOffset( 0, 0, 29, 0 );
				style.overflow = new RectOffset( 7, 7, 6, 9 );
				style.alignment = TextAnchor.UpperCenter;
				style.normal.background = texture;
				style.contentOffset = new Vector2( 0, -22 );
			}
		}
	}

}



