using System;
using UnityEngine;
using UnityEngine.AI;

public class Billboarding : MonoBehaviour
{
    [SerializeField] bool freezeXZ = true;
    void LateUpdate()
    {
       if(freezeXZ){ //freezes XZ
             transform.rotation = Quaternion.Euler(0f,Camera.main.transform.rotation.eulerAngles.y,0f);
       }else{
        transform.rotation = Camera.main.transform.rotation;
       }
    }
}
