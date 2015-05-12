using UnityEngine;
using System.Collections;

public class LockScreen : MonoBehaviour
{

    private MouseLook ml;
    private MouseLook mltwo;
    private CharacterMotor cm;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ml = player.GetComponent<MouseLook>();
        mltwo = Camera.main.GetComponent<MouseLook>();
        cm = player.GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LockAllMotors(bool inUI)
    {
        ml.enabled = !inUI;
        mltwo.enabled = !inUI;
        cm.enabled = !inUI;
        Cursor.visible = inUI;
    }
}
