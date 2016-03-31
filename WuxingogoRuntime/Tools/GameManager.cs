using UnityEngine;
using wuxingogo.Runtime;

namespace wuxingogo.Runtime
{
	
	public class GameManager<T> : XScriptableObject where T : XScriptableObject
    {
		private static T m_instance;

		public static T instance
		{
			get
			{
				if (m_instance == null)
				{
					m_instance = Resources.Load<T>(string.Format("GameManager/{0}", typeof(T).Name));
				}

				return m_instance;
			}
		}
    }
}
