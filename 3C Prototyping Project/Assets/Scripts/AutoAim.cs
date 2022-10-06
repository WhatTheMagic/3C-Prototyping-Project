using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{

    public Transform aimPosition;
    public GameObject currentGun;
    GameObject currentTarget;
    public float distance = 10f;

    bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        currentGun.transform.position = aimPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();

        if (isAiming)
            AutoAiming();
    }

    private void CheckTarget()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if(hit.transform.gameObject.tag == "Enemy")
            {
                if (!isAiming)
                    Debug.Log("Target found!");

                currentTarget = hit.transform.gameObject;
                isAiming = true;
            }
            else
            {
                currentTarget = null;
                isAiming = false;
            }
        }
    }
    private void AutoAiming()
    {
        currentGun.transform.LookAt(currentTarget.transform);

    }
}
