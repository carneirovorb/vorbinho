using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;

	private buttonPress press;


	public Color highlightColor = new Color(0, 0, 0, 0);



	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	

	public void OnDrop(PointerEventData data)
	{

		containerImage.color = normalColor;		
		press = FindObjectOfType(typeof(buttonPress)) as buttonPress;
		press.passos(data.pointerEnter.name , data.pointerDrag.name, data.pointerEnter.tag);
		//Debug.Log(data.pointerEnter.tag);


		if (receivingImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			receivingImage.overrideSprite = dropSprite;

	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);

		if (dropSprite != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		//Debug.Log(srcImage.sprite);
		return srcImage.sprite;

	}
}
