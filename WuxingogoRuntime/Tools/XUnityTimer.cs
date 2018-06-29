//
// XUnityTimer.cs
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
            XLogger.Log( string.Format( "{0} finished: {1:0.00}ms total," +
           " {2:0.000000}ms per test for {3} tests", m_timerName, ms, ms / m_numTests, m_numTests ) );
        }
    }
}
