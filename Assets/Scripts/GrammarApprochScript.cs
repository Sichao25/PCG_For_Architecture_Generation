using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarApprochScript : MonoBehaviour
{
    //this script is the generator
    public GameObject unitA;
    public float unitHeightA;
    public GameObject unitB;
    public float unitHeightB;
    public GameObject unitC;
    public float unitHeightC;
    public GameObject RoofA;
    public GameObject RoofB;
    public GameObject Window;
    public GameObject DomeRoof;
    public GameObject Tower;
    public string BaseGrammar;
    public float stepRotation;
    public int maxFloor;
    public int minFloor;
    public int branchMaxFloor;
    public int branchMinFloor;
    public float step;
    public int domeNum;
    public bool hasTower;
    public bool hasWindow;
    public bool multipleRoof;
    
    GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            DeleteAll();
            Generate();
        }*/
    }

    public void reGenerate()
    {
        DeleteAll();
        Generate();
    }

    //generate modern office building
    public void reGenerateModern()
    {
        int length = Random.Range(3, 9);
        int branchNum = Random.Range(0, 3);
        string grammar = "";
        for (int i = 0; i < length; i++)
        {
            grammar += "PF";
            if (branchNum > 0 && Random.Range(0f, 1.0f) > 0.5)
            {
                int branchLength = Random.Range(2, 6);
                string branch = "";
                for (int j = 0; j < branchLength; j++)
                {
                    branch += "PF";
                }
                grammar = grammar + "[+" + branch + "]";
                branchNum--;
            }
        }
        //return "PFPFP[+PFPFP]";
        BaseGrammar = grammar;
        DeleteAll();
        Generate();
    }

    //generate Renaissance architecture
    public void reGenerateRenaissance()
    {
        if (Random.Range(0, 3) == 1)
        {
            BaseGrammar = "FP[+P]FPFPFPFPFPFPFPFPFPFPF[+P]PF";
            DeleteAll();
            Generate();
            return;
        }
        string grammar = "FP[+P]FPFPFPFPFPFPFPFPFPFPF[+P]PF";
        int length = Random.Range(0, 5);
        for (int i = 0; i < length; i++)
        {
            grammar += "+";
            int size = Random.Range(10,20);
            for (int x = 0;x < size; x++){

                grammar += "PF";

                int branchNum = Random.Range(0, 3);
                if (branchNum > 0 && Random.Range(0f, 1.0f) > 0.8)
                {
                    int branchLength = Random.Range(1, 3);
                    string branch = "";
                    for (int j = 0; j < branchLength; j++)
                    {
                        branch += "PF";
                    }
                    grammar = grammar + "[+" + branch + "]";
                    branchNum--;
                }
            }

        }
        BaseGrammar = grammar;
        DeleteAll();
        Generate();
    }


    //generate north europe house
    public void reGenerateNorthEurope()
    {
        int length = Random.Range(3, 5);
        int branchNum = Random.Range(0, 2);
        string grammar = "";
        for (int i = 0; i < length; i++)
        {
            grammar += "PF";
            if (branchNum > 0 && Random.Range(0f, 1.0f) > 0.5)
            {
                int branchLength = Random.Range(0, 2);
                string branch = "";
                for (int j = 0; j < branchLength; j++)
                {
                    branch += "PF";
                }
                grammar = grammar + "[+" + branch + "]";
                branchNum--;
            }
        }
        //return "PFPFP[+PFPFP]";
        BaseGrammar = grammar;
        DeleteAll();
        Generate();
    }

    //generate american house
    public void reGenerateAmericanHouse()
    {
        int length = Random.Range(3, 10);
        int branchNum = Random.Range(0, 3);
        string grammar = "";
        for (int i = 0; i < length; i++)
        {
            grammar += "PF";
            if (branchNum > 0 && Random.Range(0f, 1.0f) > 0.5)
            {
                int branchLength = Random.Range(0, 3);
                string branch = "";
                for (int j = 0; j < branchLength; j++)
                {
                    branch += "FP";
                }
                if (Random.Range(0, 4) == 0)
                {
                    grammar = grammar + "[-" + branch + "]";
                }
                else
                {
                    grammar = grammar + "[+" + branch + "]";
                }
                branchNum--;
            }
        }
        if (Random.Range(0, 4)==0)
        {
            grammar += "[+FPFPFPFP]";
        }
        BaseGrammar = grammar;
        DeleteAll();
        Generate();
    }


    //generate the base of the architecture from BaseGrammar
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
        for (int i = 0; i < BaseGrammar.Length; i++)
        {
            //move forward
            if (BaseGrammar[i] == 'F')
            {
                transform.position = transform.position +
                    new Vector3(step * transform.forward.x, step * transform.forward.y, step * transform.forward.z);
            }
            //place a block
            if (BaseGrammar[i] == 'P')
            {
                basePosition.Add(transform.position);
                baseRotation.Add(transform.eulerAngles);
                onBranch.Add(branch);
                count++;
            }
            //turn certain degree on Y-axis
            if (BaseGrammar[i] == '+')
            {
                transform.Rotate(0, stepRotation, 0);
            }
            //turn minus degree on Y-axis
            if (BaseGrammar[i] == '-')
            {
                transform.Rotate(0, -stepRotation, 0);
            }
            //branch start
            if (BaseGrammar[i] == '[')
            {
                branch = true;
                savedPosition = transform.position;
                savedRotation = transform.eulerAngles;
            }
            //branch end
            if (BaseGrammar[i] == ']')
            {
                branch = false;
                transform.position = savedPosition;
                transform.eulerAngles = savedRotation;
            }
        }
        transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));

        //generate block on the base
        for (int i=0;i<count;i++)
        {
            Vector3 currentPosition = basePosition[i];
            Vector3 currentRotation = baseRotation[i];
            int blockID = Random.Range(1, 4);
            //check if there is dome
            if (dome > 0)
            {
                if (i == count - 1)
                {
                    GenerateDomeBlock(currentPosition, currentRotation, unitC, onBranch[i]);
                    dome--;
                    continue;
                }
                if (Random.Range(1, 10) == 1)
                {
                    GenerateDomeBlock(currentPosition, currentRotation, unitC, onBranch[i]);
                    dome--;
                    continue;
                }
            }
            if (blockID == 1)
            {
                GenerateBlock(currentPosition, currentRotation, unitA, unitHeightA, onBranch[i]);
            }
            else if (blockID == 2)
            {
                GenerateBlock(currentPosition, currentRotation, unitB, unitHeightB, onBranch[i]);
            }
            else
            {
                GenerateBlock(currentPosition, currentRotation, unitA, unitHeightA, onBranch[i]);
            }
        }
    }

    
    //generate one block at given position
    void GenerateBlock(Vector3 targetPosition, Vector3 targetRotation, GameObject unit, float unitHeight, bool onBranch)
    {
        int floor = Random.Range(minFloor, maxFloor + 1);
        if (onBranch)
        {
            floor = Random.Range(branchMinFloor, branchMaxFloor + 1);
        }
        GameObject Block = GameObject.Instantiate(unit, new Vector3(targetPosition.x, unit.transform.localScale.y * unitHeight * floor/2,targetPosition.z), unit.transform.rotation);
        Block.transform.localScale = new Vector3(Block.transform.localScale.x, Block.transform.localScale.y * floor, Block.transform.localScale.z);
        if (unitHeight != 1)
        {
            Block.transform.position -= new Vector3(0, Block.transform.localScale.y * unitHeight / 2, 0);
        }
        if (onBranch && hasTower)
        {
            GenerateTower(new Vector3(Block.transform.position.x, Block.transform.localScale.y * unitHeight, Block.transform.position.z), targetRotation);
        }

        Block.transform.Rotate(targetRotation);

        
        //generate roof
        if (onBranch && multipleRoof)
        {
            if (Random.Range(0.0f, 1.0f) > 0.5)
            {
                GameObject Roof = GameObject.Instantiate(RoofA, new Vector3(Block.transform.position.x, Block.transform.localScale.y * unitHeight, Block.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                Roof.transform.Rotate(targetRotation);
                Roof.transform.position = new Vector3(Block.transform.position.x, Block.transform.localScale.y, Block.transform.position.z);
            }
            else
            {
                GameObject Roof = GameObject.Instantiate(RoofB, new Vector3(Block.transform.position.x, Block.transform.localScale.y * unitHeight, Block.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
                Roof.transform.Rotate(targetRotation);
                Roof.transform.position = new Vector3(Block.transform.position.x, Block.transform.localScale.y, Block.transform.position.z);
            }
        }
        else
        {
            GameObject Roof = GameObject.Instantiate(RoofA, new Vector3(Block.transform.position.x, Block.transform.localScale.y * unitHeight, Block.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
            Roof.transform.Rotate(targetRotation);
            Roof.transform.position = new Vector3(Block.transform.position.x, Block.transform.localScale.y, Block.transform.position.z);
        }

        //generate bay window
        if (hasWindow)
        {
            if (Random.Range(0, 7) == 1)
            {
                GameObject window = GameObject.Instantiate(Window, new Vector3(Block.transform.position.x, Block.transform.localScale.y * unitHeight, Block.transform.position.z), Quaternion.Euler(targetRotation));
            }
        }
    }

    //generate dome
    void GenerateDomeBlock(Vector3 targetPosition, Vector3 targetRotation, GameObject unit, bool onBranch)
    {
        int floor = Random.Range(minFloor, maxFloor + 1);
        if (onBranch)
        {
            floor = Random.Range(branchMinFloor, branchMaxFloor + 1);
        }
        floor += 2;
        GameObject Block = GameObject.Instantiate(unit, new Vector3(targetPosition.x, unit.transform.localScale.y *5f * floor/2 , targetPosition.z), unit.transform.rotation);
        Block.transform.localScale = new Vector3(Block.transform.localScale.x, Block.transform.localScale.y * floor, Block.transform.localScale.z);
        Block.transform.position -= new Vector3(0, Block.transform.localScale.y * 2.5f, 0);


        Block.transform.Rotate(targetRotation);

        GameObject Roof = GameObject.Instantiate(DomeRoof, new Vector3(Block.transform.position.x, Block.transform.localScale.y * 5, Block.transform.position.z), Quaternion.Euler(targetRotation));

    }

    //generate cylinder block
    void GenerateCylinderBlock(Vector3 targetPosition, Vector3 targetRotation, GameObject unit, float unitHeight, bool onBranch)
    {
        int floor = Random.Range(minFloor, maxFloor + 1);
        if (onBranch)
        {
            floor = Random.Range(branchMinFloor, branchMaxFloor + 1);
        }
        GameObject Block = GameObject.Instantiate(unit, new Vector3(targetPosition.x, unit.transform.localScale.y * unitHeight * floor / 2, targetPosition.z), unit.transform.rotation);
        Block.transform.localScale = new Vector3(Block.transform.localScale.x, Block.transform.localScale.y * floor, Block.transform.localScale.z);
        Block.transform.position -= new Vector3(0, Block.transform.localScale.y * unitHeight /2, 0);


        Block.transform.Rotate(targetRotation);

        GameObject Roof = GameObject.Instantiate(DomeRoof, new Vector3(Block.transform.position.x, Block.transform.localScale.y * unitHeight, Block.transform.position.z), Quaternion.Euler(targetRotation));

    }

    //generate tower
    void GenerateTower(Vector3 targetPosition, Vector3 targetRotation)
    {
        GameObject tower1 = GameObject.Instantiate(Tower, new Vector3(targetPosition.x , targetPosition.y, targetPosition.z ), Quaternion.Euler(targetRotation));

    }

    //delete existing architecture when regenerating
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
