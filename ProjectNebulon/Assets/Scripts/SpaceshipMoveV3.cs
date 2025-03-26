using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMoveV3 : MonoBehaviour
{
	[Header("General Movement")]
	public float forwardSpeed = 23f, strafeSpeed = 7f, hoverSpeed = 5f; 
	private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
	private float forwardAccele = 2.5f, strafeAccele = 2f, hoverAccele = 2f;

	[Header("Boost Settings")]
	public float boostMultiplier = 2f; // Boost Kraft 
	public float boostDuration = 0.5f; // Zeit Wie lange man geboosted wird
	public float boostCooldown = 2f; // Cooldown-Zeit zwischen Boosts
	private bool isBoosting = false;
	private bool canBoost = true;

	[Header("Look-Rate Speed")]
	public float lookRateSpeed = 90f;
	private Vector2 lookInput, screenCenterPoint, mouseDistance;

	[Header("Roll Speed")]
	private float rollInput;
	public float rollSpeed = 90f, rollAccele = 3;

	void Start()
	{
		screenCenterPoint.x = Screen.width * .5f;
		screenCenterPoint.y = Screen.height * .5f;

		Cursor.lockState = CursorLockMode.Confined; // Macht das der Mouse cursor nicht aus dem bild kann
		Cursor.visible = false; // Blendet den Mouse Cursor aus Ingame.
	}

	void Update()
	{
		lookInput.x = Input.mousePosition.x;
		lookInput.y = Input.mousePosition.y;

		mouseDistance.x = (lookInput.x - screenCenterPoint.x) / screenCenterPoint.x;
		mouseDistance.y = (lookInput.y - screenCenterPoint.y) / screenCenterPoint.y;
		mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

		rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAccele * Time.deltaTime);
		transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

		activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAccele * Time.deltaTime);
		activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAccele * Time.deltaTime);
		activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAccele * Time.deltaTime);

		transform.position += transform.forward * activeForwardSpeed * Time.deltaTime; //Lässt das Ship vertical bewegen
		transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

		// Boost-Funktion mit Cooldown
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting && canBoost)
		{
			StartCoroutine(Boost());
		}
	}

	private IEnumerator Boost()
	{
		isBoosting = true;
		canBoost = false;
		float originalSpeed = forwardSpeed;
		forwardSpeed *= boostMultiplier;
		yield return new WaitForSeconds(boostDuration);
		forwardSpeed = originalSpeed;
		isBoosting = false;

		yield return new WaitForSeconds(boostCooldown); // Wartezeit bis zu dem nächsten Boost
		canBoost = true;
	}
}
