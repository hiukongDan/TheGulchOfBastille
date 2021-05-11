using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gulch{
	
public abstract class BouncingCurve
{
	/*
		The curve traverse from t=0 to t=duration.
		and is manually adjusted in such convenience.
	*/
	protected Func<float, float> curveFunc;
	protected float duration;
	
    public BouncingCurve(float duration){
		this.duration = duration;
	}
	
	public virtual Func<float, float> GetCurve(){
		return this.curveFunc;
	}
	
	public virtual float GetValue(float t){
		return curveFunc(t);
	}
	
	public virtual bool IsEnd(float t) => t >= this.duration;
}

public class BouncingCurve_Instance_A : BouncingCurve
{
	/*
		Please using y=\frac{k}{b^{3}}x\left(x-b\right)^{3}
	    in desmos.com/calculator or some equivalent software 
		to see the curve before using them
	*/
	public BouncingCurve_Instance_A(float k=2.5f, float b=9.0f) : base(b){
		this.curveFunc = (t) => k/b/b/b * t * (t - b) * (t - b) * (t - b);
	}
	
}

};
