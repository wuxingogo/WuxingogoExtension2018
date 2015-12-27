//
//  XFsmComponent.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using wuxingogo.Runtime;


namespace wuxingogo.Fsm
{

	[Serializable]
	public class XFsmComponent : wuxingogo.Runtime.XMonoBehaviour, IBehaviourFsm
	{
		#region IBehaviourFsm implementation

		[XAttribute( "" )]
		public void Init()
		{
			Debug.Log( "States Count : " + States.Count );
		}

		public void OnEnter()
		{
			
		}

		public void OnExit()
		{
			
		}

		public void OnUpdate()
		{
			
		}

		public void OnLateUpdate()
		{
			
		}

		public void Reset()
		{
			
		}

		public List<IFsmState> States {
			get {
				return XFsmComponent.I2CFunction<XFsmStateComponent, IFsmState>( states );
			}
			set {
				states = C2IFunction<IFsmState, XFsmStateComponent>( value );
			}
		}

		[SerializeField] List<XFsmStateComponent> states = new List<XFsmStateComponent>();

		#endregion

		public virtual void RegistState(XFsmStateComponent component)
		{
			states.Add( component );
			component.OnwerFsm = this;
		}

		public virtual void UngistState(XFsmStateComponent component)
		{
			states.Remove( component );
			component.OnwerFsm = null;
		}

		public static List<TResult> I2CFunction<TSource,TResult>(List<TSource> sources) 
		where TSource : TResult
		{
			List<TResult> results = new List<TResult>();
			for( int pos = 0; pos < sources.Count; pos++ ) {
				//  TODO loop in states.Count
				results.Add( sources[pos] );
			}
			return results;
		}

		public static List<TResult> C2IFunction<TSource,TResult>(List<TSource> sources) 
		where TResult : TSource
		{
			List<TResult> results = new List<TResult>();
			for( int pos = 0; pos < sources.Count; pos++ ) {
				//  TODO loop in states.Count
				results.Add( (TResult)sources[pos] );
			}
			return results;
		}

		
		
	}
}

