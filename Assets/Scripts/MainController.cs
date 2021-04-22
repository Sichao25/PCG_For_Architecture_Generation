using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject CustomizedMenu;
    public Dropdown styleSelection;

    public GameObject ModernMenu;
    public GameObject NorthEuropeMenu;
    public GameObject AmericanHouseMenu;
    public GameObject RenaissanceMenu;

    public GrammarApprochScript Generator;
    public GameObject CubeBlock;
    public GameObject CylinderBlock;
    public GameObject DomeRoof;
    public GameObject LowCityRoof;
    public GameObject HighCityRoof;
    public GameObject HouseRoof;
    public GameObject Window;
    public GameObject Tower;
    public GameObject NoRoof;
    // Start is called before the first frame update
    void Start()
    {
        styleSelection.onValueChanged.AddListener(delegate {
            DropdownValueChanged(styleSelection);
        });
        CustomizedMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropdownValueChanged(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                CustomizedMenu.SetActive(false);
                ModernMenu.SetActive(true);
                NorthEuropeMenu.SetActive(false);
                AmericanHouseMenu.SetActive(false);
                RenaissanceMenu.SetActive(false);
        
                Generator.RoofA = NoRoof;
                Generator.hasTower = false;
                Generator.domeNum = 0;
                Generator.hasWindow = false;
                Generator.multipleRoof = false;
                Generator.maxFloor = 9;
                Generator.minFloor = 9;
                Generator.branchMaxFloor = 9;
                Generator.branchMinFloor = 9;
                Generator.BaseGrammar = "PFPFP[+PFPFP]";

                Generator.unitA = CubeBlock;
                Generator.unitB = CubeBlock;
                Generator.unitC = CylinderBlock;
                Generator.stepRotation = 90;
                Generator.step = 5f;
                break;
            case 1:
                CustomizedMenu.SetActive(false);
                ModernMenu.SetActive(false);
                NorthEuropeMenu.SetActive(false);
                AmericanHouseMenu.SetActive(true);
                RenaissanceMenu.SetActive(false);
                Generator.RoofA = HouseRoof;         
                Generator.hasTower = false;
                Generator.hasWindow = true;
                Generator.multipleRoof = false;
                Generator.domeNum = 0;
                Generator.maxFloor = 3;
                Generator.minFloor = 1;
                Generator.branchMaxFloor = 3;
                Generator.branchMinFloor = 1;
                Generator.BaseGrammar = "PFPF[+FP]FPFPFPF[+FPFPFP]";

                Generator.unitA = CubeBlock;
                Generator.unitB = CubeBlock;
                Generator.unitC = CylinderBlock;
                Generator.stepRotation = 90;
                Generator.step = 7.5f;
                break;
            case 2:
                CustomizedMenu.SetActive(false);
                ModernMenu.SetActive(false);
                NorthEuropeMenu.SetActive(true);
                AmericanHouseMenu.SetActive(false);
                RenaissanceMenu.SetActive(false);
                Generator.RoofA = HouseRoof;
                Generator.hasTower = false;
                Generator.domeNum = 0;
                Generator.hasWindow = true;
                Generator.multipleRoof = false;
                Generator.maxFloor = 5;
                Generator.minFloor = 5;
                Generator.branchMaxFloor = 5;
                Generator.branchMinFloor = 5;
                Generator.BaseGrammar = "PFP[+P]FPFP";

                Generator.unitA = CubeBlock;
                Generator.unitB = CubeBlock;
                Generator.unitC = CylinderBlock;
                Generator.stepRotation = 90;
                Generator.step = 7.5f;
                break;
            case 3:
                CustomizedMenu.SetActive(false);
                ModernMenu.SetActive(false);
                NorthEuropeMenu.SetActive(false);
                AmericanHouseMenu.SetActive(false);
                RenaissanceMenu.SetActive(true);
                Generator.RoofA = HouseRoof;
                Generator.hasTower = true;
                Generator.domeNum = 1;
                Generator.hasWindow = true;
                Generator.multipleRoof = true;
                Generator.maxFloor = 3;
                Generator.minFloor = 3;
                Generator.branchMaxFloor = 5;
                Generator.branchMinFloor = 5;
                Generator.BaseGrammar = RenaissanceGrammar();

                Generator.unitA = CubeBlock;
                Generator.unitB = CubeBlock;
                Generator.unitC = CylinderBlock;
                Generator.stepRotation = 90;
                Generator.step = 7.5f;
                break;
            case 4:
                CustomizedMenu.SetActive(true);
                ModernMenu.SetActive(false);
                NorthEuropeMenu.SetActive(false);
                AmericanHouseMenu.SetActive(false);
                RenaissanceMenu.SetActive(false);
                break;
            default:
                Generator.RoofA = NoRoof;
                Generator.hasTower = false;
                Generator.domeNum = 0;
                Generator.hasWindow = false;
                Generator.multipleRoof = false;
                Generator.maxFloor = 9;
                Generator.minFloor = 9;
                Generator.branchMaxFloor = 9;
                Generator.branchMinFloor = 9;
                Generator.BaseGrammar = "PFPFP[+PFPFP]";

                Generator.unitA = CubeBlock;
                Generator.unitB = CubeBlock;
                Generator.unitC = CylinderBlock;
                Generator.stepRotation = 90;
                Generator.step = 7.5f;
                break;
        }
        
    }

    string OfficeBuildingGrammar()
    {
        int length = Random.Range(3,9);
        int branchNum = Random.Range(0, 3);
        string grammar = "";
        for (int i = 0; i < length; i++)
        {
            grammar += "PF";
            if (branchNum>0 && Random.Range(0f, 1.0f) > 0.5)
            {
                grammar = grammar + grammar;
            }
        }
        //return "PFPFP[+PFPFP]";
        return grammar;
    }

    string AmericanHouseGrammar()
    {
        return "PFP[+FP]FPFPFPF[+FPFPFP]";
    }
    string NorthEuropeanGrammar()
    {
        return "PFP[+P]FPFP";
    }
    string RenaissanceGrammar()
    {
        return "FP[+P]FPFPFPFPFPFPFPFPFPFPF[+P]PF";
    }

    public void SetModernHeight(string text)
    {
        bool result;
        int floor = 9;
        result = int.TryParse(text,out floor);
        if (result)
        {
            Generator.maxFloor = floor;
            Generator.minFloor = floor;
            Generator.branchMaxFloor = floor + Random.Range(0, 2);
            Generator.branchMinFloor = floor + Random.Range(-2, 2);
        }
    }

    public void SetNorthEuropeHeight(string text)
    {
        bool result;
        int floor = 5;
        result = int.TryParse(text, out floor);
        if (result)
        {
            Generator.maxFloor = floor;
            Generator.minFloor = floor;
            Generator.branchMaxFloor = floor;
            Generator.branchMinFloor = floor;
        }
    }

}
