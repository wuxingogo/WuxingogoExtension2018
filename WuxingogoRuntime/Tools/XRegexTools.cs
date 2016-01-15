using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


namespace wuxingogo.tools
{

	public class XRegexTools
	{
		private static string numberRegex = @"\-?[0-9]+?\.[0-9]+?";
		/// <summary>
		/// Seach Decimal Number
		/// </summary>
		/// <returns>The number.</returns>
		public static string RegexNumber(string value){
			MatchCollection matches = Regex.Matches(value, numberRegex);
			return "";
		}

//		public static int 
	}
}
