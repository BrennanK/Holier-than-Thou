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

        if (origin.GetChild(0).GetChild(1).gameObject.GetComponentInChildren<MeshRenderer>())
        {
             playerH = origin.GetChild(0).GetChild(1).gameObject.GetComponentInChildren<MeshRenderer>().materials;
        }


        if (origin.tag == "Player")
        {
            playerM.DisableKeyword("_ALPHATEST_ON");
            playerM.DisableKeyword("_ALPHABLEND_ON");
            playerM.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            playerM.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            playerM.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            playerM.SetInt("_Zwrite", 0);
            playerM.shader = Shader.Find("Nasty/CelShading - Transparent");
            playerM.SetColor("_ColorBlend", new Color(playerM.GetColor("_ColorBlend").r, playerM.GetColor("_ColorBlend").g, playerM.GetColor("_ColorBlend").b, 0.3f));

            playerM.renderQueue = 3000;
            playerM.SetFloat("_Mode", 3);

            competitor.invisMaterial = playerM;

            if (playerH.Length > 0)
            {
                for (int i = 0; i < playerH.Length; i++)
                {
                    playerH[i].DisableKeyword("_ALPHATEST_ON");
                    playerH[i].DisableKeyword("_ALPHABLEND_ON");
                    playerH[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    playerH[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    playerH[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    playerH[i].SetInt("_Zwrite", 0);
                    playerH[i].shader = Shader.Find("Nasty/CelShading - Transparent");
                    playerH[i].SetColor("_ColorBlend", new Color(playerH[i].GetColor("_ColorBlend").r, playerH[i].GetColor("_ColorBlend").g, playerH[i].GetColor("_ColorBlend").b, 0.3f));
                    playerH[i].renderQueue = 3000;
                    playerH[i].SetFloat("_Mode", 3);
                }

        }

    }
        else
        {
            origin.GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            origin.GetComponent<TrailRenderer>().enabled = false;
            if (origin.GetChild(0).GetChild(1).gameObject.GetComponentInChildren<MeshRenderer>())
            {
                origin.GetChild(0).GetChild(1).gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }

        competitor.CantFindMe(origin, duration);

    }

    
}
