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

    public static Dictionary<SceneCode, Rect> cameraClamps = new Dictionary<SceneCode, Rect>();

    public float camSpeed = 5f;
    public float startFollowingDistanceX = 2f;
    public float startFollowingDistanceY = 0.1f;
    public float pixelsPerUnit = 32;

    public float defaultCamZ = -10f;

    private Vector3 workspace;

    private Camera cam;

    private Vector2 fractions;

    public Vector3 deltaPos { get; private set; }

    private Vector2 viewportWorldSize = new Vector2(10, 7);

    public bool isDebug = true;

    public bool IsCameraFollowing = true;

    public SceneCode sceneCode;

    void Awake()
    {
        cam = Camera.main;
        workspace.Set(following.position.x, following.position.y, cam.transform.position.z);
        workspace.x = Mathf.Clamp(workspace.x, cameraClamp.xMin, cameraClamp.xMax);
        workspace.y = Mathf.Clamp(workspace.y, cameraClamp.yMin, cameraClamp.yMax);
        cam.transform.position = workspace;

        deltaPos = Vector3.zero;
    }

    void Start()
    {
        oldFollowing = following;
        // Vector2 center = cam.ViewportToWorldPoint(Vector2.zero);
        // Vector2 topright = cam.ViewportToWorldPoint(Vector2.one);

        // viewportWorldSize = new Vector2(2*(topright.x - center.x), 2*(topright.y - center.y));

        
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

    public void ClampCamera(Vector2 pos){
        workspace.x = Mathf.Clamp(pos.x, cameraClamp.xMin, cameraClamp.xMax);
        workspace.y = Mathf.Clamp(pos.y, cameraClamp.yMin, cameraClamp.yMax);
        workspace.z = defaultCamZ;

        cam.transform.position = workspace;
    }

    void LateUpdate()
    {
        if(IsCameraFollowing){
            doCameraFollowing();
        }
    }

    void doCameraFollowing(){
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


    void OnDrawGizmos(){
        if(isDebug){
            var rect = GameObject.Find("/Scenes").transform.Find(sceneCode.ToString()).GetComponent<SceneCodeUtil>().CameraClamp;
            Gizmos.DrawLine(new Vector2(rect.xMin - viewportWorldSize.x/2, rect.yMin - viewportWorldSize.y/2), 
                new Vector2(rect.xMin - viewportWorldSize.x/2, rect.yMax + viewportWorldSize.y/2));
            // top border
            Gizmos.DrawLine(new Vector2(rect.xMin - viewportWorldSize.x/2, rect.yMax + viewportWorldSize.y/2), 
                new Vector2(rect.xMax + viewportWorldSize.x/2, rect.yMax + viewportWorldSize.y/2));
            // right border
            Gizmos.DrawLine(new Vector2(rect.xMax + viewportWorldSize.x/2, rect.yMin - viewportWorldSize.y/2), 
                new Vector2(rect.xMax + viewportWorldSize.x/2, rect.yMax + viewportWorldSize.y/2));
            // buttom border
            Gizmos.DrawLine(new Vector2(rect.xMin - viewportWorldSize.x/2, rect.yMin - viewportWorldSize.y/2), 
                new Vector2(rect.xMax + viewportWorldSize.x/2, rect.yMin -  viewportWorldSize.y/2));
        }
        else{
            // left border
            Gizmos.DrawLine(new Vector2(cameraClamp.xMin - viewportWorldSize.x/2, cameraClamp.yMin - viewportWorldSize.y/2), 
                new Vector2(cameraClamp.xMin - viewportWorldSize.x/2, cameraClamp.yMax + viewportWorldSize.y/2));
            // top border
            Gizmos.DrawLine(new Vector2(cameraClamp.xMin - viewportWorldSize.x/2, cameraClamp.yMax + viewportWorldSize.y/2), 
                new Vector2(cameraClamp.xMax + viewportWorldSize.x/2, cameraClamp.yMax + viewportWorldSize.y/2));
            // right border
            Gizmos.DrawLine(new Vector2(cameraClamp.xMax + viewportWorldSize.x/2, cameraClamp.yMin - viewportWorldSize.y/2), 
                new Vector2(cameraClamp.xMax + viewportWorldSize.x/2, cameraClamp.yMax + viewportWorldSize.y/2));
            // buttom border
            Gizmos.DrawLine(new Vector2(cameraClamp.xMin - viewportWorldSize.x/2, cameraClamp.yMin - viewportWorldSize.y/2), 
                new Vector2(cameraClamp.xMax + viewportWorldSize.x/2, cameraClamp.yMin -  viewportWorldSize.y/2));
        }

    }
}
