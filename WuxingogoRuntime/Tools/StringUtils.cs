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
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace wuxingogo.tools
{

	public static class StringUtils
	{
		private static string betweenCondition(string left, string right)
		{
			return string.Format( @"\{0}.*?\{1}", left, right );
		}

		public static int RegexCharCount(string value, string single)
		{
			MatchCollection matches = Regex.Matches(value, "[" + single + "]");
			return matches.Count;
		}
		public static int RegexCharsCount(this string value, string left, string right)
		{
			
			MatchCollection matches = Regex.Matches(value, betweenCondition(left, right));
			return matches.Count;
		}
		public static string[] RegexCutString(this string value, string left, string right)
		{
			string condition = betweenCondition( left, right );

			MatchCollection matches = Regex.Matches( value, condition );
			List<string> result = new List<string>();
			for( int i = 0; i < matches.Count; i++ ) {
				//  TODO loop in matches.Count
				result.Add( "" );
				if( matches[i].Length > 0 ) {
					
					result[i] = value.SubstringWithLength( matches[i].Index + 1, matches[i].Index + matches[i].Length - 1 );
				}
			}
			return result.ToArray();
		}

		public static string RegexCutStringReverse(this string value, string left, string right)
		{
			string condition = betweenCondition( left, right );

			MatchCollection matches = Regex.Matches( value, condition );
			int pos = 0;
			string result = "";
			for( int i = 0; i < matches.Count; i++ ) {
				//  TODO loop in matches.Count
				result += value.Substring(pos, matches[i].Index -pos);
				pos = matches[i].Index + matches[i].Length;
			}
			if( pos < value.Length )
				result += value.SubstringWithLength( pos, value.Length );
			return result;
		}

		public static string SubstringWithLength(this string value, int start, int end){
			return value.Substring(start, end - start);
		}

		public static string Substring(string value, string single)
		{
			
			if(value.Contains(single))
			{
				value = value.Substring(0, value.IndexOf(single));
			}
			return value;
		}

		public static string SubstringFromEnd(string value, string str)
		{
			var startIndex = value.LastIndexOf( "/" );
			value = value.Substring(startIndex, value.Length - startIndex);
			return value;
		}

		public static string ToUTF8( this string src )
		{
			byte[] bytes = Encoding.Default.GetBytes(src);
			src = Encoding.UTF8.GetString(bytes);
			return src;
		}
	}
}

