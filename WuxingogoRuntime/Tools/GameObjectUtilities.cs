//
// GameObjectUtilities.cs
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

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace wuxingogo.tools
{
    public static class GameObjectUtilities
    {
        public static string DefaultNewGameObjectName = "BoxCollider";
        private static string FullPath(GameObject go)
        {
            return go.transform.parent == null
                    ? go.name
                    : FullPath(go.transform.parent.gameObject) + "/" + go.name;
        }

        public static BoxCollider NewBoxCollider(Vector3 size, bool isTrigger)
        {
            GameObject newGo = new GameObject(DefaultNewGameObjectName);
            var boxCollider = newGo.AddComponent<BoxCollider>();
            boxCollider.size = size;
            boxCollider.isTrigger = isTrigger;
            return boxCollider;
        }

        public static BoxCollider AddBoxCollider(GameObject newGo, Vector3 size, bool isTrigger)
        {
            var boxCollider = newGo.AddComponent<BoxCollider>();
            boxCollider.size = size;
            boxCollider.isTrigger = isTrigger;
            return boxCollider;
        }

        public static Transform FindByName(Transform root, string name)
        {
            for (int i = 0; i < root.childCount; ++i)
            {
                Transform t = root.GetChild(i);
                if (t.gameObject.name == name)
                    return t;
            }
            for (int i = 0; i < root.childCount; ++i)
            {
                Transform t = root.GetChild(i);
                Transform result = FindByName(t, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static void FindByNameAll(Transform root, string name, ref List<Transform> list)
        {
            for (int i = 0; i < root.childCount; ++i)
            {
                Transform t = root.GetChild(i);
                if (t.gameObject.name == name)
                    list.Add(t);
            }
            for (int i = 0; i < root.childCount; ++i)
            {
                Transform t = root.GetChild(i);
                FindByNameAll(t, name, ref list);
            }
        }

        public static GameObject CreatePrefab(Transform parent, GameObject prefab, bool layerAndTag = true)
        {
            if (prefab == null)
                return null;
            GameObject go = (GameObject)GameObject.Instantiate(prefab);
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            if (parent != null && layerAndTag)
            {
                go.layer = parent.gameObject.layer;
                go.tag = parent.tag;
            }
            return go;
        }
        public static GameObject CreatePrefab(Vector3 pos, Vector3 eulrAngles, string prefabName)
        {
            GameObject result = CreatePrefab(null, prefabName);
            if (result == null)
                return result;
            result.transform.position = pos;
            result.transform.eulerAngles = eulrAngles;
            result.transform.localScale = Vector3.one;
            return result;
        }
        public static T CreatePrefab<T>(Transform parent, GameObject prefab) where T : Component
        {
            var go = CreatePrefab(parent, prefab);

            return go.GetComponent<T>();
        }

        public static T CreatePrefab<T>(Transform parent, string prefabName) where T : Component
        {
            var go = CreatePrefab(parent, prefabName);

            return go.GetComponent<T>();
        }

        public static GameObject CreatePrefab(Transform parent, string prefabName)
        {
            Object o = Resources.Load<GameObject>(prefabName);
            if (o == null)
                return null;
            GameObject go = (GameObject)GameObject.Instantiate(o);
            go.transform.SetParent(  parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            if (parent != null)
                go.layer = parent.gameObject.layer;
            return go;
        }

        public static void AlignTransform(Transform lhs, Transform rhs)
        {
            lhs.position = rhs.position;
            lhs.rotation = rhs.rotation;
        }
        public static void DestoryAllChildren(Transform root)
        {
			bool isPlaying = Application.isPlaying;

            for (int i = root.childCount; i > 0; --i)
            {
                if (isPlaying)
                {
                    GameObject.Destroy(root.GetChild(0).gameObject);
                }
                else
                {
                    GameObject.DestroyImmediate(root.GetChild(0).gameObject);
                }
            }
        }
        public static void DestoryChild(Transform root, string childName)
        {
			bool isPlaying = Application.isPlaying;
			Transform target = GameObjectUtilities.FindByName(root, childName);
			if (target != null) {
				
				if(!isPlaying)
					GameObject.DestroyImmediate (target.gameObject);
				else
					GameObject.Destroy (target.gameObject);

			}
        }

        public static void DeactiveAllChildren( Transform root )
        {
            for( int i = 0; i < root.childCount; i++ )
            {
                Transform t = root.GetChild( i );
                t.gameObject.SetActive( false );
            }
        }
        public static void ChildrenAction(Transform root, System.Action<GameObject> action, bool isRecursion = true)
        {
            for (int i = 0; i < root.childCount; ++i)
            {
                Transform t = root.GetChild(i);
                if (isRecursion) ChildrenAction(t, action);
                action(t.gameObject);
            }
        }

        public static Transform GetRootParent(Transform obj)
        {
            while (obj.parent != null)
            {
                obj = obj.parent;
            }
            return obj;
        }

        public static string GetFullPathName(GameObject obj, string oldStr)
        {
            GameObject o = obj;

            while (o.transform.parent != null)
            {
                oldStr = "/" + o.name + oldStr;
                o = o.transform.parent.gameObject;
            }
            oldStr = "/" + o.name + oldStr;
            return oldStr;
        }

        public static string GetRelativePath(GameObject obj, GameObject child)
        {
            Transform o = child.transform;
            string relitivePath = child.name;
            while (o.parent != null)
            {
                if (o.parent == obj.transform)
                {
                    return relitivePath;
                }
                else
                {
                    relitivePath = o.parent.name + "/" + relitivePath;
                    o = o.transform.parent;

                }
            }
            return "";
        }
        public static void ReplaceTransform(Transform lhs, Transform rhs)
        {
            lhs.SetParent(rhs.parent);
            lhs.localPosition = rhs.localPosition;
            lhs.rotation = rhs.localRotation;
            Object.Destroy(rhs.gameObject);
        }

        public static Transform FindSibling(this Transform transform, string name)
        {
            var parent = transform.parent;
            if (parent != null)
            {
                return parent.Find(name);
            }
            GameObject go = GameObject.Find(name);
            if (go != null)
                return go.transform;
            return null;
        }
    }
}
