using UnityEditor;
using UnityEngine;
using System;

namespace wuxingogo.Editor
{
    public class XTomporaryWindow : XBaseWindow {
        public Action OnPaint = null;

        public override void OnXGUI()
        {
            base.OnXGUI();
            if( OnPaint == null )
            {
                this.Close();
                return;
            }
            OnPaint();
        }
    }
}

