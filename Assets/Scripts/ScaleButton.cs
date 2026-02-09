using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleButton : MonoBehaviour
{
    public void MouseEnter()
    {
        transform.localScale = new Vector3(1.2f, 1.2f);
    }

    public void MouseExit()
    {
        transform.localScale = new Vector3(1f, 1f);
    }
}
