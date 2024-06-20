using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public enum WiiMoteButton
{
    None,
    B,
    A,
    One,
    Two,
    Plus,
    Minus,
    Home,
    DUp,
    DDown,
    DLeft,
    DRight
}

public class WiimoteFacade : MonoBehaviour
{
    Wiimote wiimote;

    private void Awake()
    {
        InitWiimotes();
    }

    // Start is called before the first frame update
    void Start()
    {
        bool led1=RandomBoolean(), led2 = RandomBoolean(), led3 = RandomBoolean(), led4 = RandomBoolean();
        wiimote.SendPlayerLED(led1, led2, led3, led4);
        wiimote.SetupIRCamera(IRDataType.EXTENDED);
        wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_IR12);
    }

    // Update is called once per frame
    void Update()
    {
        if(wiimote != null) { 
            wiimote.ReadWiimoteData();
        }
    }

    void InitWiimotes()
    {
        WiimoteManager.FindWiimotes(); // Poll native bluetooth drivers to find Wiimotes
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            wiimote = remote;
        }
    }
    void FinishedWithWiimotes()
    {
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
    }

    bool RandomBoolean() {
        return (Random.Range(0, 2) == 1);
    }

    public Vector3 GetAccelVector() 
    {
        //wiimote.Accel.GetCalibratedAccelData();
        var array = wiimote.Accel.accel;
        int x = array[0] - 512;
        int y = array[2] - 620;
        int z = array[1] - 512;
        return new Vector3(x, y, z);
    }

    public Vector2 GetIRVector() 
    {
        var point = wiimote.Ir.GetIRMidpoint();
        return new Vector2(point[0], point[1]);
    }

    public WiiMoteButton GetPressedButton()
    {
        if (wiimote.Button.b) return WiiMoteButton.B;
        if (wiimote.Button.a) return WiiMoteButton.A;
        if (wiimote.Button.one) return WiiMoteButton.One;
        if (wiimote.Button.two) return WiiMoteButton.Two;
        if (wiimote.Button.plus) return WiiMoteButton.Plus;
        if (wiimote.Button.minus) return WiiMoteButton.Minus;
        if (wiimote.Button.home) return WiiMoteButton.Home;
        if (wiimote.Button.d_up) return WiiMoteButton.DUp;
        if (wiimote.Button.d_down) return WiiMoteButton.DDown;
        if (wiimote.Button.d_left) return WiiMoteButton.DLeft;
        if (wiimote.Button.d_right) return WiiMoteButton.DRight;

        return WiiMoteButton.None; // Nenhum botão pressionado
    }

    private void OnApplicationQuit()
    {
        FinishedWithWiimotes();
    }

}
