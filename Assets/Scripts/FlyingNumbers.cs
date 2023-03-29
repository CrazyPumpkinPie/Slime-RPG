using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingNumbers : MonoBehaviour
{
    public void DestroyParentObject()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }
}
