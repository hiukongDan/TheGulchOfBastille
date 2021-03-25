using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraShake : MonoBehaviour
{
    public void ShakeCameraVertically(float duration, float range, float speed, float fadeInTime=0f, float fadeOutTime=0f){
        StartCoroutine(shakeCameraVertically(duration, range, speed));
    }

    /// <Summary>
    /// Fade in/out using linear interpolation.
    /// </Summary>
    IEnumerator shakeCameraVertically(float duration, float range, float speed, float fadeInTime=0f, float fadeOutTime=0f){
        float startTime = Time.time;

        GetComponent<BasicFollower>().IsCameraFollowing = false;

        Vector3 oldPosition = transform.position;
        bool isUp = true;
        while(startTime + duration > Time.time){
            float currentTime = Time.time - startTime;
            float multiplier = 1f;
            if(currentTime < fadeInTime){
                // fade in
                multiplier *= currentTime/fadeInTime;
            }
            if(duration - currentTime < fadeOutTime){
                // fade out
                multiplier *= (duration-currentTime)/fadeOutTime;
            }
            float currentRange = range * multiplier;
            Vector3 targetPos = new Vector3(transform.position.x, 
                oldPosition.y + (isUp?Random.Range(0, currentRange):Random.Range(-currentRange, 0)), 
                transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, speed);
            yield return new WaitForEndOfFrame();
            isUp = !isUp;
        }
        GetComponent<BasicFollower>().IsCameraFollowing = true;
    }
}
