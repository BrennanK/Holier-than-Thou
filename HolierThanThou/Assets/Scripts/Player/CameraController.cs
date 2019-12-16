using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    GameObject myPlayer;
    CinemachineFreeLook myCamera;
    private List<RaycastHit> m_objectsInBetweenPlayerAndCamera;
    Color originalColor;
    Color originalColor2;
    Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponentInParent<Competitor>().gameObject;
        myCamera = GetComponent<CinemachineFreeLook>();
        m_objectsInBetweenPlayerAndCamera = new List<RaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAllTheseToTransparent(Physics.RaycastAll(transform.position, (myPlayer.transform.position - transform.position), (Vector3.Distance(transform.position, (myPlayer.transform.position)) ) ));

        RaycastHit hitSphere;
        if(Physics.SphereCast(myPlayer.transform.position, 1f,Vector3.up, out hitSphere,10f) || Physics.SphereCast(myCamera.transform.position, 1f,Vector3.up, out hitSphere, 10f) || Physics.SphereCast(new Vector3(myCamera.transform.position.x, myPlayer.transform.position.y, myCamera.transform.position.z), 1f,Vector3.up, out hitSphere, 10f))
        {
            myCamera.m_YAxis.Value = Mathf.Lerp(myCamera.m_YAxis.Value, .5f, .1f);
        }
        else
        {
            myCamera.m_YAxis.Value = Mathf.Lerp(myCamera.m_YAxis.Value, 1, .1f); ;
        }
    }

    private void SetAllTheseToTransparent(RaycastHit[] _hitsToSet) {
        List<RaycastHit> allHitsToSet = new List<RaycastHit>();

        foreach(RaycastHit hit in _hitsToSet) {
            if(hit.transform == myPlayer.transform || hit.transform == this.transform) {
                continue;
            }

            allHitsToSet.Add(hit);
        }

        foreach(RaycastHit hit in m_objectsInBetweenPlayerAndCamera) {

            if (hit.transform.GetComponent<MeshRenderer>())
            {

                materials = hit.transform.GetComponent<Renderer>().materials;

                for (int j = 0; j < materials.Length; j++)
                {
                    if (materials[j].shader.name == "Nasty/CelShading" || materials[j].shader.name == "Nasty/CelShading - Transparent")
                    {
                        if (materials[j].GetColor("_ColorBlend").a < 0.26f)
                        {
                            return;
                        }
                        else
                        {
                            SetAlphaToColorOnHit(myPlayer.GetComponent<Competitor>(), hit, 1.00f);
                        }
                    }
                    else
                    {
                        if (materials[j].GetColor("_Color").a < 0.26f)
                        {
                            return;
                        }
                        else
                        {
                            SetAlphaToColorOnHit(myPlayer.GetComponent<Competitor>(), hit, 1.00f);
                        }
                    }

                }

            }

            if (hit.transform.tag == "ItemBox" || !hit.transform.GetComponent<Renderer>() || hit.transform.tag == "PlasmaTube") {
                return;
            }
            else{ 

                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i].shader.name == "Nasty/CelShading" || materials[i].shader.name == "Nasty/CelShading - Transparent")
                    {
                        materials[i].shader = Shader.Find("Nasty/CelShading");
                    }
                }
            }

        }

        m_objectsInBetweenPlayerAndCamera.Clear();

        foreach(RaycastHit hit in allHitsToSet) {
            if (hit.transform.GetComponent<MeshRenderer>())
            {
                materials = hit.transform.GetComponent<Renderer>().materials;

                for (int x = 0; x < materials.Length; x++)
                {

                    if (materials[x].shader.name == "Nasty/CelShading" || materials[x].shader.name == "Nasty/CelShading - Transparent")
                    {
                        if (hit.transform.tag == "LessTransparent")
                        {
                            SetAlphaToColorOnHit(myPlayer.GetComponent<Competitor>(), hit, 0.8f);
                        }
                        else if (materials[x].GetColor("_ColorBlend").a < 0.26f)
                        {
                            return;
                        }
                        else
                        {
                            SetAlphaToColorOnHit(myPlayer.GetComponent<Competitor>(), hit, 0.26f);
                        }
                    }
                    else
                    {
                        if (hit.transform.tag == "LessTransparent")
                        {
                            SetAlphaToColorOnHit(myPlayer.GetComponent<Competitor>(), hit, 0.8f);
                        }
                        else if (materials[x].GetColor("_Color").a < 0.26f)
                        {
                            return;
                        }
                        else
                        {
                            SetAlphaToColorOnHit(myPlayer.GetComponent<Competitor>(), hit, 0.26f);
                        }
                    }

                }

            }
            m_objectsInBetweenPlayerAndCamera.Add(hit);
        }
    }

    private void SetAlphaToColorOnHit(Competitor competitor, RaycastHit _raycastHit, float _alphaToSet) {

        if(competitor.isTerrain == true || competitor.tag == "Enemy" || _raycastHit.transform.tag == "ItemBox" || _raycastHit.transform.tag == "PlasmaTube")
        {
            return;
        }
        else
        {
            materials = _raycastHit.transform.GetComponent<Renderer>().materials;
            for (int i = 0; i < materials.Length; i++)
            {

                if (_raycastHit.transform.GetComponent<MeshRenderer>())
                {
                    
                    if (materials[i].shader.name == "Nasty/CelShading" || materials[i].shader.name == "Nasty/CelShading - Transparent")
                    {
                            originalColor2 = materials[i].GetColor("_ColorBlend");
                            materials[i].DisableKeyword("_ALPHATEST_ON");
                            materials[i].DisableKeyword("_ALPHABLEND_ON");
                            materials[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                            materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                            materials[i].SetInt("_Zwrite", 0);
                            materials[i].renderQueue = 3000;
                            materials[i].shader = Shader.Find("Nasty/CelShading - Transparent");
                            materials[i].SetColor("_ColorBlend", new Color(originalColor2.r, originalColor2.g, originalColor2.b, _alphaToSet));
                            materials[i].SetFloat("_Mode", 3);
                        
                       
                    }
                    else
                    {
                            originalColor = materials[i].GetColor("_Color");
                            materials[i].DisableKeyword("_ALPHATEST_ON");
                            materials[i].DisableKeyword("_ALPHABLEND_ON");
                            materials[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                            materials[i].SetInt("_Zwrite", 0);
                            materials[i].renderQueue = 3000;
                            materials[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, _alphaToSet);
                            materials[i].SetFloat("_Mode", 3);
                    }

                }
            }
        }
    }
    
}
