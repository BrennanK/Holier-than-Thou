using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
	[SerializeField] RectTransform[] ItemsToScale;

	[SerializeField] int width = 800;
	[SerializeField] int height = 480;
	
    // Start is called before the first frame update
    void Start()
    {
		int newWidth = Screen.width;
		int newHeight = Screen.height;
		float adjustment = (float)newHeight / (float)height;

		if (newHeight == height && newWidth == width)
		{
			//Nothing to see here. Move along.
		}
		else
		{
			foreach (RectTransform item in ItemsToScale)
			{
				item.offsetMax *= adjustment;
				item.offsetMin *= adjustment;
			}
		}
	}

//#if UNITY_EDITOR
//	private bool screenResolutionChanged = false;
//	int currentHeight = 0;
//	int previousHeight = 0;
//	int currentWidth = 0;
//	int previousWidth = 0;
//	void Update()
//	{
//		currentHeight = Screen.height;
//		currentWidth = Screen.width;
//		if(currentHeight != previousHeight || currentWidth != previousWidth)
//		{
//			screenResolutionChanged = true;
//		}
//		previousHeight = currentHeight;
//		previousWidth = currentWidth;

//		if (screenResolutionChanged)
//		{
//			screenResolutionChanged = false;
//			float adjustment = (float)Screen.height / (float)height;
//			foreach (RectTransform item in ItemsToScale)
//			{
//				item.offsetMax *= adjustment;
//				item.offsetMin *= adjustment;
//			}
//		}
//	}
//#endif
}
