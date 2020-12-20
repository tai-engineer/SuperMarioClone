using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicUtil
{
    // offset to avoid line cast detect parent object's collider
    public static bool GroundCheck(Vector2 position, float width, float distance, LayerMask layer, float offset = 0f, bool debugDraw = false)
    {
        RaycastHit2D leftHit, centerHit, rightHit;
        Vector2 leftPos, centerPos, rightPos;
        Vector2 leftEndPos, centerEndPos, rightEndPos;

        leftPos   = new Vector2(position.x - width, position.y - offset);
        centerPos = new Vector2(position.x, position.y - offset);
        rightPos  = new Vector2(position.x + width, position.y - offset);

        leftEndPos = new Vector2(leftPos.x, leftPos.y - distance);
        centerEndPos = new Vector2(centerPos.x, centerPos.y - distance);
        rightEndPos = new Vector2(rightPos.x, rightPos.y - distance);

        leftHit = Physics2D.Linecast(leftPos, leftEndPos, layer);
        centerHit = Physics2D.Linecast(centerPos, centerEndPos, layer);
        rightHit = Physics2D.Linecast(rightPos, rightEndPos, layer);

        if(debugDraw)
        {
            Debug.DrawLine(leftPos, leftEndPos, Color.red);
            Debug.DrawLine(centerPos, centerEndPos, Color.red);
            Debug.DrawLine(rightPos, rightEndPos, Color.red);
        }

        return (leftHit.collider != null || centerHit.collider != null ||
            rightHit.collider != null);
    }
    public static bool ObstacleCheck(Vector2 position, bool left, float distance, LayerMask layer, float offset = 0f, bool debugDraw = false)
    {
        RaycastHit2D hit;
        Vector2 pos, endPos;

        pos = position;
        pos.x = left ? position.x - offset : position.x + offset;

        endPos = pos;
        endPos.x = left ? pos.x - distance : pos.x + distance;

        hit = Physics2D.Linecast(pos, endPos, layer);

        if(debugDraw)
        {
            Debug.DrawLine(pos, endPos, Color.red);
        }

        return hit.collider != null;
    }
}
