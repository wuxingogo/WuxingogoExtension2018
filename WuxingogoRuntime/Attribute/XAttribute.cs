//  XATTRIBUTE
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 wuxingogo
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------------
// 2015/3/16 
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;

namespace wuxingogo.Runtime
{
	[AttributeUsage( AttributeTargets.All, Inherited = true, AllowMultiple = false )]
	public class XAttribute : PropertyAttribute
    {
        public string title = "";
        public XAttribute(){}

        public XAttribute(string title) : this()
        {
            this.title = title;
        }
    }
}
