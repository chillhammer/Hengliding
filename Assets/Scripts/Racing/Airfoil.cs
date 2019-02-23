using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airfoil
{
	private float span;
	private float cord;
	private float AR;
	private float cl0;
	private float cd0;
	private float rho = 1.225f;
    private Matrix4x4 R;
    public Vector3 pos;

    public Airfoil(Vector3 pos, Quaternion orient, float span, float cord, float cl0, float cd0){
        AR = Mathf.Pow(span,2)*cord;
        this.pos = pos;
        R = Matrix4x4.Rotate(orient);
        this.span = span;
        this.cord = cord;
        this.cl0 = cl0;
        this.cd0 = cd0;
    }

    public Vector3 aeroForce(Vector3 vel_b, Vector3 ang_vel, float defl) {
		// Vector3 vel_e = rb.velocity;
		// Matrix4x4 R_eb = Matrix4x4.Rotate(rb.rotation);
		// Vector3 vel_b = R_eb.inverse.MultiplyVector(vel_e);
        vel_b = vel_b + Vector3.Cross(ang_vel, pos);
        vel_b = R.inverse.MultiplyVector(vel_b);

        Matrix4x4 Defl = Matrix4x4.Rotate(Quaternion.Euler(defl,0,0));
        vel_b = Defl.inverse.MultiplyVector(vel_b);
        // Debug.Log(vel_b);

		float alpha = Mathf.Atan2(-vel_b.y, vel_b.z);

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
		// force += Vector3.right * -Mathf.Sign(vel_b.x) * Mathf.Pow(vel_b.x,2) * rho * 0.5f;
        force = Defl.MultiplyVector(force);
        force = R.MultiplyVector(force);
		// force = R_eb.MultiplyVector(force);

		return force;
	}
}
