using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposedCableVisibilityHandler : MonoBehaviour
{
    public void OnInteract()
    {
        gameObject.SetActive(false);
    }
}
