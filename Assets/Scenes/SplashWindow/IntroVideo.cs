using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{

    private float waitForSec = 8f;
    void Awake(){
        StartCoroutine(PlayIntroVideo());
    }

    IEnumerator PlayIntroVideo(){
        //SceneManager.LoadSceneAsync("Gulch_Main");
        yield return new WaitForSeconds(waitForSec);
        SceneManager.LoadScene("Gulch_Main");
    }
}
