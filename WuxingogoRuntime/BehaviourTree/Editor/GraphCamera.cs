//
//  XWindowCamera.cs
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


namespace wuxingogo.Node
{

	[Serializable]
	public class GraphCamera
	{
		[SerializeField]
		Vector2 position = Vector2.zero;

		/// <summary>
		/// Position of the camera
		/// </summary>
		public Vector2 Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}

		[SerializeField]
		Vector2 scale = Vector2.one;

		/// <summary>
		/// Zoom scale of the graph camera
		/// </summary>
		public Vector2 Scale {
			get {
				return scale;
			}
			set {
				scale = value;
			}
		}

		/// <summary>
		/// Pan the camera along the specified delta value
		/// </summary>
		/// <param name="x">Delta value to move along the X value</param>
		/// <param name="y">Delta value to move along the Y value</param>
		public void Pan(int x, int y)
		{
			Pan( new Vector2( x, y ) );
		}

		/// <summary>
		/// Pan the camera along the specified delta value
		/// </summary>
		/// <param name="delta">The delta offset to move the camera to</param>
		public void Pan(Vector2 delta)
		{
			position += delta;
		}

        /// <summary>
        /// Handles the user mouse and keyboard input 
        /// </summary>
        /// <param name="e"></param>
        bool isPress = false;
		public void HandleInput(Event e)
		{
			int dragButton = 2;
			if( e.button == dragButton && e.type == EventType.layout ) {
                if( !isPress )
                {
                    isPress = true;
                }
                else
                {
                    Pan( -e.delta );
                }
                
			}
            if( e.type == EventType.MouseUp && e.button == dragButton )
            {
                isPress = false;
            }
		}

		/// <summary>
		/// Converts world coordinates (in the graph view) into Screen coordinates (relative to the editor window)
		/// </summary>
		/// <param name="worldCoord">The world cooridnates of the graph view</param>
		/// <returns>The screen cooridnates relative to the editor window</returns>
		public Vector2 WorldToScreen(Vector2 worldCoord)
		{
			return worldCoord - position;
		}

		/// <summary>
		/// Converts the Screen coordinates (of the editor window) into the graph's world coordinate
		/// </summary>
		/// <param name="screenCoord"></param>
		/// <returns>The world coordinates in the graph view</returns>
		public Vector2 ScreenToWorld(Vector2 screenCoord)
		{
			return screenCoord + position;
		}
	}
}

