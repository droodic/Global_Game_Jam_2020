using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    [SerializeField] private bool _hideMouse;
    // Start is called before the first frame update
    void Start()
    {
        if (_hideMouse)
        {
            Cursor.visible = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
