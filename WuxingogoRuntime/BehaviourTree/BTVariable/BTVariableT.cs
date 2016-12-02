using UnityEngine;
using System.Collections;
using System;

namespace wuxingogo.btFsm
{
    public class BTVariableT<T>: BTVariable
    {
        public virtual T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public override bool isNull()
        {
            return value == null;
        }
        [SerializeField]
        private T value;

        public override object variableValue
        {
            get
            {
                return value;
            }

            set
            {
                this.value = ( T )value;
            }
        }

        public override Type variableType
        {
            get
            {
                return typeof( T );
            }
            set
            {

            }
        }

        public BTVariableT<T> GetSource()
        {
            var source = ( BTVariableT<T> )Source;
            
            return source;
            
        }
    }
}

