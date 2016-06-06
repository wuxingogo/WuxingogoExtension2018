using UnityEngine;
using System;


namespace wuxingogo.tools {
    [ExecuteInEditMode]
    public class ShowFPS : MonoBehaviour
    {
        [NonSerialized]
        private int mFrame;

        [NonSerialized]
        public int mFPS;

        [NonSerialized]
        private float mNextFPS = 0.5f;

        private void Update()
        {
            this.mFrame++;
            this.mNextFPS += Time.unscaledDeltaTime;
            while (this.mNextFPS > 0.5f)
            {
                this.mNextFPS -= 0.5f;
                this.mFPS = Mathf.RoundToInt((float)this.mFrame * 2f);
                this.mFrame = 0;
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(100,100, 100, 100), this.mFPS.ToString(), new GUIStyle());
        }
    }
}