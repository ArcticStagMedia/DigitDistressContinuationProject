using UnityEngine;
using System.Collections;

public class ClipboardRotationScript : MonoBehaviour
{
    public Quaternion startRotation;
    public Quaternion endRotation;
    public Transform startPosition;
    public Transform endPosition;

    bool canLerp;
    // Use this for initialization
    void Start()
    {
        canLerp = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
