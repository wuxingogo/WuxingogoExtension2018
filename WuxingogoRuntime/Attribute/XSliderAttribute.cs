using UnityEngine;

namespace wuxingogo.Runtime
{
    public class XSliderAttribute : PropertyAttribute
    {
        public int MinValue = 0;
        public int MaxValue = 0;

        public XSliderAttribute( int min, int max )
        {
            this.MinValue = min;
            this.MaxValue = max;
        }
    }
}