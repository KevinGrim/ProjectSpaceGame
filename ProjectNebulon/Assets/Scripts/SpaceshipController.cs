using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
	[Header("Movement")]
	public float baseSpeed = 10f;         // Basisgeschwindigkeit
	public float boostMultiplier = 3f;    // Boost-Faktor
	public float strafeSpeed = 5f;        // Seitwärts- und Auf/Ab-Geschwindigkeit
	public float acceleration = 5f;       // Beschleunigungsrate

	[Header("Rotation")]
	public float rotationSpeed = 90f;     // Drehgeschwindigkeit in Grad/Sekunde
	public float rollSpeed = 80f;         // Rollen mit Q/E

	[Header("Physics")]
	public float damping = 2f;            // Trägheit (je höher, desto weniger Drift)
	public bool useInertia = true;        // Falls Trägheit gewünscht ist

	private Rigidbody rb;
	private Vector3 targetVelocity;
	private Vector3 currentVelocity;
	private Vector3 angularVelocity;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.drag = 0;
		rb.angularDrag = 0;
		Cursor.lockState = CursorLockMode.Locked; // Maus sperren
	}

	void Update()
	{
		HandleRotation();
		HandleMovement();
	}

	void FixedUpdate()
	{
		if (useInertia)
		{
			// Weiche Geschwindigkeitsübergänge mit Trägheit
			currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.fixedDeltaTime * damping);
			rb.velocity = currentVelocity;
		}
		else
		{
			rb.velocity = targetVelocity;
		}

		// Sanfte Rotation anwenden
		rb.angularVelocity = angularVelocity;
	}

	void HandleRotation()
	{
		float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
		float roll = 0f;

		if (Input.GetKey(KeyCode.Q)) roll = rollSpeed * Time.deltaTime;
		if (Input.GetKey(KeyCode.E)) roll = -rollSpeed * Time.deltaTime;

		// Rotation basierend auf Mausbewegung und Rollen
		Quaternion yaw = Quaternion.Euler(0, mouseX, 0);
		Quaternion pitch = Quaternion.Euler(-mouseY, 0, 0);
		Quaternion rollRotation = Quaternion.Euler(0, 0, roll);

		rb.rotation *= yaw * pitch * rollRotation;
	}

	void HandleMovement()
	{
		float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f;

		Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical") * baseSpeed * speedMultiplier;
		Vector3 strafeMovement = transform.right * Input.GetAxis("Horizontal") * strafeSpeed;
		Vector3 verticalMovement = transform.up * ((Input.GetKey(KeyCode.Space) ? 1 : 0) - (Input.GetKey(KeyCode.LeftControl) ? 1 : 0)) * strafeSpeed;

		targetVelocity = forwardMovement + strafeMovement + verticalMovement;
	}
}
