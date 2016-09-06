//
// XFileUtils.cs
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
using System.Collections.Generic;
using System.IO;

namespace wuxingogo.tools
{
	public class XFileUtils
	{
		public XFileUtils()
		{
		}

		public static List<WWW> LoadStreamingFolderAssets(string path, string title = "Default Folder")
		{
			List<WWW> result = new List<WWW>();
			path = Path.Combine(Application.streamingAssetsPath, path);
			string[] fileEntries = Directory.GetFiles(path);

			foreach (string fileName in fileEntries)
			{

				WWW www = new WWW(fileName);
				if (www != null)
					result.Add(www);
			}
			return result;
		}

		public static List<T> LoadResourceFolderAssets<T>(string path) where T : Object
		{

			List<T> result = new List<T>();
			path = CombinePath("Assets", "Resouces", path);
			string[] fileEntries = Directory.GetFiles(path);


			foreach (string fileName in fileEntries)
			{
				T t = Resources.Load<T>(fileName);

				if (t != null)
					result.Add(t);
			}
			return result;
		}

		public static string CombinePath(string original, params string[] path)
		{
			for (int i = 0; i < path.Length; i++)
			{
				original = Path.Combine(original, path[i]);
			}
			return original;
		}

		public static List<string> GetSubFolder(List<string> totalFolders, string rootFolder)
		{
			DirectoryInfo directory = new DirectoryInfo(rootFolder);
			DirectoryInfo[] directories = directory.GetDirectories();

			foreach (DirectoryInfo folder in directories)
			{
				totalFolders.Add(folder.Name);
				GetSubFolder(totalFolders, folder.FullName);
			}
			return totalFolders;
		}
	}
}

