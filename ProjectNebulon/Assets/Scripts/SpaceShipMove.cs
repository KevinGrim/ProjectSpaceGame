using UnityEngine;

public class SpaceShipMove : MonoBehaviour
{
	public float moveSpeed = 10f;      // Normale Geschwindigkeit
	public float boostMultiplier = 2f; // Boost-Faktor (wie viel schneller)
	public float strafeSpeed = 7f;     // Seitw�rtsgeschwindigkeit
	public float verticalSpeed = 10f;  // Auf-/Abw�rtsgeschwindigkeit
	public float rotationSpeed = 5f;   // Drehgeschwindigkeit

	void Update()
	{
		HandleMovement();
		HandleRotation();
	}

	void HandleMovement()
	{
		float moveInput = Input.GetAxis("Vertical");  // W/S (vorw�rts/r�ckw�rts)
		float strafeInput = Input.GetAxis("Horizontal"); // A/D (links/rechts)

		float verticalInput = 0f;
		if (Input.GetKey(KeyCode.Space)) verticalInput = 1f;  // Nach oben
		if (Input.GetKey(KeyCode.LeftControl)) verticalInput = -1f; // Nach unten

		// Boost aktivieren, wenn Shift gedr�ckt wird
		float currentSpeed = moveSpeed;
		float currentStrafeSpeed = strafeSpeed;
		float currentVerticalSpeed = verticalSpeed;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			currentSpeed *= boostMultiplier;
			currentStrafeSpeed *= boostMultiplier;
			currentVerticalSpeed *= boostMultiplier;
		}

		// Bewegungsrichtung berechnen
		Vector3 moveDirection = transform.forward * moveInput * currentSpeed +
								transform.right * strafeInput * currentStrafeSpeed +
								transform.up * verticalInput * currentVerticalSpeed;

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
