using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    AimController aimController;

    WiimoteFacade wiimoteFacade;

    [SerializeField] GameObject hookPrefab;

    [SerializeField] float shootCooldown;

    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        aimController = GetComponent<AimController>();
        wiimoteFacade = GetComponent<WiimoteFacade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wiimoteFacade.GetPressedButton() == WiiMoteButton.B && canShoot) 
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() 
    {
        Instantiate(hookPrefab,aimController.GetAimTransformPosition(), hookPrefab.transform.rotation);
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

}
