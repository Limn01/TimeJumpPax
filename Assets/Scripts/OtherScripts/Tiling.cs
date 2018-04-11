using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offsetX = 2;
    public int offsetY = 2;

    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool hasATopBuddy = false;
    public bool hasADownBuddy = false;

    public bool reverseScale = false;

    float spriteWidth = 0f;
    float spriteHeight = 0f;
    Camera cam;
    Transform mytransform;
    //Transform newHorBuddy;
    //Transform newVertBuddy;

    private void Awake()
    {
        cam = Camera.main;
        mytransform = transform;
    }

    private void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
        spriteHeight = sRenderer.sprite.bounds.size.y;
    }

    private void Update()
    {
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            float edgeVisiblePositionRight = (mytransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (mytransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewHorizontalBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewHorizontalBuddy(-1);
                hasALeftBuddy = true;
            }
        }

        //if (hasADownBuddy == false || hasATopBuddy == false)
        //{
        //    float camVerticalExtend = cam.orthographicSize * Screen.width / Screen.height;
        //
        //    float edgeVisiblePositionDown = (mytransform.position.y + spriteHeight / 2) + camVerticalExtend;
        //    float edgeVisiblePositionUp = (mytransform.position.y + spriteHeight / 2) - camVerticalExtend;
        //
        //    if (cam.transform.position.y >= edgeVisiblePositionUp - offsetY && hasATopBuddy == false)
        //    {
        //        MakeNewVerticalBuddy(1);
        //        hasATopBuddy = true;
        //    }
        //    else if (cam.transform.position.y <= edgeVisiblePositionDown + offsetY && hasADownBuddy == false)
        //    {
        //        MakeNewVerticalBuddy(-1);
        //        hasADownBuddy = true;
        //    }
        //}
    }

    void MakeNewHorizontalBuddy(int rightOrLeft)
    {
        Vector3 newsPos = new Vector3(mytransform.position.x + spriteWidth * rightOrLeft, mytransform.position.y, mytransform.position.z);

        Transform newBuddy = Instantiate(mytransform, newsPos, mytransform.rotation) as Transform;


        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = mytransform.parent;

        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }

    //void MakeNewVerticalBuddy(int upOrDown)
    //{
    //    Vector3 newPos = new Vector3(mytransform.position.x, mytransform.position.y + spriteHeight * upOrDown, mytransform.position.z);
    //   // Transform newBuddy = Instantiate(mytransform, newPos, mytransform.rotation) as Transform;
    //
    //    if (reverseScale == true)
    //    {
    //        newVertBuddy.localScale = new Vector3(newVertBuddy.localScale.x, newVertBuddy.localScale.y * -1, newVertBuddy.localScale.z);
    //    }
    //
    //    newVertBuddy.parent = mytransform.parent;
    //
    //    if (upOrDown > 0)
    //    {
    //        newVertBuddy.GetComponent<Tiling>().hasADownBuddy = true;
    //    }
    //    else
    //    {
    //        newVertBuddy.GetComponent<Tiling>().hasATopBuddy = true;
    //    }
    //}
}
