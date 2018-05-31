using UnityEngine;

namespace Ferr.VertexPainter {
	[ExecuteInEditMode]
	public class CameraRotate : MonoBehaviour {
		[SerializeField] Transform _target;
		[SerializeField] float     _speed = 2;
		
		float _height;
		float _distance;
		float _startAngle;

		Vector3 Target { get { return _target == null ? Vector3.zero : _target.position; } }

		void Start() {
			Vector3 center = Target;

			Vector3 d = transform.position - center;
			_height = transform.position.y;
			d.y=0;
			_distance   = d.magnitude;
			_startAngle = Mathf.Atan2(d.z, d.x);
		}
		void Update() {
			if (Application.isPlaying) {
				float time = Time.time * _speed;
				transform.position = new Vector3(Mathf.Cos(_startAngle + time) * _distance, _height, Mathf.Sin(_startAngle + time) * _distance);
			}
			transform.LookAt(Target);
		}
	}
}