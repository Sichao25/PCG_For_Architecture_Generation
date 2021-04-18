using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarApprochScript : MonoBehaviour
{
    public GameObject unitA;
    public GameObject unitB;
    public GameObject unitC;
    public GameObject RoofA;
    public GameObject Window;
    public string BaseGrammar;
    public float stepRotation;
    public int maxFloor;
    public int minFloor;
    public int branchMaxFloor;
    public int branchMinFloor;
    public float step;
    public int domeNum;
    //private Quaternion currentRotation;
    GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
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
        List<Vector3> basePosition = new List<Vector3>();
        List<Vector3> baseRotation = new List<Vector3>();
        List<bool> onBranch = new List<bool>();
        Vector3 savedPosition = transform.position;
        Vector3 savedRotation = transform.eulerAngles;
        int count = 0;
        bool branch = false;
        int dome = domeNum;
        //float step = unitA.transform.localScale.x;
        //basePosition.Add(transform.position);
        //baseRotation.Add(transform.eulerAngles);
        for (int i = 0; i < BaseGrammar.Length; i++)
        {
            if (BaseGrammar[i] == 'F')
            {
                transform.position = transform.position +
                    new Vector3(step * transform.forward.x, step * transform.forward.y, step * transform.forward.z);
            }
            if (BaseGrammar[i] == 'P')
            {
                basePosition.Add(transform.position);
                baseRotation.Add(transform.eulerAngles);
                onBranch.Add(branch);
                count++;
            }
            if (BaseGrammar[i] == '+')
            {
                transform.Rotate(0, stepRotation, 0);
            }
            if (BaseGrammar[i] == '-')
            {
                transform.Rotate(0, -stepRotation, 0);
            }
            if (BaseGrammar[i] == '[')
            {
                branch = true;
                savedPosition = transform.position;
                savedRotation = transform.eulerAngles;
            }
            if (BaseGrammar[i] == ']')
            {
                branch = false;
                transform.position = savedPosition;
                transform.eulerAngles = savedRotation;
            }
        }
        transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        for (int i=0;i<count;i++)
        {
            Vector3 currentPosition = basePosition[i];
            Vector3 currentRotation = baseRotation[i];
            int blockID = Random.Range(1, 4);
            print(blockID);
            if (blockID == 1)
            {
                GenerateBlock(currentPosition, currentRotation, unitA, onBranch[i]);
            }
            else if (blockID == 2)
            {
                GenerateBlock(currentPosition, currentRotation, unitB, onBranch[i]);
            }
            else
            {
                GenerateBlock(currentPosition, currentRotation, unitC, onBranch[i]);
            }
        }
    }

    

    void GenerateBlock(Vector3 targetPosition, Vector3 targetRotation, GameObject unit, bool onBranch)
    {
        int floor = Random.Range(minFloor, maxFloor + 1);
        if (onBranch)
        {
            floor = Random.Range(branchMinFloor, branchMaxFloor + 1);
        }
        GameObject Block = GameObject.Instantiate(unit, new Vector3(targetPosition.x, unit.transform.localScale.y * floor/2,targetPosition.z), unit.transform.rotation);
        float height = Block.transform.localScale.y / 2;
        Block.transform.localScale = new Vector3(Block.transform.localScale.x, Block.transform.localScale.y * floor, Block.transform.localScale.z);
       // Block.transform.position = new Vector3(Block.transform.position.x, Block.transform.localScale.y / 2, Block.transform.position.z);
        //Block.transform.position += new Vector3(0, Block.transform.localScale.y / 2, 0);
        //float y = Block.transform.localScale.y / 2 - Block.transform.position.y;
        //Block.transform.position += new Vector3(0, y, 0);
        //Block.transform.SetPositionAndRotation(new Vector3(Block.transform.position.x, Block.transform.localScale.y / 2, Block.transform.position.x), Quaternion.Euler(new Vector3(0, 0, 0)));


        Block.transform.Rotate(targetRotation);


        //GenerateRoof(targetPosition,targetRotation,RoofA,floor);



        GameObject Roof = GameObject.Instantiate(RoofA, new Vector3(Block.transform.position.x, Block.transform.localScale.y, Block.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
        Roof.transform.Rotate(targetRotation);
        Roof.transform.position = new Vector3(Block.transform.position.x, Block.transform.localScale.y, Block.transform.position.z);
        
        if (Random.Range(0, 7) == 1)
        {
            GameObject window = GameObject.Instantiate(Window, new Vector3(Block.transform.position.x, Block.transform.localScale.y, Block.transform.position.z), Quaternion.Euler(targetRotation));
        }
    }

    void GenerateCylinderBlock(Vector3 targetPosition, Vector3 targetRotation, GameObject unit)
    {
        int floor = Random.Range(1, maxFloor);
        GameObject Block = GameObject.Instantiate(unit, new Vector3(targetPosition.x, unit.transform.localScale.y * floor, targetPosition.z), unit.transform.rotation);
        float height = Block.transform.localScale.y / 2;
        Block.transform.localScale = new Vector3(Block.transform.localScale.x, Block.transform.localScale.y * floor , Block.transform.localScale.z);
        
        Block.transform.Rotate(targetRotation);



        GameObject Roof = GameObject.Instantiate(RoofA, new Vector3(targetPosition.x + 7.5f, Block.transform.localScale.y, targetPosition.z - 7.5f), Quaternion.Euler(new Vector3(0, 0, 0)));
        //Roof.transform.SetPositionAndRotation(new Vector3(Block.transform.position.x + 7.5f, Block.transform.localScale.y, Block.transform.position.z - 7.5f), Quaternion.Euler(targetRotation));
        Roof.transform.Rotate(targetRotation);
        Roof.transform.position = new Vector3(Block.transform.position.x, Block.transform.localScale.y * 2, Block.transform.position.z);

    }

    void GenerateRoof(Vector3 targetPosition, Vector3 targetRotation, GameObject unit,int floor)
    {
        GameObject Roof = GameObject.Instantiate(unit, new Vector3(targetPosition.x, targetPosition.y, targetPosition.z), unit.transform.rotation);
        Roof.transform.Rotate(targetRotation);
    }


    public void DeleteAll()
    {
        objects = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject o in objects)
        {
            Destroy(o);
        }
        transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        //unit.transform.localEulerAngles = InitialRotation;
        //currentRotation = transform.rotation;
    }
}
