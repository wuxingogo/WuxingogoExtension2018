namespace wuxingogo.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using wuxingogo.Runtime;


    public class PrefsT<T> : XScriptableObject
    {
        public override string ToString()
        {
            return string.Format( "{0}:{1}", key, Value );
        }

        public T Value = default( T );

        [X]
        public T ValuePrefs
        {
            get { return GetPrefs(); }
            set
            {
                if( object.Equals( value, Value ) == false )
                {
                    Value = value;
                    SetPrefs();
                }
            }
        }

        [X] public string key = string.Empty;

        public PrefsT( string key )
        {
            this.key = key;

        }

        public PrefsT( string key, T defaultValue ) : this( key )
        {
            if( PlayerPrefsManager.HasKey( key ) )
            {
                GetPrefs();
            }
            else
            {
                ValuePrefs = defaultValue;
            }


        }

        public virtual T GetPrefs( T defaultValue )
        {
            return Value;
        }

        [X]
        public virtual T GetPrefs()
        {
            return Value;
        }

        public virtual void SetPrefs()
        {
            PlayerPrefs.Save();
        }
    }
}