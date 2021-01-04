using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCameraCotroller : MonoBehaviour
{
    public GameObject target;
    public Camera cam;

    public void SelectTarget(GameObject _target) 
    {
        if (!cam.gameObject.activeSelf) cam.gameObject.SetActive(true);
        if (target != null) TargetToNull();
        target = _target;
        target.GetComponent<Clickeable>().IsTarget();
    }

    public void TargetToNull() 
    {
        if (target == null) return;
        target.GetComponent<Clickeable>().IsNotTarget();
        target = null;
    }

    public void Update()
    {
        if(target!=null)
            cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, cam.transform.position.z);
    }

}
