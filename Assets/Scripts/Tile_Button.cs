using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Button : MonoBehaviour
{
    bool isPushed = false;

    public Vector3 startPos;
    public int count = 4;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TestMousePosition() == false)
        {
            if(isPushed == true)
            {
                isPushed = false;
                transform.position = startPos;
            }
            
            return;
        }

        if(Input.GetMouseButtonDown(0) == true)
        {
            isPushed = true;
            transform.position = startPos + Vector3.down * 0.1f;
        }
        else if(Input.GetMouseButtonUp(0) == true && isPushed == true)
        {
            isPushed = false;
            transform.position = startPos;

            OnClick();
        }
    }

    protected virtual void OnClick()
    {
        if (GameMaster.Tile_AddList(gameObject) == false)
            return;
        count--;
        if (count == 0)
        {
            transform.position = new Vector3(0, 30, 0);
        }
    }
    
    private bool TestMousePosition()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mp.x > transform.position.x - 0.5f &&
            mp.x < transform.position.x + 0.5f &&
            mp.y > transform.position.y - 0.6f &&
            mp.y < transform.position.y + 0.6f)
            return true;

        return false;
    }
}
