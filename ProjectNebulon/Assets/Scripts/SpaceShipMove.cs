using UnityEngine;

// Dynamic Space-Ship Movement
public class SpaceShipMove : MonoBehaviour
{
	public float moveSpeed = 10f;     // Geschwindigkeit vorwärts/rückwärts
	public float strafeSpeed = 7f;    // Seitwärtsbewegung
	public float verticalSpeed = 10f; // Auf-/Abwärtsgeschwindigkeit
	public float rotationSpeed = 5f;  // Drehgeschwindigkeit

	void Update()
	{
		HandleMovement();
		HandleRotation();
	}

	void HandleMovement()
	{
		float moveInput = Input.GetAxis("Vertical");  // W/S (vorwärts/rückwärts)
		float strafeInput = Input.GetAxis("Horizontal"); // A/D (links/rechts)

		float verticalInput = 0f;
		if (Input.GetKey(KeyCode.Space)) verticalInput = 1f;  // Nach oben
		if (Input.GetKey(KeyCode.LeftControl)) verticalInput = -1f; // Nach unten

		// Bewegungsrichtung berechnen
		Vector3 moveDirection = transform.forward * moveInput * moveSpeed +
								transform.right * strafeInput * strafeSpeed +
								transform.up * verticalInput * verticalSpeed;

		transform.position += moveDirection * Time.deltaTime;
	}

	void HandleRotation()
	{
		Vector3 mousePos = Input.mousePosition;
		float screenCenterX = Screen.width * 0.5f;

		// Berechnung der Mausabweichung von der Mitte
		float mouseOffsetX = (mousePos.x - screenCenterX) / screenCenterX;

		// Yaw (links/rechts drehen)
		float yawRotation = mouseOffsetX * rotationSpeed;

		// Rotation nur um die Y-Achse
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + yawRotation, 0);
	}
}
