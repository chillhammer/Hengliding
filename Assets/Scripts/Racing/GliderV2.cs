using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderV2 : MonoBehaviour {

	private Rigidbody rb;
	public float thrust = 0;
	public float el = 0.4f;
	public float ail = 0.4f;
	public float rud = 0.4f;
	public float span = 2;
	public float cord = 0.5f;
	public float rho = 1.225f;
	private Airfoil[] airfoils = new Airfoil[4];
	private float da = 0;
	private float de = 0;
	private float dr = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;

		rb.inertiaTensor = new Vector3(1,1,1);
		Debug.Log("J: " + rb.inertiaTensor.ToString());

		// Create wings
		airfoils[0] = new Airfoil(new Vector3(span/4, 0, 0), Quaternion.Euler(0,0,0), span/2, cord, 0.1f, 0.01f);
		airfoils[1] = new Airfoil(new Vector3(-span/4, 0, 0), Quaternion.Euler(0,0,0), span/2, cord, 0.1f, 0.01f);
		airfoils[2] = new Airfoil(new Vector3(0, 0, -.5f), Quaternion.Euler(0,0,0), span/2, cord, 0.1f, 0.01f);
		airfoils[3] = new Airfoil(new Vector3(0, span/8, -.5f), Quaternion.Euler(90,0,0), span/4, cord, 0, 0.01f);
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
		if (Input.GetMouseButton(0)) {
			float axisH = Input.GetAxis("Horizontal")/100;
			float axisV = Input.GetAxis("Vertical")/100;
			axisH = Mathf.Min(Mathf.Abs(axisH), 1) * Mathf.Sign(axisH);
			axisV = Mathf.Min(Mathf.Abs(axisV), 1) * Mathf.Sign(axisV);
			da += ail * axisH;
			de -= el * axisV;
			dr += rud * axisH;
			if (Mathf.Abs(da) > 30) {
				da = Mathf.Sign(da) * 30;
			}
			if (Mathf.Abs(de) > 30) {
				de = Mathf.Sign(de) * 30;
			}
			if (Mathf.Abs(dr) > 30 * rud / ail) {
				dr = Mathf.Sign(dr) * 30 * rud / ail;
			}
			// rb.AddTorque((transform.up * rud - transform.forward * ail) * axisH * Mathf.Pow(Vector3.Dot(rb.velocity, transform.forward), 2)/1000);
			// rb.AddTorque(transform.right * el * axisV * Mathf.Pow(Vector3.Dot(rb.velocity, transform.forward), 2)/1000);
		}
		if (Input.GetMouseButtonUp(0)) {
			da = 0;
			de = 0;
			dr = 0;
		}
		
		Matrix4x4 R_eb = Matrix4x4.Rotate(rb.rotation);
		Vector3 force = new Vector3(0,0,0);
		Vector3 torque = new Vector3(0,0,0);
		float[] defl = {da, -da, de, dr};
		Vector3 vel_b = R_eb.inverse.MultiplyVector(rb.velocity);
		Vector3 ang_b = R_eb.inverse.MultiplyVector(rb.angularVelocity);
		Debug.Log("Vel_b: " + vel_b.ToString());
		Debug.Log("Ang_b: " + ang_b.ToString());

		for (int i = 0; i < 3; i++) {
			// Debug.Log("Deflections: " + defl[i].ToString());
			Vector3 df = airfoils[i].aeroForce(vel_b, ang_b, defl[i]);
			Debug.Log("df: " + df.ToString());
			force += df;
			torque += Vector3.Cross(airfoils[i].pos, df);
			// Debug.Log(torque);
		}
		force += transform.right * -Mathf.Sign(vel_b.x) * Mathf.Pow(vel_b.x,2) * rho * 0.5f;
		rb.AddForce(transform.forward * thrust + R_eb.MultiplyVector(force));
		rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
		rb.AddTorque(R_eb.MultiplyVector(torque));
	}

	// Vector3 aeroForce() {
	// 	Vector3 vel_e = rb.velocity;
	// 	Matrix4x4 R_eb = Matrix4x4.Rotate(rb.rotation);
	// 	Vector3 vel_b = R_eb.inverse.MultiplyVector(vel_e);

	// 	float alpha = Mathf.Atan2(-vel_b.y, vel_b.z);
	// 	// Debug.Log("alpha: " + alpha.ToString());
	// 	Debug.Log("Vel_b: " + vel_b.ToString());

	// 	float cl = 0;
	// 	float alphaCrit = Mathf.PI / 12;
	// 	float alphaMax = Mathf.PI / 6;
		
	// 	if (Mathf.Abs(alpha) <= alphaCrit) {
	// 		cl = Mathf.PI*2*alpha + cl0;
	// 	} else if (Mathf.Abs(alpha) <= alphaMax) {
	// 		if (alpha > 0) {
	// 			float clmax = Mathf.PI*2*alphaCrit + cl0;
	// 			cl = clmax * (1 - Mathf.Pow((alpha - alphaCrit) / (alphaMax - alphaCrit), 2));
				
	// 		} else {
	// 			float clmin = -Mathf.PI*2*alphaCrit + cl0;
	// 			cl = clmin * (1 - Mathf.Pow((alpha + alphaCrit) / (alphaMax - alphaCrit), 2));
	// 		}
	// 	}
		
	// 	float cd = cd0 + Mathf.Pow(Mathf.PI*2*alpha + cl0,2) / (Mathf.PI * AR);
	// 	if (Mathf.Abs(alpha) > alphaMax) {
	// 		cd = cd0 + Mathf.Pow(Mathf.PI*2*alphaMax + cl0,2) / (Mathf.PI * AR);
	// 	}

	// 	float D = cd * 0.5f * rho * vel_b.sqrMagnitude * cord * span / 5;
	// 	float L = cl * 0.5f * rho * vel_b.sqrMagnitude * cord * span;
	// 	// Debug.Log("Lift: " + L.ToString());
	// 	// Debug.Log("Drag: " + D.ToString());

	// 	Matrix4x4 R_w = Matrix4x4.Rotate(Quaternion.Euler(alpha / Mathf.PI * 180, 0, 0));
	// 	// Debug.Log("R_w: " + R_w.ToString());
	// 	Vector3 force = R_w.MultiplyVector(L * Vector3.up - D * Vector3.forward);
	// 	force += Vector3.right * -Mathf.Sign(vel_b.x) * Mathf.Pow(vel_b.x,2) * rho * 0.5f;
	// 	// Debug.Log("Force_b: " + force.ToString());
	// 	force = R_eb.MultiplyVector(force);
	// 	// Debug.Log("Lift: " + R_w.MultiplyVector(L * Vector3.up).ToString());
	// 	// Debug.Log("Drag: " + R_w.MultiplyVector( - D * Vector3.forward).ToString());

	// 	return force;
	// }
}
