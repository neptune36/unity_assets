using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookTheMouse : NetworkBehaviour
{
    public Transform body;

    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        /*
        Vector3 upAxis = Vector3.forward;
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = body.position.z;
        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        body.LookAt(mouseWorldSpace, upAxis);
        body.eulerAngles = new Vector3(0, 0, -body.eulerAngles.z);
        Input.GetJoystickNames()
        */

        if (!Input.GetJoystickNames()[0].Equals(""))
        {
            Vector3 dir = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            body.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(body.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            body.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }


    }
}
