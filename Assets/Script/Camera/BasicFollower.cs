using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BasicFollower : MonoBehaviour
{
    public Transform following;
    private Transform oldFollowing;

    public Rect cameraClamp;

    //public Rect OldCameraClamp;

    public float camSpeed = 5f;
    public float startFollowingDistanceX = 2f;
    public float startFollowingDistanceY = 0.1f;
    public float pixelsPerUnit = 32;

    public float defaultCamZ = -10f;

    private Vector3 workspace;

    private Camera cam;

    private Vector2 fractions;

    public Vector3 deltaPos { get; private set; }

    void Awake()
    {
        cam = Camera.main;
        workspace.Set(following.position.x, following.position.y, cam.transform.position.z);
        workspace.x = Mathf.Clamp(workspace.x, cameraClamp.xMin, cameraClamp.xMax);
        workspace.y = Mathf.Clamp(workspace.y, cameraClamp.yMin, cameraClamp.yMax);
        cam.transform.position = workspace;

        fractions = Vector2.zero;

        deltaPos = Vector3.zero;
    }

    void Start()
    {
        oldFollowing = following;
    }

/*    public void UpdateCameraClampArea(Rect newClamp)
    {
        OldCameraClamp = cameraClamp;
        cameraClamp = newClamp;
    }

    public void RestoreCameraClampArea()
    {
        cameraClamp = OldCameraClamp;
    }*/

    public void UpdateCameraFollowing(Transform following)
    {
        oldFollowing = this.following;
        this.following = following;
    }

    public void RestoreCameraFollowing()
    {
        this.following = oldFollowing;
    }

    void LateUpdate()
    {
        float offsetX = transform.position.x - following.position.x;
        float offsetY = transform.position.y - following.position.y;
        if (Mathf.Abs(offsetX) > startFollowingDistanceX || Mathf.Abs(offsetY) > startFollowingDistanceY)
        {
            workspace = Vector2.Lerp(cam.transform.position, new Vector3(following.position.x, following.position.y), camSpeed * Time.deltaTime);

            // clamp camera's central point
            workspace.x = Mathf.Clamp(workspace.x, cameraClamp.xMin, cameraClamp.xMax);
            workspace.y = Mathf.Clamp(workspace.y, cameraClamp.yMin, cameraClamp.yMax);
            workspace.z = defaultCamZ;

            deltaPos = workspace - cam.transform.position;

            cam.transform.position = workspace;
        }
    }
}
