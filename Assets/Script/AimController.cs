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
    Vector2 newPos;
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectCanvas, IrVector, null, out newPos);
        newPos *= -1;
        Aim.rectTransform.localPosition = newPos;
    }

    public Vector2 GetIR() 
    {
        Vector2 ir = wiimoteFacade.GetIRVector();

        ir.x = 1 - ir.x;
        ir.y = 1 - ir.y;

        return ir;
    }

    public Vector3 GetAimTransformPosition ()
    {
        Vector3 aimWorldPos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectCanvas, newPos, Camera.main, out aimWorldPos))
        {
            return aimWorldPos;
        }
        else 
        {
            return Camera.main.transform.position;
        }
    }

}
