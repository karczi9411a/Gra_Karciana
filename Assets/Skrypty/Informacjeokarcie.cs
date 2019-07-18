using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Informacjeokarcie : MonoBehaviour, IPointerEnterHandler //, System.ICloneable
{
	public static Informacjeokarcie instancja2; //przydaje sie bo sa bledy

	public string nazwa;
	public int zycie;
	//public int atak;
	public string opis;

	public Text nazwaTxt;
	public Text zycieTxt;
	//public Text atakTxt;
	public Text opisTxt;

	public enum Strona { Moja, Przec };
	public Strona strona = Strona.Moja;

	public enum StatusKarty { wTalii, wRece, NaStole, Zniszczona };
	public StatusKarty statuskarty = StatusKarty.wTalii;
	//	public bool Zaznaczony = false;
	public GameObject zaslonka;
	


	void Start()
	{
		//	opisTxt.gameObject.SetActive(false);
	}

	void Awake()
	{
		instancja2 = this;
	}

	void FixedUpdate()
	{
		if (zycie <= 0)
		{
			//statuskarty = StatusKarty.Zniszczona;
			//gameObject.SetActive(false);
			Destroy(gameObject);
			Informacjeorozgrywce.instancja.Cmentarz.Add(gameObject);
			Informacjeorozgrywce.instancja.KartywStole.Remove(gameObject);
			Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Remove(gameObject);
			Informacjeorozgrywce.instancja.KartywStoleDolnym.Remove(gameObject);
		}

		nazwaTxt.text = nazwa.ToString();
		zycieTxt.text = zycie.ToString();
		
	}

	public void UstawStatusKarty(StatusKarty status)
	{
		statuskarty = status;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (strona == Strona.Moja) opisTxt.text = opis.ToString();	
	}
}