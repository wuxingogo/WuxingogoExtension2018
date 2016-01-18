//XCodeObject.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 10/15/2015 1:38:10 PM 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using UnityEditor;


namespace wuxingogo.Code
{

	public class XCodeObject : ScriptableObject
	{
		public XCodeClass classUnit = new XCodeClass();

		public string className {
			get{
				return classUnit.name;
			}
			set{
				classUnit.name = value;
			}
		}

		public void Compile(string outPutPath)
		{
			classUnit.Compile(outPutPath);
		}

		public void Draw(XBaseWindow window){
			classUnit.DrawSelf(window);
		}
    
	}
}