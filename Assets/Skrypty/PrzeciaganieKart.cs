using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrzeciaganieKart : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Transform powrotdorodzica = null;
	public Transform miejscenakarteRodzic = null;
	public Transform karta = null;

	public GameObject miejscenakarte = null;
	
	public static PrzeciaganieKart instancja4;

	void Awake()
	{
		instancja4 = this;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//	Debug.Log("Zaczynamtrzymac");

		//	Informacjeokarcie.instancja2.Zaznaczony = true;

		//miejscenakarte = new GameObject("Miejsce");
		miejscenakarte = gameObject;
		miejscenakarte.transform.SetParent(this.transform.parent);
		//LayoutElement le = miejscenakarte.AddComponent<LayoutElement>();
		//le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		//le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		//le.flexibleWidth = 0;
		//le.flexibleHeight = 0;

		powrotdorodzica = this.transform.parent;   //...koment dol

		miejscenakarteRodzic = powrotdorodzica;

		this.transform.SetParent(this.transform.parent.parent); //powrot do grupy czyli do reki 
		GetComponent<CanvasGroup>().blocksRaycasts = false;  //blokowanie
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("Trzymanie");

		this.transform.position = eventData.position; //przeciaganie karta

		if (miejscenakarte.transform.parent != miejscenakarteRodzic) miejscenakarte.transform.SetParent(miejscenakarteRodzic);

		int nowerodzenstwoIndex = miejscenakarteRodzic.childCount;

		for (int i = 0; i < miejscenakarteRodzic.childCount; i++)
		{
			//Debug.Log("Zliczam dzieci "+i);
			
			if (this.transform.position.x < miejscenakarteRodzic.GetChild(i).position.x)
			{
				nowerodzenstwoIndex = i;

				if (miejscenakarte.transform.GetSiblingIndex() < nowerodzenstwoIndex) nowerodzenstwoIndex--;
				break;
			}
			//	Debug.Log("Zliczam dzieci 2 " + i);
		}
		miejscenakarte.transform.SetSiblingIndex(nowerodzenstwoIndex);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(powrotdorodzica.name == "RekaGracza") this.transform.SetParent(this.transform.parent.parent);
		//	Debug.Log("Koniectrzymania");
		this.transform.SetParent(powrotdorodzica); //powrot do rodzica jesli nie lezy na panelu
		this.transform.SetSiblingIndex(miejscenakarte.transform.GetSiblingIndex());  //wklada pomiedzy a nie na koniec rodzica

		GetComponent<CanvasGroup>().blocksRaycasts = true;

		karta = miejscenakarteRodzic; //zmienna pomocnicza do warunku

		if (karta.name == "Stoldolny" || karta.name == "Stolsrodkowy" || karta.name == "Stolgorny")
		{
			//Debug.Log("BLOKADA");
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		} 

		//Destroy(miejscenakarte);
		
	}
}
