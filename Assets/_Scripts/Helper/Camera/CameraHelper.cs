using UnityEngine;

namespace ARKit.Helper.Camera
{
    public class CameraHelper : MonoBehaviour
    {
		const float ACCELERATION = 10;
		const float ACC_SPRINT_MULTIPLIER = 4;
		const float LOOK_SENSITIVITY = 1;
		const float DAMPING_COEFFICIENT = 5;

		private Vector3 velocity;

		void Update()
		{
			UpdateInput();

			velocity = Vector3.Lerp(velocity, Vector3.zero, DAMPING_COEFFICIENT * Time.deltaTime);

			transform.position += velocity * Time.deltaTime;
		}

		void UpdateInput()
		{
			velocity += GetAccelerationVector() * Time.deltaTime;

			Vector2 mouseDelta = LOOK_SENSITIVITY * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

			Quaternion rotation = transform.rotation;
			Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
			Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);

			transform.rotation = horiz * rotation * vert;
		}

		Vector3 GetAccelerationVector()
		{
			Vector3 moveInput = default;

			void AddMovement(KeyCode key, Vector3 dir)
			{
				if (Input.GetKey(key))
					moveInput += dir;
			}

			AddMovement(KeyCode.W, Vector3.forward);
			AddMovement(KeyCode.S, Vector3.back);
			AddMovement(KeyCode.D, Vector3.right);
			AddMovement(KeyCode.A, Vector3.left);
			AddMovement(KeyCode.Space, Vector3.up);
			AddMovement(KeyCode.LeftControl, Vector3.down);

			Vector3 direction = transform.TransformVector(moveInput.normalized);

			if (Input.GetKey(KeyCode.LeftShift))
				return direction * (ACCELERATION * ACC_SPRINT_MULTIPLIER);

			return direction * ACCELERATION;
		}
	}
}
