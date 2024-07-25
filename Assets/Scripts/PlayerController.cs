using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EnumState.State curState;
    private GameObject element;
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private Rigidbody hip;
    private Vector3 direction;
    private int punchLayerIndex;
    public float useDelay = 0.5f;

    public float useForce = 10000f;
    // Start is called before the first frame update
    void Start()
    {
        curState = EnumState.State.Normal;
        punchLayerIndex = targetAnimator.GetLayerIndex("PunchLayer");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }
        
        if (Input.GetKeyDown(KeyCode.J) && element)
        {
            this.targetAnimator.Play("Punch", punchLayerIndex);
            StartCoroutine(UseElementCoroutine());
        }   
    }

    IEnumerator UseElementCoroutine()
    {
        yield return new WaitForSeconds(useDelay);

        element.transform.SetParent(null);

        element.GetComponent<Element>().SetUsed();
        element.GetComponent<CapsuleCollider>().enabled = true;
        //Debug.Log(direction);
        element.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log(direction);
        Debug.Log(useForce);
        Debug.Log(direction * useForce);
        element.GetComponent<Rigidbody>().AddForce(direction * useForce, ForceMode.Force);
        element = null;
    }



    public void SetState(EnumState.State state)
    {
        curState = state;
    }

    public void SetElement(GameObject element)
    {
        this.element = element;
    }

    
}
