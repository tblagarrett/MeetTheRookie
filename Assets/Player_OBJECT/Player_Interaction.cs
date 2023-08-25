using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player_Interaction : MonoBehaviour
{
    public GameObject interactIcon;
    public GameObject currInteractable;
    public List<GameObject> allInteractables;
    public bool showIcon;

    public void Start()
    {
        interactIcon.SetActive(false);
        allInteractables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Interactable"));
    }

    public void Update()
    {
        currInteractable = GetNearestInteractable();
        if(showIcon){
            GetComponent<Player_Interaction>().interactIcon.transform.position = IconMath();
        }
    }

    public Vector3 IconMath(){
            Vector3 iconPosition = GetComponentInChildren<SpriteRenderer>().transform.position;
            iconPosition.y +=GetComponentInChildren<SpriteRenderer>().bounds.size.y/2+1;
            iconPosition.x +=GetComponentInChildren<SpriteRenderer>().bounds.size.x/2;

            return iconPosition;
    }


    #region <<INTERACTIONS>>>
    public void InteractWithObject()
    {
        currInteractable.GetComponent<Interactable>().BaseInteract();
    }
    public void InteractionCheck(){
        if(currInteractable!= null && showIcon){
            InteractWithObject();
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
        float shortestDistance = float.MaxValue;
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
