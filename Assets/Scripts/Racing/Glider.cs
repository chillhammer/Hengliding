using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glider : MonoBehaviour {

	private Rigidbody rb;
	public float thrust = 0;
	public float el = 0.002f;
	public float ail = 0.002f;
	public float rud = 0.000125f;
	public float span = .7f;
	public float cord = 0.3f;
	private float AR;
	public float cl0 = 0.1f;
	public float cd0 = 0.01f;
	public float rho = 1.225f;
	public Text indicator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.inertiaTensor = new Vector3(.1f,.1f,.1f);
		rb.maxAngularVelocity = 5;
		AR = Mathf.Pow(span, 2) / cord;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Q)) {
			thrust += Time.deltaTime * 10;
			if (thrust > 200) {
				thrust = 200;
			}
		}
		if (Input.GetKey(KeyCode.E)) {
			thrust -= Time.deltaTime * 10;
			if (thrust < 0) {
				thrust = 0;
			}
		}
		
		
	}

	void FixedUpdate() {
		if (Input.GetMouseButtonUp(0)) {
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (Input.GetMouseButton(0)) {
			float axisH = 2*(Input.mousePosition.x - Screen.width/2)/Screen.width;
			float axisV = 2*(Input.mousePosition.y - Screen.height/2)/Screen.height;
			axisH = Mathf.Min(Mathf.Abs(axisH), 1) * Mathf.Sign(axisH);
			axisV = Mathf.Min(Mathf.Abs(axisV), 1) * Mathf.Sign(axisV);
			rb.AddTorque((-transform.forward * ail * axisH + transform.right * el * axisV) * Mathf.Pow(Vector3.Dot(rb.velocity, transform.forward), 2));
		}
		float rudder = 0;
		if (Input.GetKey(KeyCode.A)) {
			rudder -= 1;
		}
		if (Input.GetKey(KeyCode.D)) {
			rudder += 1;
		}
		rb.AddTorque(transform.up * rud * rudder * Mathf.Pow(Vector3.Dot(rb.velocity, transform.forward), 2));

		Vector3 lift = aeroForce();
		rb.AddForce(transform.forward * thrust + lift);
		rb.AddForce(Vector3.down * 4.9f, ForceMode.Acceleration);
	}

	Vector3 aeroForce() {
		Vector3 vel_e = rb.velocity;
		Matrix4x4 R_eb = Matrix4x4.Rotate(rb.rotation);
		Vector3 vel_b = R_eb.inverse.MultiplyVector(vel_e);

		float alpha = Mathf.Atan2(-vel_b.y, vel_b.z);
		// Debug.Log("Vel_b: " + vel_b.ToString());
		indicator.text = string.Format("Airspeed: {0}\nAlpha: {1}", vel_b.magnitude, alpha);

		float cl = 0;
		float alphaCrit = Mathf.PI / 12;
		float alphaMax = Mathf.PI / 6;
		
		if (Mathf.Abs(alpha) <= alphaCrit) {
			cl = Mathf.PI*2*alpha + cl0;
		} else if (Mathf.Abs(alpha) <= alphaMax) {
			if (alpha > 0) {
				float clmax = Mathf.PI*2*alphaCrit + cl0;
				cl = clmax * (1 - Mathf.Pow((alpha - alphaCrit) / (alphaMax - alphaCrit), 2));
				
			} else {
				float clmin = -Mathf.PI*2*alphaCrit + cl0;
				cl = clmin * (1 - Mathf.Pow((alpha + alphaCrit) / (alphaMax - alphaCrit), 2));
			}
		}
		
		float cd = cd0 + Mathf.Pow(Mathf.PI*2*alpha + cl0,2) / (Mathf.PI * AR);
		if (Mathf.Abs(alpha) > alphaMax) {
			cd = cd0 + Mathf.Pow(Mathf.PI*2*alphaMax + cl0,2) / (Mathf.PI * AR);
		}

		float D = cd * 0.5f * rho * vel_b.sqrMagnitude * cord * span / 5;
		float L = cl * 0.5f * rho * vel_b.sqrMagnitude * cord * span;


		Matrix4x4 R_w = Matrix4x4.Rotate(Quaternion.Euler(alpha / Mathf.PI * 180, 0, 0));
		Vector3 force = R_w.MultiplyVector(L * Vector3.up - D * Vector3.forward);
		force += Vector3.right * -Mathf.Sign(vel_b.x) * Mathf.Pow(vel_b.x,2) * rho * 0.5f / 5;
		force = R_eb.MultiplyVector(force);

		return force;
	}
}
