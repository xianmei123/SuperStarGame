using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public GameObject firePrefab;

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
        Debug.Log(other.gameObject.name);
        Transform topLevelParent = other.transform.root;
        if (topLevelParent.CompareTag("Player"))
        {
            Debug.Log("Player:" + topLevelParent.gameObject.name + " get " + this.name);
        }
        GameObject hand = FindChildWithTag(topLevelParent.gameObject, "Hand");
        GameObject fireInstance = Instantiate(firePrefab, hand.transform.position, hand.transform.rotation);
        fireInstance.transform.SetParent(hand.transform);
        Destroy(this.gameObject);
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


