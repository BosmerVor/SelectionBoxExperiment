using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool selected;
    float outline;
    // Start is called before the first frame update
    void Start()
    {
        outline = transform.GetComponent<SpriteOutline>().outlineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
            transform.GetComponent<SpriteOutline>().outlineSize = 2;   
        }
        else{
            transform.GetComponent<SpriteOutline>().outlineSize = 0;
        }
    }
}
