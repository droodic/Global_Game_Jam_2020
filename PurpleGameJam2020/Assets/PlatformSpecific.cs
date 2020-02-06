using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpecific : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_WEBGL
        this.gameObject.SetActive(false);
#endif
    }
}
