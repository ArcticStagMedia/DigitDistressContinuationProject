using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour
{

    public Camera mainCamera;
    public GameObject player;
    private MouseLook mL;
    private MouseLook mLTwo;
    private CharacterMotor cM;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;       
        mL = player.GetComponent<MouseLook>();
        mLTwo = mainCamera.GetComponent<MouseLook>();
        cM = player.GetComponent<CharacterMotor>();
    }

    public void MovementSwitch(bool inUI)
    {
        mL.enabled = !inUI;
        mLTwo.enabled = !inUI;
        cM.enabled = !inUI;
        SwitchLockState(!inUI);
    }

    public void SetLookDirection(Quaternion lookDirection)
    {
        mL.transform.rotation = lookDirection;
        mLTwo.transform.rotation = lookDirection;
    }

    private void SwitchLockState(bool inUI)
    {
        if (!inUI)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }else if (inUI)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }
}
