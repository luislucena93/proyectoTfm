using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraManager : MonoBehaviour
{
    [SerializeField] private Camera P1Camera;
    [SerializeField] private Camera P2Camera;
    [SerializeField] private Camera P1P2Camera;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float maxRange = 15f;
    [SerializeField] private Image screenSplitter;
    // Start is called before the first frame update
    void Start()
    {
        P1Camera.enabled = false;
        P2Camera.enabled = false;
        screenSplitter.enabled = false;
        P1P2Camera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(player1.position, player2.position - player1.position);
        if (Physics.Raycast(ray, out hit, maxRange))
        {
            if (hit.transform == player2)
            {
                P1Camera.enabled = false;
                P2Camera.enabled = false;
                screenSplitter.enabled = false;
                P1P2Camera.enabled = true;
            } else
            {
                P1Camera.enabled = true;
                P2Camera.enabled = true;
                screenSplitter.enabled = true;
                P1P2Camera.enabled = false;
            }
        } else
        {
            P1Camera.enabled = true;
            P2Camera.enabled = true;
            screenSplitter.enabled = true;
            P1P2Camera.enabled = false;
        }
    }
}
