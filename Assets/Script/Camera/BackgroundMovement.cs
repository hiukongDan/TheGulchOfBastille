using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Transform background;
    public Transform foreground;

    public float margin;

    public float backgroundMoveSpeed = 10f;

    private Transform cam;

    private float backgroundLength, foregroundLength;

    private Vector3 workspace;


    void Start()
    {
        cam = Camera.main.transform;
        backgroundLength = background.GetComponent<SpriteRenderer>().sprite.rect.width;
        foregroundLength = foreground.GetComponent<SpriteRenderer>().sprite.rect.width;

        float camOffsetPercentage = cam.transform.position.x / foregroundLength / 2;
        workspace.Set((-camOffsetPercentage * (backgroundLength / 2 - margin) + cam.transform.position.x), background.position.y, background.position.z);
        background.position = workspace;
    }

    void Update()
    {
        float camOffsetPercentage = cam.transform.position.x / foregroundLength / 2;
        workspace.Set((-camOffsetPercentage * (backgroundLength / 2 - margin) + cam.transform.position.x), background.position.y, background.position.z);

        background.position = Vector3.Lerp(background.position, workspace, Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
    }
}
