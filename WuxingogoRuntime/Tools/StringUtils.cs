//
//  StringUtils.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Text.RegularExpressions;


namespace wuxingogo.tools
{

	public class StringUtils
	{
		public static int RegexCharCount(string value, string single)
		{
			MatchCollection matches = Regex.Matches(value, "[" + single + "]");
			return matches.Count;
		}

		public static string CutString(string value, int start, int end){
			return value.Substring(start, end - start);
		}

		public static string CutOnCharLeft(string value, string single)
		{
			
			if(value.Contains(single))
			{
				value = value.Substring(0, value.IndexOf(single));
			}
			return value;
		}
	}
}

