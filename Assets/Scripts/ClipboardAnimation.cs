using UnityEngine;
using System.Collections;

public class ClipboardAnimation : MonoBehaviour
{
    private Animator anim;
    private bool clipboardUp;
    private int clipboardUpHash;
    public Canvas clipboardCanvas;
    private LockScreen lockScreen;
    private GameObject player;
    private Camera mainCamera;
    private Quaternion cameraLookAngle = new Quaternion(0, 0, 0, 0);

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        clipboardUp = false;
        anim = this.gameObject.GetComponent<Animator>();
        clipboardUpHash = Animator.StringToHash("clipboardUp");
        clipboardCanvas = GetComponent<Canvas>();
        lockScreen = player.GetComponent<LockScreen>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            clipboardUp = !clipboardUp;
            anim.SetBool(clipboardUpHash, clipboardUp);
            if (clipboardUp)
            {
                lockScreen.LockAllMotors(true);
                mainCamera.transform.rotation = cameraLookAngle;
                Cursor.lockState = CursorLockMode.None;
                clipboardCanvas.enabled = true;

            }
            else if (!clipboardUp)
            {
                lockScreen.LockAllMotors(false);
                Cursor.lockState = CursorLockMode.Locked;
                clipboardCanvas.enabled = false;
            }
        }
    }
}
