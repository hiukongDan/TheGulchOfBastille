using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gulch{
	
public abstract class BouncingCurve
{
	/*
		The curve traverses from t=0 to t=duration.
		and is manually adjusted in such convenience.
	*/
	protected Func<float, float> curveFunc;
	protected float duration;
	protected Dictionary<float, float> partialValueTable;
	
    public BouncingCurve(float duration){
		this.duration = duration;
	}
	
	public virtual Func<float, float> GetCurve(){
		return this.curveFunc;
	}
	
	public virtual float GetValue(float t){
		return curveFunc(t);
	}
	
	public virtual float GetFirstRoot(float value){
		/*
			Warning!
			Not promising to be bug free :)
		*/
		float ret = 0f;
		foreach(KeyValuePair<float, float> kvp in partialValueTable){
			if(Gulch.Math.AlmostEqual(value, kvp.Value, 1f)){
				ret = kvp.Key;
				break;
			}
		}
		for(; !Gulch.Math.AlmostEqual(value, GetValue(ret)); ret+=0.01f)
			;
		
		return ret;
	}
	
	public virtual bool IsEnd(float t) => t >= this.duration;
	
	protected virtual void calculatePartialValueTable(){
		partialValueTable = new Dictionary<float, float>();
		for(float x = 0f; x < this.duration; x += 1f){
			partialValueTable.Add(x, GetValue(x));
		}
	}
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
		calculatePartialValueTable();
	}
	
}

};
