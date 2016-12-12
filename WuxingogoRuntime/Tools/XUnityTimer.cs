

namespace wuxingogo.tools
{
    using UnityEngine;
    using System;
    using System.Diagnostics;
    using System.Collections;
    public class XUnityTimer : IDisposable
    {
        private string m_timerName;
        private int m_numTests;
        private Stopwatch m_watch;
        // give the timer a name, and a count of the number of tests we're

    public XUnityTimer( string timerName, int numTests )
        {
            m_timerName = timerName;
            m_numTests = numTests;
            if( m_numTests <= 0 )
                m_numTests = 1;
            m_watch = Stopwatch.StartNew();
        }
        // called when the 'using' block ends
        public void Dispose()
        {
            m_watch.Stop();
            float ms = m_watch.ElapsedMilliseconds;
            UnityEngine.Debug.Log( string.Format( "{0} finished: {1:0.00}ms total," +
           " {2:0.000000}ms per test for {3} tests", m_timerName, ms, ms / m_numTests, m_numTests ) );
        }
    }
}
