using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnClick : MonoBehaviour {
    private void OnMouseUp()
    {
        Destroy(gameObject);
    }
}
