using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public GameObject firePrefab;
    public GameObject owner;
    public bool used;
    public EnumState.State causedState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (owner && used)
        {
            Debug.Log(other.name);
            Transform topParent = other.transform.root;
            if (topParent.gameObject == owner) return;
            if (topParent.CompareTag("Player"))
            {
                Debug.Log(topParent.name + " " + causedState.ToString());
                topParent.GetComponent<PlayerController>().SetState(causedState);
                
                Destroy(this.gameObject);
            }
        }
        else if (owner)
        {
                return;
            }
        else
        {
            Debug.Log(other.gameObject.name);
            Transform topParent = other.transform.root;
            if (topParent.CompareTag("Player"))
            {
                Debug.Log("Player:" + topParent.gameObject.name + " get " + this.name);
                GameObject hand = FindChildWithTag(topParent.gameObject, "Hand");
                //GameObject fireInstance = Instantiate(firePrefab, hand.transform.position, hand.transform.rotation);
                this.transform.position = hand.transform.position;
                this.transform.SetParent(hand.transform);
                owner = topParent.gameObject;
                owner.GetComponent<PlayerController>().SetElement(this.gameObject);
                used = false;
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
           
            
            //Destroy(this.gameObject);
        }

    }


   

    public void SetUsed()
    {
        used = true;
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        for (int i = 0; i < t.childCount; i++)
        {
            Transform child = t.GetChild(i);
            if (child.tag == tag)
            {
                return child.gameObject;
            }
            GameObject foundInChildren = FindChildWithTag(child.gameObject, tag);
            if (foundInChildren != null)
            {
                return foundInChildren;
            }
        }
        return null;
    }
}
