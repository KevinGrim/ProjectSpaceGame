using UnityEngine;

public class SpaceShipMove : MonoBehaviour
{
	public float thrustForce = 10f; // Schubkraft
	public float rotationSpeed = 100f; // Rotationsgeschwindigkeit

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false; // Schwerkraft deaktivieren
	}

	void Update()
	{
		// Vorwärts/Rückwärts-Bewegung
		float thrustInput = Input.GetAxis("Vertical");
		Vector3 thrust = transform.forward * thrustInput * thrustForce;
		rb.velocity = new Vector3(thrust.x, rb.velocity.y, thrust.z);

		// Rotation
		float rotateInput = Input.GetAxis("Horizontal");
		Vector3 rotation = new Vector3(0f, rotateInput, 0f) * rotationSpeed * Time.deltaTime;
		transform.Rotate(rotation);
	}
}
