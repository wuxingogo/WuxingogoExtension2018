namespace wuxingogo.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PrefsInt : PrefsT<int>
    {
        public PrefsInt( string key ) : base( key )
        {
        }

        public PrefsInt( string key, int defaultValue ) : base( key, defaultValue )
        {
        }

        public override int GetPrefs()
        {
            Value = PlayerPrefsManager.GetInt( key );
            return Value;
        }

        public override int GetPrefs( int defaultValue )
        {
            Value = PlayerPrefsManager.GetInt( key, defaultValue );
            return Value;
        }

        public override void SetPrefs()
        {
            PlayerPrefsManager.SetInt( key, Value );
            base.SetPrefs();
        }
    }

    public class PrefsString : PrefsT<string>
    {
        public PrefsString( string key ) : base( key )
        {
        }

        public PrefsString( string key, string defaultValue ) : base( key, defaultValue )
        {
        }

        public override string GetPrefs( string defaultValue )
        {
            Value = PlayerPrefsManager.GetString( key, defaultValue );
            return Value;
        }

        public override string GetPrefs()
        {
            Value = PlayerPrefsManager.GetString( key );
            return Value;
        }

        public override void SetPrefs()
        {
            PlayerPrefsManager.SetString( key, Value );
            base.SetPrefs();
        }
    }

    public class PrefsDateTime : PrefsT<DateTime>
    {
        public PrefsDateTime( string key ) : base( key )
        {
        }

        public PrefsDateTime( string key, DateTime defaultValue ) : base( key, defaultValue )
        {
        }

        public override DateTime GetPrefs( DateTime defaultValue )
        {
            Value = PlayerPrefsManager.GetTime( key, defaultValue );
            return Value;
        }

        public override DateTime GetPrefs()
        {
            Value = PlayerPrefsManager.GetTime( key );
            return Value;
        }

        public override void SetPrefs()
        {
            PlayerPrefsManager.SetTime( key, Value );
            base.SetPrefs();
        }
    }

    public class PrefsLong : PrefsT<long>
    {
        public PrefsLong( string key ) : base( key )
        {

        }

        public PrefsLong( string key, long defaultValue ) : base( key, defaultValue )
        {
        }

        public override long GetPrefs( long defaultValue )
        {
            Value = PlayerPrefsManager.GetInt( key, ( int )( object )defaultValue );
            return Value;
        }

        public override long GetPrefs()
        {
            Value = PlayerPrefsManager.GetInt( key );
            return Value;
        }

        public override void SetPrefs()
        {
            PlayerPrefsManager.SetInt( key, ( int )Value );
            base.SetPrefs();
        }
    }

    public class PrefsBool : PrefsT<bool>
    {
        public PrefsBool( string key ) : base( key )
        {
        }

        public PrefsBool( string key, bool defaultValue ) : base( key, defaultValue )
        {
        }

        public override bool GetPrefs( bool defaultValue )
        {
            Value = PlayerPrefsManager.GetBool( key, defaultValue );
            return Value;
        }

        public override bool GetPrefs()
        {
            Value = PlayerPrefsManager.GetBool( key );
            return Value;
        }

        public override void SetPrefs()
        {
            PlayerPrefsManager.SetBool( key, Value );
            base.SetPrefs();
        }
    }
}