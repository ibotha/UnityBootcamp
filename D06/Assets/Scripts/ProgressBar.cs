using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
	public Sprite Normal;
	public Sprite Detected;
	Image image;
	public float progress = 0;
	public RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
		rect = GetComponent<RectTransform>();
		image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		if (progress > 0.74f)
		{
			image.sprite = Detected;
		}
		else
		{
			image.sprite = Normal;
		}
		rect.sizeDelta = new Vector2(Mathf.Lerp(0, 210, progress), rect.sizeDelta.y);
		rect.ForceUpdateRectTransforms();
    }
}
