using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    private bool hasCrown;
    private GameObject crownPH;
    private int CrownValue = 0; 

    // Start is called before the first frame update
    void Start()
    {
        crownPH = TransformDeepChildExtension.FindDeepChild(this.transform, "PE_Crown").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CrownValue >= 1)
        {
            hasCrown = true;
            CrownCheck(hasCrown);
        }
        else
        {
            hasCrown = false;
            CrownCheck(hasCrown);
        }
    }

    void CrownCheck(bool hasCrown)
    {
        if (hasCrown)
        {
            crownPH.SetActive(true);
        }
        else
        {
            crownPH.SetActive(false);
        }
    }

    public bool Crowned()
    {
        return hasCrown;
    }

    public int getCrownVal()
    {
        return CrownValue;
    }

    public void addCrownVal()
    {
        CrownValue++;
    }
    
    public void setCrownVal(int val)
    {
        CrownValue = CrownValue + val;
    }

    public void resetCrown()
    {
        CrownValue = 0;
    }
}
