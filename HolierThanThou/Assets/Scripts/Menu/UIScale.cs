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
}
