using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect
{
    public static Vector2 PixelPerfectClamp(Vector2 position, float pixelsPerUnit)
    {
        Vector2 newPos = new Vector2(
            Mathf.RoundToInt(position.x * pixelsPerUnit) / pixelsPerUnit,
            Mathf.RoundToInt(position.y * pixelsPerUnit) / pixelsPerUnit
            );

        return newPos;
    }

    public static Vector2 PixelPerfectClampFloor(Vector2 position, float pixelsPerUnit)
    {
        Vector2 newPos = new Vector2(
            Mathf.FloorToInt(position.x * pixelsPerUnit) / pixelsPerUnit,
            Mathf.FloorToInt(position.y * pixelsPerUnit) / pixelsPerUnit
            );

        return newPos;
    }

    public static Vector2 PixelPerfectClampCeil(Vector2 position, float pixelsPerUnit)
    {
        Vector2 newPos = new Vector2(
            Mathf.CeilToInt(position.x * pixelsPerUnit) / pixelsPerUnit,
            Mathf.CeilToInt(position.y * pixelsPerUnit) / pixelsPerUnit
            );

        return newPos;
    }
}
