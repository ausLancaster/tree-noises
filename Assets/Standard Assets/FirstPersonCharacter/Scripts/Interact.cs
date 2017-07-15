using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interact : MonoBehaviour {

    public GameObject touch;
    public GameObject firstPersonCharacter;

    float reach = 3.0f;
    bool canInteract = false;
    bool insideCollider = false;
    GameObject receiver = null;

    private void Awake()
    {
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reach)
            && hit.transform.gameObject.tag == "Interactable")
        {
            enableInteract(hit.transform.gameObject);
        }
        else if (insideCollider == false)
        {
            disableInteract();
        }

        if (canInteract && Input.GetButtonDown("Interact"))
        {
            if (receiver)
            {
                PlaySound(receiver);
            }
            else
            {
                throw new UnassignedReferenceException();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            insideCollider = true;
            enableInteract(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            insideCollider = false;
            disableInteract();
        }
    }

    private void enableInteract(GameObject gameObject)
    {
        canInteract = true;
        touch.SetActive(true);
        receiver = gameObject;
    }

    private void disableInteract()
    {
        canInteract = false;
        touch.SetActive(false);
        receiver = null;
    }

    private void PlaySound(GameObject receiver)
    {
        AudioSource audio = receiver.GetComponent<AudioSource>();

        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
