using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject> { }

/// <summary>
/// 检测GameObject的碰撞盒中是否有指定Layer的物体进入或离开，建议一个碰撞盒对应一个Controller
/// </summary>
public class ZoneTriggerController : MonoBehaviour
{
	[SerializeField] private BoolEvent _enterZone = default;
	[SerializeField] private LayerMask _layers = default;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((1 << other.gameObject.layer & _layers) != 0)
		{
			_enterZone.Invoke(true, other.gameObject);
			Debug.Log("Enter");
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if ((1 << other.gameObject.layer & _layers) != 0)
		{
			_enterZone.Invoke(false, other.gameObject);
		}
	}
}
