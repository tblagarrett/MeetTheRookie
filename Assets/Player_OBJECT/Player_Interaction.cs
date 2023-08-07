using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : max distance for interaction, if no objects inside max distance then currinteractable is null

[RequireComponent(typeof(BoxCollider2D))]
public class Player_Interaction : MonoBehaviour
{
    public GameObject interactIcon;
    public GameObject currInteractable;
    public List<GameObject> allInteractables;
    public float maxInteractableDistance;

    public void Start()
    {
        interactIcon.SetActive(false);
        allInteractables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Interactable"));
    }

    public void Update()
    {
        currInteractable = GetNearestInteractable();
    }

    #region <<INTERACTIONS>>>
    public void InteractWithObject()
    {
        if (currInteractable != null)
        {
            currInteractable.GetComponent<IInteractable>().Interact();
        }
    }

    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    private GameObject GetNearestInteractable()
    {
        //for TODO just made the shortest distance the max inter distance 
        //because you don't even want to consider objs outside that range
        float shortestDistance = maxInteractableDistance;
        GameObject nearestInteractable = null;

        foreach (GameObject interactable in allInteractables)
        {
            float currentDistance = Vector2.Distance(transform.position, interactable.transform.position);
            if (currentDistance < shortestDistance) 
            {
                shortestDistance = currentDistance;
                nearestInteractable = interactable;
            }
        }

        return nearestInteractable;
    }
    #endregion
}
