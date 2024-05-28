using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    AimController aimController;

    WiimoteFacade wiimoteFacade;

    [SerializeField] GameObject hookPrefab;

    [SerializeField] float shootCooldown;

    bool canShoot = true;

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
        Vector2 ir = aimController.GetIR();

        Vector3 viewportPos = Camera.main.ViewportToWorldPoint(new Vector3(ir.x, ir.y, Camera.main.nearClipPlane));
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportPos);

        Debug.Log("IR Vector: " + ir);
        Debug.Log("World Position: " + worldPos);

        Vector3 dir = viewportPos - Camera.main.transform.position;
        dir.Normalize();

        RaycastHit hitInfo;

        Physics.Raycast(Camera.main.transform.position, dir * 10, out hitInfo) ;

        Debug.DrawRay(Camera.main.transform.position, dir * 10, Color.magenta, 1);

        Instantiate(hookPrefab, Camera.main.transform.position, Quaternion.LookRotation(dir)).GetComponent<HookController>();

        canShoot = false;
        
        yield return new WaitForSeconds(shootCooldown);
        
        canShoot = true;
    }

}
