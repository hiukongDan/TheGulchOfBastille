using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BasicFollower : MonoBehaviour
{
    public Transform following;

    public Rect cameraClamp;

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

    }

    void LateUpdate()
    {
        float offsetX = transform.position.x - following.position.x;
        float offsetY = transform.position.y - following.position.y;
        if (Mathf.Abs(offsetX) > startFollowingDistanceX || Mathf.Abs(offsetY) > startFollowingDistanceY)
        {
            workspace = Vector2.Lerp(cam.transform.position, new Vector3(following.position.x, following.position.y), camSpeed * Time.deltaTime);

            workspace.x = Mathf.Clamp(workspace.x, cameraClamp.xMin, cameraClamp.xMax);
            workspace.y = Mathf.Clamp(workspace.y, cameraClamp.yMin, cameraClamp.yMax);
            workspace.z = defaultCamZ;

            deltaPos = workspace - cam.transform.position;

            cam.transform.position = workspace;
        }
    }
}
