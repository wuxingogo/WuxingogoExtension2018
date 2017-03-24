//
// SingletonClass.cs
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

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace wuxingogo.tools
{
    public interface ISingletonT
    {
        void DestoryT();
    }
    public class SingletonMBT<T> : MonoBehaviour, ISingletonT 
        where T : SingletonMBT<T>
    {
        static T mInstance = null;
        public static T Inst { get { return mInstance; } }
        public void DestoryT()
        {
            MonoBehaviour _this = this as MonoBehaviour;
            Destroy(_this.gameObject);
        }
        protected virtual void Awake()
        {
            mInstance = this as T;
        }
        protected virtual void OnDestroy()
        {
            mInstance = null;
        }
    }

#region 自动单件实例
    public class AutoSingleT<T> : ISingletonT where T : class//new()，new不支持非公共的无参构造函数 
    {
        /*
         * 单线程测试通过！
         * 多线程测试通过！
         * 根据需要在调用的时候才实例化单例类！
        */
        private static T mInstance;
        public static readonly object SyncObject = new object();
        public static T Inst
        {
            set
            {
                mInstance = value;
            }
            get
            {
                //没有第一重 singleton == null 的话，每一次有线程进入 GetInstance()时，均会执行锁定操作来实现线程同步，
                //非常耗费性能 增加第一重singleton ==null 成立时的情况下执行一次锁定以实现线程同步
                if (mInstance == null)
                {
                    lock (SyncObject)
                    {
                        if (mInstance == null)//Double-Check Locking 双重检查锁定
                        {
                            //_instance = new T();
                            //需要非公共的无参构造函数，不能使用new T() ,new不支持非公共的无参构造函数 
#if NETFX_CORE && UNITY_METRO && !UNITY_EDITOR
                            try
                            {
                                mInstance = Activator.CreateInstance<T>();
                            }
                            catch(System.MissingMemberException e)
                            {
                                Logger.LogError(e.Message);
                            }
#else
                            mInstance = (T)Activator.CreateInstance(typeof(T), true); //第二个参数防止异常：“没有为该对象定义无参数的构造函数。”
#endif
                        }
                    }
                }
                return mInstance;
            }
        }
        public void DestoryT()
        {
            Inst = null;
        }
    }
#endregion

//#region 预创Ωion

#region 预创建单件2
    public class SingletonT<T> : ISingletonT where T : class//new()，new不支持非公共的无参构造函数 
    {
        /*         
         * 单线程测试通过！
         * 多线程测试通过！
         *主动实例化单例类！
         * 注：使用静态初始化的话，无需显示地编写线程安全代码，C# 与 CLR 会自动解决多线程同步问题
        */
        protected SingletonT() { }
        /*内部类
         * 创建内部类的一个目的是为了抽象外部类的某一状态下的行为，
         * 或者C#内部类仅在外部类的某一特定上下文存在。或是隐藏实现，
         * 通过将内部类设为private，可以设置仅有外部类可以访问该类。
         * 内部类的另外一个重要的用途是当外部类需要作为某个特定的类工作，
         * 而外部类已经继承与另外一个类的时候，因为C#不支持多继承，
         * 所以创建一个对应的内部类作为外部类的一个facade来使用。 
        */
        protected class SingletonCreator
        {
            internal static readonly T Instance = (T)Activator.CreateInstance(typeof(T), true);// new T();
            internal static bool Created = false;
        }
        
        internal static T _instance = null;
        public static T GetInstance(){
			if(_instance == null){
				_instance = (T)Activator.CreateInstance(typeof(T), true);// new T();
			}
			return _instance;
        }
		public void DestoryT(){
			
		}
    }
#endregion
}
