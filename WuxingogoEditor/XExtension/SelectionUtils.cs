//
// SelectionUtils.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2018 ly-user
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using wuxingogo.tools;
using Object = UnityEngine.Object;

namespace wuxingogo.Editor
{
    public class SelectionUtils : XBaseEditor
    {

        public static List<T> GetObjects<T>() where T : Object
        {
            List<T> array = new List<T>();
            var objects = Selection.objects;
            for( int i = 0; i < objects.Length; i++ )
            {
                var obj = objects[ i ];
                if( obj is T )
                {
                    array.Add( obj as T);
                }
            }
            return array;
        }

        public static List<T> GetObjectsComponent<T>() where T : Component
        {
            List<T> array = new List<T>();
            var gameObjects = Selection.gameObjects;
            for( int i = 0; i < gameObjects.Length; i++ )
            {
                var obj = gameObjects[ i ];
                var components = obj.GetComponentsInChildren<T>();
                for( int j = 0; j < components.Length; j++ )
                {
                    var c = components[ j ];
                    if( array.Contains( c ) == false )
                    {
                        array.Add( c );
                    }
                }
            }
            return array;
        }

        public static T GetObject<T>() where T : Object
        {
            return Selection.activeObject as T;
        }

        [MenuItem( "Wuxingogo/Tools/Selection/Info" )]
        public static void GetInfo()
        {
            var o = GetObject<Object>();
            XLogger.Log( "name:" + o.name );  
            XLogger.Log( "Type:" + o.GetType() );
            XLogger.Log( "InstanceID:" + o.GetInstanceID() ); 
        }

        [MenuItem("Wuxingogo/Tools/Selection/Copy Asset Path")]
        public static void CopyAssetPath()
        {
            var objs = GetObjects<Object>();
            string content = "";
            for (int i = 0; i < objs.Count; i++)
            {
                var obj = objs[i];
                var path = AssetDatabase.GetAssetPath(obj);

                content += path + "\n";
            }
            content.CopyToClipboard();
        }
        
        [MenuItem("Wuxingogo/Tools/Selection/Copy GameObject Path")]
        public static void CopyGameObjectPath()
        {
            var objs = Selection.gameObjects;
            
            string content = "";
            for (int i = 0; i < objs.Length; i++)
            {
                var obj = objs[i];
                var path = GameObjectUtilities.GetFullPathName(obj);
                content += path + "\n";
            }
            content.CopyToClipboard();
        }
    }
}