using UnityEngine;
using System.Collections;
using wuxingogo.Runtime;
using System.Collections.Generic;

namespace wuxingogo.btFsm
{
    public class BTVariable: XScriptableObject
    {
        [X]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public BTFsm Owner = null;

        public BTVariable Source = null;

        public bool isGlobal = false;

        public bool isAwake = false;

        public virtual bool isNull()
        {
            return true;
        }

        public virtual object variableValue
        {
            get;
            set;
        }

        public virtual System.Type variableType
        {
            get;
            set;
        }

        public virtual void OnCreate()
        {

        }

        public virtual void OnAwake()
        {
            
        }

        public static BTVariable Create(BTFsm btFsm, BTVariable source)
        {
            var btVar = Instantiate( source );
            btVar.Owner = btFsm;
            btVar.Source = source;
            btVar.Name = source.Name;
            btFsm.totalVariable.Add( btVar );
            return btVar;
        }

       
    }
}
