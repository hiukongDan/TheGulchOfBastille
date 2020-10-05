using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollower : MonoBehaviour
{
    private Camera cam;

    public Transform following;
    public float camSpeed = 1f;

    public float startFollowingDistance = 1f;

    public Rect cameraClamp;

    private Vector3 workspace;

    void Start()
    {
        cam = Camera.main;
        workspace.Set(following.position.x, following.position.y, cam.transform.position.z);
        workspace.x = Mathf.Clamp(workspace.x, cameraClamp.xMin, cameraClamp.xMax);
        workspace.y = Mathf.Clamp(workspace.y, cameraClamp.yMin, cameraClamp.yMax);
        cam.transform.position = workspace;
    }

    void LateUpdate()
    {
        if(Mathf.Abs(transform.position.x - following.position.x) > startFollowingDistance)
        {
            workspace = Vector3.Lerp(cam.transform.position, new Vector3(following.position.x, following.position.y, cam.transform.position.z), camSpeed * Time.deltaTime);
            workspace.x = Mathf.Clamp(workspace.x, cameraClamp.xMin, cameraClamp.xMax);
            workspace.y = Mathf.Clamp(workspace.y, cameraClamp.yMin, cameraClamp.yMax);
            cam.transform.position = workspace;
        }
    }

}
