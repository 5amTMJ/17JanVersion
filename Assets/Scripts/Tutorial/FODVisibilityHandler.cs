using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FODVisibilityHandler : MonoBehaviour
{
    public GameObject clearEffectPrefab; // Assign a particle/visual effect prefab

    public void OnInteract()
    {
        // Spawn the effect at this object's position/rotation
        if (clearEffectPrefab != null)
        {
            Instantiate(clearEffectPrefab, transform.position, Quaternion.identity);
        }

        // Now disable the FOD object
        gameObject.SetActive(false);
    }
}
