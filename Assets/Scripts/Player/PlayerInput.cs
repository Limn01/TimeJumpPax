using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class PlayerInput : MonoBehaviour
{
    Player player;

    public bool usingJosticks = false;
    public bool usingDpad;
    
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            Vector2 dPadInput = new Vector2(Input.GetAxisRaw("DpadHorizontal"), Input.GetAxisRaw("DpadVertical"));
            player.SetDirectionalInput(dPadInput);
        }
        else
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.OnJumpInputDown();
        }
        if (Input.GetButtonUp("Jump"))
        {
            player.OnJumpInputUp();
        }
    }
}
