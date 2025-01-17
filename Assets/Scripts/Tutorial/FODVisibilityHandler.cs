using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FODVisibilityHandler : MonoBehaviour
{
    public void OnInteract()
    {
        gameObject.SetActive(false);
    }
}
