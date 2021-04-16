using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarGeneratorScript : MonoBehaviour
{
    public GameObject unit;
    public string InitialGrammar;
    public int iteration;
    private string grammar;
    private Vector3 InitialRotation;
    public int unitRotation;
    GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        grammar = InitialGrammar;
        InitialRotation = new Vector3(90,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeleteAll();
            Generate();
        }
    }

    void Generate()
    {
        grammar = InitialGrammar;
        for (int i=0;i<iteration;i++)
        {
            string nextGrammar = "";
            for (int j = 0; j < grammar.Length; j++)
            {
                if (grammar[i] == 'F')
                {
                    nextGrammar = nextGrammar + grammar;
                }
                else
                {
                    nextGrammar = nextGrammar + grammar[i];
                }
            }
            grammar = nextGrammar;
        }

        for (int i = 0; i < grammar.Length; i++)
        {
            if (grammar[i] == 'F')
            {
                transform.position = transform.position + 
                    new Vector3(unit.transform.localScale.x* transform.forward.x, unit.transform.localScale.y* transform.forward.y, unit.transform.localScale.z* transform.forward.z);
                GameObject.Instantiate(unit, transform.position, unit.transform.rotation);
            }
            if (grammar[i] == 'w')
            {
                //transform.position = transform.position + 
                    //new Vector3(unit.transform.localScale.x * transform.forward.x /2, unit.transform.localScale.y * transform.forward.y /2, unit.transform.localScale.z * transform.forward.z /2);
                transform.Rotate(unitRotation, 0, 0);
                unit.transform.Rotate(unitRotation, 0, 0);
            }
            if (grammar[i] == 's')
            {
                transform.Rotate(-unitRotation, 0, 0);
                unit.transform.Rotate(-unitRotation, 0, 0);
            }
            if (grammar[i] == 'a')
            {
                transform.Rotate(0,unitRotation, 0);
                unit.transform.Rotate(0,unitRotation, 0);
            }
            if (grammar[i] == 'd')
            {
                transform.Rotate(0,-unitRotation, 0);
                unit.transform.Rotate(0,-unitRotation,0);
            }
            if (grammar[i] == 'q')
            {
                transform.Rotate(0, 0, unitRotation);
                unit.transform.Rotate(0, 0,unitRotation);
            }
            if (grammar[i] == 'e')
            {
                transform.Rotate(0, 0, -unitRotation);
                unit.transform.Rotate(0, 0, -unitRotation);
            }
        }
    }


    public void DeleteAll()
    {
        objects = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject o in objects)
        {
            Destroy(o);
        }
        transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        unit.transform.localEulerAngles = InitialRotation;
        //currentRotation = transform.rotation;
    }
}
