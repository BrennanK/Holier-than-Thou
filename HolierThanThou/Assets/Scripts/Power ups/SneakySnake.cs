using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakySnake : PowerUp
{
    public SneakySnake(bool _isEnhancement, bool _hasDuration, float _duration, float _radius) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {

    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        Competitor competitor = origin.GetComponent<Competitor>();
        Material[] playerH = new Material[0];
        var playerM = origin.GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().material;

        if (origin.GetChild(0).GetChild(2).gameObject.GetComponentInChildren<MeshRenderer>())
        {
             playerH = origin.GetChild(0).GetChild(2).gameObject.GetComponentInChildren<MeshRenderer>().materials;
        }

        Color playerColor = new Color(playerM.color.r, playerM.color.g, playerM.color.b, 0.3f);
       
        if (origin.tag == "Player")
        {
            
            playerM.DisableKeyword("_ALPHATEST_ON");
            playerM.DisableKeyword("_ALPHABLEND_ON");
            playerM.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            playerM.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            playerM.SetInt("_Zwrite", 0);
            playerM.SetColor("_Color", playerColor);
            playerM.renderQueue = 3000;
            playerM.SetFloat("_Mode", 3);

            competitor.invisMaterial = playerM;

            if (playerH.Length > 0)
            {
                for (int i = 0; i < playerH.Length; i++)
                {
                    Color playerHColor = new Color(playerH[i].color.r, playerH[i].color.g, playerH[i].color.b, 0.3f);
                    playerH[i].DisableKeyword("_ALPHATEST_ON");
                    playerH[i].DisableKeyword("_ALPHABLEND_ON");
                    playerH[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    playerH[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    playerH[i].SetInt("_Zwrite", 0);
                    playerH[i].SetColor("_Color", playerHColor);
                    playerH[i].renderQueue = 3000;
                    playerH[i].SetFloat("_Mode", 3);
                }

        }

    }
        else
        {
            origin.GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            origin.GetChild(0).GetChild(1).gameObject.SetActive(false);
            if (origin.GetChild(0).GetChild(2).gameObject.GetComponentInChildren<MeshRenderer>())
            {
                origin.GetChild(0).GetChild(2).gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }

        competitor.CantFindMe(origin, duration);

    }

    
}
