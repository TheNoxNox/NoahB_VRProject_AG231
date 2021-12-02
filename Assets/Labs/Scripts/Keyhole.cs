using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    public DoorManager doormgr;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Key")
        {
            //Destroy(other);
            doormgr.KeyPlaced();
            Debug.Log("Key Placed");
        }
    }
}
