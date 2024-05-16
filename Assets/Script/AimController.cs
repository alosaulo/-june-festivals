using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimController : MonoBehaviour
{
    [SerializeField] RectTransform rectCanvas;
    [SerializeField] Image Aim;
    Camera mCamera;
    WiimoteFacade wiimoteFacade;

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        wiimoteFacade = GetComponent<WiimoteFacade>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moteIR = mCamera.ViewportToScreenPoint(wiimoteFacade.GetIRVector());
        MoveAim(moteIR);
    }

    void MoveAim(Vector2 IrVector) 
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectCanvas, IrVector, null, out newPos);
        Aim.rectTransform.localPosition = newPos * -1;
    }

    public Vector3 GetAimTransformPosition ()
    {
        return Aim.transform.position;
    }

}
