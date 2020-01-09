using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;


[RequireComponent(typeof(Image))]
public class dropAndClear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

	
	//	DRAG	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public bool dragOnSurfaces = true;
	
	private GameObject m_DraggingIcon;
	private RectTransform m_DraggingPlane;
	public Color cor;

	public void OnBeginDrag(PointerEventData eventData)
	{


		receivingImage.overrideSprite = null;
		//Debug.Log(eventData.pointerEnter.name);

		var canvas = FindInParents<Canvas>(gameObject);
		if (canvas == null)
			return;


		//instancia um objeto auxiliar para tranferir o sprite
		m_DraggingIcon =new GameObject("icon");
		
		m_DraggingIcon.transform.SetParent (canvas.transform, false);
		m_DraggingIcon.transform.SetAsLastSibling();
		
		var image = m_DraggingIcon.AddComponent<Image>();

		// The icon will be under the cursor.
		// We want it to be ignored by the event system.
		CanvasGroup group = m_DraggingIcon.AddComponent<CanvasGroup>();
		group.blocksRaycasts = false;



		image.sprite = dropSprite;
		//image.SetNativeSize();

		//carrega a classe que recebera os comandos
		press = FindObjectOfType(typeof(buttonPress)) as buttonPress;		
		if(m_DraggingIcon.GetComponent<Image>().sprite!=null)
		{
			press.passos(eventData.pointerEnter.name, null, eventData.pointerEnter.tag);
		}else{image.color = cor;}

		if (dragOnSurfaces)
			m_DraggingPlane = transform as RectTransform;
		else
			m_DraggingPlane = canvas.transform as RectTransform;
		
		SetDraggedPosition(eventData);
	
	}
	
	public void OnDrag(PointerEventData data)
	{
		if (m_DraggingIcon != null)
			SetDraggedPosition(data);
	}
	
	private void SetDraggedPosition(PointerEventData data)
	{
		if(m_DraggingIcon.GetComponent<Image>().sprite!=null){


			if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
				m_DraggingPlane = data.pointerEnter.transform as RectTransform;
			
			var rt = m_DraggingIcon.GetComponent<RectTransform>();
			Vector3 globalMousePos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
			{
				rt.position = globalMousePos;
				rt.rotation = m_DraggingPlane.rotation;
			}

		}
	}

	//destroi individualmente os elementos retirados da trilha

	public void OnEndDrag(PointerEventData eventData)
	{
		if (m_DraggingIcon != null)
			Destroy(m_DraggingIcon);

		m_DraggingIcon.GetComponent<Image>().sprite=null;
		dropSprite = null;
		Debug.Log("destuido");
	}
	
	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();
		
		if (comp != null)
			return comp;
		
		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}








	//	DROP	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





	private Sprite dropSprite;
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	private trashContent limparConteudo;
	private buttonPress press;
	public Sprite alphaSprite;
	
	
	public Color highlightColor = Color.yellow;


	void Start () {
		limparConteudo = FindObjectOfType(typeof(trashContent)) as trashContent;
		press = FindObjectOfType(typeof(buttonPress)) as buttonPress;
	}

	void Update () {

		if(limparConteudo.limpar){
			press.cleanAll();
			StartCoroutine(delay());


		}

	}

	IEnumerator delay() {		
		yield return new WaitForSeconds(0.4f);
		receivingImage.overrideSprite = alphaSprite;
		
	}



	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	
	public void OnDrop(PointerEventData data)
	{
		
		containerImage.color = normalColor;		

		if(data.pointerDrag.tag=="btns"){
		press.passos(data.pointerEnter.name , data.pointerDrag.name, data.pointerEnter.tag);
		}
		
		
		if (receivingImage == null)
			return;
		
		dropSprite = GetDropSprite (data);
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
