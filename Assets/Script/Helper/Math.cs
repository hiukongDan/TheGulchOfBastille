using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gulch{
	
public static class Math
{
    public static bool AlmostEqual(float x, float y, float err=0.01f){
		if (y > x){
			float tmp = x;
			x = y;
			y = tmp;
		}
		return x >= y && x-err <= y;
	}
	
	public static bool AlmostGreater(float x, float y, float err=0.01f){
		return x + err >= y;
	}
	
	public static bool AlmostLesser(float x, float y, float err=0.01f){
		return x-err <= y;
	}
}

};
