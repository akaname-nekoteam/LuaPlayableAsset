public class BulletBehaviour : UnityEngine.MonoBehaviour
{
	// カメラから出たら自害する
	private void OnBecameInvisible() => UnityEngine.GameObject.Destroy( gameObject);
} // class BulletBehaviour

