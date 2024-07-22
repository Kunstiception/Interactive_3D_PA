using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GazeController : MonoBehaviour
{
    public float maxDistance = 2;

    public TextMeshProUGUI contextLabel;

    private InteractableObject currentGazeObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 origin = Camera.main.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(origin, forward, out hit, maxDistance) && hit.collider.gameObject.GetComponent<InteractableObject>() != null)
        {
            Debug.DrawRay(origin, forward * hit.distance, Color.green);

            currentGazeObject = hit.collider.gameObject.GetComponent<InteractableObject>();

            contextLabel.text = currentGazeObject.commandoText;
        }
        else
        {
            Debug.DrawRay(origin, forward * maxDistance, Color.red);

            contextLabel.text = "";
            currentGazeObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentGazeObject != null)
        {
            currentGazeObject.TriggerInteraction();
        }
    }
}
