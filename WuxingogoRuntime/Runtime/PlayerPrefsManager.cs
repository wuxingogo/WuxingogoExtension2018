namespace wuxingogo.Data
{

    //#define ENABLE_ENCRYPTION
    //#define DISABLE_ENCRYPTION
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;
    using wuxingogo.Runtime;
    
    
    public class PlayerPrefsManager  {
    
        public static string privateKey="9ETrEsWaFRach3gexaDr";
    
        [X]
        private static List<string> TotalKey
        {
            get
            {
                if( totalKey == null )
                {
                    var value = PlayerPrefs.GetString( "TotalKey" );
                    if( value.Length > 0 )
                    {
                        value = value.Substring( 0, value.Length - 1 );
                    }
                    if(value.Length > 0)
                        totalKey = value.Split( ';' ).ToList();
                    else
                        totalKey = new List<string>();
                }
                return totalKey;
            }
        }
    
        private static List<string> totalKey = null;
        
        [Conditional("UNITY_EDITOR1")]
        public static void RecordKeyInEditor(string key)
        {
            if( !HasKey( key ) )
            {
                TotalKey.Add( key );
            }
            var value = "";
            for( int i = 0; i < TotalKey.Count; i++ )
            {
                value += TotalKey[ i ] + ";";
            }
            PlayerPrefs.SetString( "TotalKey", value);
        }
    
        [X]
        public static string CustomHash( string key )
        {
            #if ENABLE_ENCRYPTION
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(key);
            
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);
    
            string hashString = "";
     
            for( int i = 0; i < hashBytes.Length; i++ )
            {
                hashString += System.Convert.ToString(hashBytes[i], 16);
            }
            
            return hashString;
            #else 
            return key;
            #endif
        }
    
        public static bool HasKey( string key )
        {
            return PlayerPrefs.HasKey( key );
        }
    
        [X]
        public static string GetString( string key, string defaultValue = "" )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            string v = PlayerPrefs.GetString( key, defaultValue );
            v = Decrypt( v );
            return v;
        }
    
        [X]
        public static void SetString( string key, string value )
        {
    
            key = CustomHash( key );
            RecordKeyInEditor( key );
            value = Encryption( value );
    
            PlayerPrefs.SetString( key, value );
        }
    
        [X]
        public static float GetFloat( string key, float defaultValue = 0 )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            float v = PlayerPrefs.GetFloat( key, defaultValue );
            v *= 654321;
            return v;
        }
    
        [X]
        public static void SetFloat( string key, float value )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            value /= 654321;
            PlayerPrefs.SetFloat( key, value );
        }
    
        [X]
        public static int GetInt( string key, int defaultValue = 0 )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            if( !PlayerPrefs.HasKey( key ) )
            {
                PlayerPrefs.SetInt( key, defaultValue - 12345 );
            }
            int v = PlayerPrefs.GetInt( key, defaultValue );
            v += 12345;
            return v;
    
        }
    
        [X]
        public static void SetInt( string key, int value )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            value -= 12345;
            PlayerPrefs.SetInt( key, value );
        }
        
        
        [X]
        public static bool GetBool( string key, bool value = false )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            int v = PlayerPrefs.GetInt( key, value ? 0 : 1 );
            return ((v == 0) ? true : false);
    
        }
    
        [X]
        public static void SetBool( string key, bool value = false )
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            PlayerPrefs.SetInt( key, value ? 0 : 1 );
        }
    
        [X]
        public static void SetList( string key, List<string> list)
        {
            string c = "";
            for( int i = 0; i < list.Count; i++ )
            {
                c += list[ i ] + ";";
            }
            if(c.Length > 0)
                c = c.Substring( 0, c.Length - 1 );
            SetString( key, c );
        }
    
        [X]
        public static List<string> GetList( string key )
        {
            var value = GetString( key );
            if( string.IsNullOrEmpty( value ) )
            {
                return new List<string>();
            }
            var list = value.Split( ';' ).Where( e => !string.IsNullOrEmpty( e ) ).Select( e=>e ).ToList();
            return list;
        }
    
        
        [X]
        public static void SetQueue( string key, Queue<string> enumerable )
        {
            string c = "";
            foreach( var v in enumerable )
            {
                c += v+ ";";
            }
            if(c.Length > 0)
                c = c.Substring( 0, c.Length - 1 );
            SetString( key, c );
        }
        [X]
        public static Queue<string> GetQueue( string key )
        {
            var value = GetString( key );
            if( string.IsNullOrEmpty( value ) )
            {
                return new Queue<string>();
            }
            var queue = new Queue<string>();
            string[] v = value.Split( ';' );
            for( int i = 0; i < v.Length; i++ )
            {
                queue.Enqueue( v[ i ] );
            }
            return queue;
        }
        
        [X]
        public static void SetStack( string key, Stack<string> enumerable )
        {
            string c = "";
            foreach( var v in enumerable )
            {
                c += v + ";";
            }
            if(c.Length > 0)
                c = c.Substring( 0, c.Length - 1 );
            SetString( key, c );
        }
        [X]
        public static Stack<string> GetStack( string key )
        {
            var value = GetString( key );
            if( string.IsNullOrEmpty( value ) )
            {
                return new Stack<string>();
            }
            var stack = new Stack<string>();
            string[] v = value.Split( ';' );
            for( int i = 0; i < v.Length; i++ )
            {
                stack.Push( v[ i ] );
            }
            return stack;
        }
    
        [X]
        public static DateTime GetTime(string key, DateTime defaultTime)
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            if( !PlayerPrefs.HasKey( key ) )
            {
                var value = Encryption( defaultTime.ToString() );
    
                PlayerPrefs.SetString( key, value );
                return defaultTime;
            }
            
            string str = PlayerPrefs.GetString( key );
            str = Decrypt( str );
            return System.DateTime.Parse(str);
        }
        
        [X]
        public static DateTime GetTime(string key)
        {
            key = CustomHash( key );
            RecordKeyInEditor( key );
            if( !PlayerPrefs.HasKey( key ) )
            {
                var value = Encryption( DateTime.MinValue.ToString() );
    
                PlayerPrefs.SetString( key, value );
                return DateTime.MinValue;
            }
    
            string str = PlayerPrefs.GetString( key );
            str = Decrypt( str );
            return System.DateTime.Parse(str);
        }
        
        public static void SetTime( string key, System.DateTime time ) {
            string str = time.ToString();
            SetString(key, str);
        }
    
        ///
        /// ref : https://www.cnblogs.com/guohu/p/5562759.html
        /// 
        //加密
        [X]
        private static string Encryption(string express)
        {
            #if ENABLE_ENCRYPTION
            if( string.IsNullOrEmpty( express ) )
                return string.Empty;
            CspParameters param = new CspParameters();
            param.KeyContainerName = privateKey;//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
            #else
            return express;
            #endif
        }
     
        //解密
        [X]
        private static string Decrypt(string ciphertext)
        {
    #if ENABLE_ENCRYPTION
            if( string.IsNullOrEmpty( ciphertext ) )
                return string.Empty;
            CspParameters param = new CspParameters();
            param.KeyContainerName = privateKey;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
            #else
            return ciphertext;
            #endif
        }
        
        ////==============================================////
    }

}



