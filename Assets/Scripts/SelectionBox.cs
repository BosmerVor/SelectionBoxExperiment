using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{

    Vector3[] coord = new Vector3[2];
    bool drawing = false;
    Rect selectionBox;

    public Texture2D boxTexture;
    GUIStyle boxStyle = null;
    GameObject[] characters;

    void Start()
    {
        coord[0] = new Vector3(0f, 0f, 0f);
        coord[1] = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        GetCoordinates();
        if(drawing){
            selectionBox = DrawSelectionBox();
            Overlaps();
        }
    }

    void GetCoordinates(){

        if(Input.GetMouseButtonDown(0)){
            drawing = true;
            coord[0] = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            coord[0].y = Screen.height - coord[0].y;
            coord[0].z = 0f;
        }

        if(Input.GetMouseButton(0)){
            coord[1] = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            coord[1].y = Screen.height - coord[1].y;
            coord[1].z = 0f;
        }

        if(Input.GetMouseButtonUp(0)){
            coord[0] = new Vector3(0f, 0f, 0f);
            coord[1] = new Vector3(0f, 0f, 0f);
            drawing = false;
        }

    }

    Rect DrawSelectionBox(){
        float xmin, ymin, xmax, ymax;
        xmin = (coord[0].x < coord[1].x) ? coord[0].x : coord[1].x;
        xmax = (coord[0].x > coord[1].x) ? coord[0].x : coord[1].x;
        ymin = (coord[0].y < coord[1].y) ? coord[0].y : coord[1].y;
        ymax = (coord[0].y > coord[1].y) ? coord[0].y : coord[1].y;
        Rect selectionRect = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        return selectionRect;
    }

    void OnGUI(){
        if(boxStyle == null){
            boxStyle = new GUIStyle("box");
            boxStyle.normal.background = boxTexture;
            boxStyle.border = new RectOffset(1, 1, 1, 1);
        }
        if(drawing){
            // boxStyle.Draw(selectionBox, false, false, false, false);
            GUI.Box(selectionBox, "", boxStyle);
        }
    }

    void Overlaps(){
        characters = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject character in characters){
            //Debug.Log(selectionBox);
            if(Camera.main.WorldToScreenPoint(character.transform.position).x > selectionBox.x &&
               Camera.main.WorldToScreenPoint(character.transform.position).x < selectionBox.x + selectionBox.width &&
               Screen.height - Camera.main.WorldToScreenPoint(character.transform.position).y >  selectionBox.y &&
               Screen.height - Camera.main.WorldToScreenPoint(character.transform.position).y < selectionBox.y + selectionBox.height){
                character.GetComponent<Selectable>().selected = true;
            }
            else{
                character.GetComponent<Selectable>().selected = false;
            }
        }

    }
}
