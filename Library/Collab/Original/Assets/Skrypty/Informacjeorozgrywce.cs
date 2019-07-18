using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Informacjeorozgrywce : MonoBehaviour
{
	public Transform PozycjaRekiMoja;
	public Transform PozycjaRekiPrzeci;

	public static Informacjeorozgrywce instancja; //przydaje sie bo sa bledy

	public List<GameObject> MojaTaliaKart = new List<GameObject>();
	public List<GameObject> MojaRekaKart = new List<GameObject>();
	//public List<GameObject> MojaStolKart = new List<GameObject>();

	public List<GameObject> PrzecTaliaKart = new List<GameObject>();
	public List<GameObject> PrzecRekaKart = new List<GameObject>();
	//public List<GameObject> PrzecStolKart = new List<GameObject>();

	public enum Tura { MojaTura, PrzecTura };

	public Text pktMojeTxt;
	public Text pktPrzecTxt;

	public int pktMoje;
	public int pktPrzec;

	#region SetStartData
	public Tura tura = Tura.MojaTura;

	public bool graStart = false;
	public int turaNumer = 1;
	#endregion

	public int Runda;
	public int wygranerundyMoje;
	public int wygranerudnyPrzec;

	public Text rundaTxt;
	public Text wygranerundyMojeTxt;
	public Text wygranerudnyPrzecTxt;

	public Button koniecTura;

	public bool passMoj = false;
	public bool passPrzec = false;

	string BiezacaTura;
	public Text BiezacaTuraTxt;

	string passMojString;
	string passPrzecString;

	public Text passMojTxt;
	public Text passPrzecTxt;

	void Awake()
	{
		instancja = this;
	}

	void Start()
	{
		foreach (GameObject kartaobjekt in GameObject.FindGameObjectsWithTag("karta"))
		{
			Debug.Log("Znaleziono " + kartaobjekt.name);
			Informacjeokarcie info = kartaobjekt.GetComponent<Informacjeokarcie>();

			if (info.strona == Informacjeokarcie.Strona.Moja)
				MojaTaliaKart.Add(kartaobjekt);
			else
				PrzecTaliaKart.Add(kartaobjekt);
		}
		StartGry();
	}

	public void StartGry()
	{
		graStart = true;
		Runda = 1;
		wygranerudnyPrzec = 0;
		wygranerundyMoje = 0;
		Aktualizujgre();

		for (int i = 0; i < 5; i++)
		{
			Kartaztali(Informacjeokarcie.Strona.Moja);
			Kartaztali(Informacjeokarcie.Strona.Przec);
		}
		Blokpoturze();
	}

	public void Kartaztali(Informacjeokarcie.Strona strona)
	{

		if (strona == Informacjeokarcie.Strona.Moja && MojaTaliaKart.Count != 0 && MojaTaliaKart.Count < 25) //w tali 25
		{
			int random = Random.Range(0, MojaTaliaKart.Count);
			GameObject tempkarta = MojaTaliaKart[random];

			tempkarta.transform.SetParent(PozycjaRekiMoja);
			tempkarta.GetComponent<Informacjeokarcie>().UstawStatusKarty(Informacjeokarcie.StatusKarty.wRece);

			MojaTaliaKart.Remove(tempkarta);
			MojaRekaKart.Add(tempkarta);
		}

		if (strona == Informacjeokarcie.Strona.Przec && PrzecTaliaKart.Count != 0 && PrzecTaliaKart.Count < 25)
		{
			int random = Random.Range(0, PrzecTaliaKart.Count);
			GameObject tempkarta = PrzecTaliaKart[random];

			tempkarta.transform.SetParent(PozycjaRekiPrzeci);
			tempkarta.GetComponent<Informacjeokarcie>().UstawStatusKarty(Informacjeokarcie.StatusKarty.wRece);

			PrzecTaliaKart.Remove(tempkarta);
			PrzecRekaKart.Add(tempkarta);
		}
		Aktualizujgre();
	}

	public void Aktualizujgre()
	{
		if (passMoj == true) passMojString = "PASS"; else passMojString = "";
		if (passPrzec == true) passPrzecString = "PASS"; else passPrzecString = "";
		if (passMoj == true && passPrzec == true) { passMojString = ""; passPrzecString = ""; }
		if (tura == Tura.MojaTura) BiezacaTura = "Twoja";
		if (tura == Tura.PrzecTura) BiezacaTura = "Przeciwnika";

		pktMojeTxt.text = pktMoje.ToString();
		pktPrzecTxt.text = pktPrzec.ToString();
		rundaTxt.text = Runda.ToString();
		wygranerudnyPrzecTxt.text = wygranerudnyPrzec.ToString();
		wygranerundyMojeTxt.text = wygranerundyMoje.ToString();
		BiezacaTuraTxt.text = BiezacaTura.ToString();
		passMojTxt.text = passMojString.ToString();
		passPrzecTxt.text = passPrzecString.ToString();
	}

	public void Blokpoturze()
	{
		if (/*Informacjeokarcie.instancja2.strona == Informacjeokarcie.Strona.Moja &&*/ tura == Tura.MojaTura)
		{
			for (int i = 0; i < MojaRekaKart.Count; i++)
			{
				MojaRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = true;
			}
			for (int i = 0; i < PrzecRekaKart.Count; i++)
			{
				PrzecRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = false;
			}
		}
		if (/*Informacjeokarcie.instancja2.strona == Informacjeokarcie.Strona.Przec &&*/ tura == Tura.PrzecTura)
		{
			for (int i = 0; i < MojaRekaKart.Count; i++)
			{
				MojaRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = false;
			}
			for (int i = 0; i < PrzecRekaKart.Count; i++)
			{
				PrzecRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = true;
			}
		}

	}

	void odblokblokMoje()
	{
		for (int i = 0; i < MojaRekaKart.Count; i++)
		{
			MojaRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		for (int i = 0; i < PrzecRekaKart.Count; i++)
		{
			PrzecRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
	}

	void odblokblokPrzec()
	{
		for (int i = 0; i < MojaRekaKart.Count; i++)
		{
			MojaRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
		for (int i = 0; i < PrzecRekaKart.Count; i++)
		{
			PrzecRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void przycsikKoniecRundy()
	{
		if (tura == Tura.PrzecTura) //dzieje sie tak ze jesli ja wyrzuce karte a przeciwnik od razu spasuje to przegrywa , nie wyswietla sie komnunikat
		{	
			passPrzec = true;
			//Blokpoturze();
			odblokblokPrzec();
			//tura = Tura.MojaTura; //tu jest problem
		}
		
		if (tura == Tura.MojaTura) //warunki
		{
			passMoj = true;
			//Blokpoturze(); 
			odblokblokMoje();
			tura = Tura.PrzecTura;
			Aktualizujgre();
		}

		if (passPrzec == true) tura = Tura.MojaTura;

		bool blok=false;

		if (passMoj == true && passPrzec == true)
		{

			if (tura == Tura.MojaTura)
			{
				odblokblokMoje();
				tura = Tura.PrzecTura;
				
			}
			if (tura == Tura.PrzecTura)
			{
				odblokblokPrzec();
				tura = Tura.MojaTura;
				
			}

			if (Runda == 2)
			{
				for (int i = 0; i < 2; i++)
				{
					Kartaztali(Informacjeokarcie.Strona.Moja);
					Kartaztali(Informacjeokarcie.Strona.Przec);
				}
			}

			if (Runda == 3 && blok == false)
			{
				for (int i = 0; i < 1; i++)
				{
					Kartaztali(Informacjeokarcie.Strona.Moja);
					Kartaztali(Informacjeokarcie.Strona.Przec);
				}
			}

			if (pktMoje > pktPrzec) { wygranerundyMoje++; }
			if (pktPrzec > pktMoje) { wygranerudnyPrzec++; }
			if (pktMoje == pktPrzec)
			{
				wygranerudnyPrzec++;
				wygranerundyMoje++;
			}

			Runda++;

			if (Runda > 3)
			{
				Runda = 3;
				blok = true;
			}
			pktMoje = 0;
			pktPrzec = 0;
			
			Aktualizujgre();
			passMoj = false;
			passPrzec = false;
		}
		Aktualizujgre();
	}
}
