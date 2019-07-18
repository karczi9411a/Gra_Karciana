using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using System.Linq;


[System.Serializable]
public class Informacjeorozgrywce : MonoBehaviour
{
	public Transform PozycjaRekiMoja;
	public Transform PozycjaRekiPrzeci;
	public Transform PozycjaStoluGornego;
	public Transform PozycjaStoluSrodkowego;
	public Transform PozycjaStoluDolnego;


	public static Informacjeorozgrywce instancja; //przydaje sie bo sa bledy

	public List<GameObject> MojaTaliaKart = new List<GameObject>();
	public List<GameObject> MojaRekaKart = new List<GameObject>();

	public List<GameObject> PrzecTaliaKart = new List<GameObject>();
	public List<GameObject> PrzecRekaKart = new List<GameObject>();

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

	//public List<GameObject> MojaCmenatrzKart = new List<GameObject>();
	//public List<GameObject> PrzecCmenatrzKart = new List<GameObject>();

	//public Transform CmentarzaMojaPozycja;
	//public Transform CmentarzaPrzeciPozycja;

	public List<GameObject> KartywStole = new List<GameObject>();
	public List<GameObject> KartywStoleSrodkowym = new List<GameObject>();
	public List<GameObject> KartywStoleDolnym = new List<GameObject>();

	public List<GameObject> Cmentarz = new List<GameObject>();

	public Canvas RamkaWyniku;
	string WynikRozgrywki;
	public Text WynikRozgrywkiTxt;

	bool blok = false;

	public AudioClip[] dzwiek;
	public AudioSource zrodlodlakart;

	public void Muzyczka(int clip)
	{
		zrodlodlakart.clip = dzwiek[clip];
		zrodlodlakart.Play();
	}

	void Awake()
	{
		instancja = this;
	}

	void Start()
	{
		///wylacz zaslonki dla ulatwienia
		
		 foreach (GameObject zaslonkaobiekt in GameObject.FindGameObjectsWithTag("zaslonkadoprzeciwnika"))
		 {
		//	zaslonkaobiekt.SetActive(false);
		 }
		


		foreach (GameObject kartaobjekt in GameObject.FindGameObjectsWithTag("karta"))
		{
		//	Debug.Log("Znaleziono " + kartaobjekt.name);
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

	void FixedUpdate()
	{
		if (PrzecRekaKart.Count == 0) { passPrzec = true; Aktualizujgre(); }
		if (MojaRekaKart.Count == 0) { passMoj = true; Aktualizujgre(); }
		if (passPrzec == true && passMoj == true)
		{
			PrzycsikKoniecRundydlamnie();
			PrzycsikKoniecRundydlakompa();
		}

	}

	public void Aktualizujgre()
	{	
		//	if (Informacjeokarcie.instancja2.zycie == 0) Destroy(gameObject);
		RamkaWyniku.enabled = false;
		WynikRozgrywki = "";
		if (wygranerundyMoje == 2 && wygranerudnyPrzec == 2) { RamkaWyniku.enabled = true; WynikRozgrywki = "REMIS"; blok = true; }
		if (wygranerundyMoje == 2 & wygranerudnyPrzec < 2) { RamkaWyniku.enabled = true; WynikRozgrywki = "WYGRAŁEŚ"; blok = true; }
		if (wygranerudnyPrzec == 2 & wygranerundyMoje < 2) { RamkaWyniku.enabled = true; WynikRozgrywki = "PRZEGRAŁEŚ"; blok = true; }
	//	if (WynikRozgrywki == "REMIS" || WynikRozgrywki == "WYGRAŁEŚ" || WynikRozgrywki == "PRZEGRAŁEŚ") { MojaRekaKart.Clear(); PrzecRekaKart.Clear(); }

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
		WynikRozgrywkiTxt.text = WynikRozgrywki.ToString();
	}


	public void Blokpoturze()
	{
		if (/*Informacjeokarcie.instancja2.strona == Informacjeokarcie.Strona.Moja &&*/
		tura == Tura.MojaTura)
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
				//PrzecRekaKart[i].GetComponent<Informacjeokarcie>().GetComponent<CanvasGroup>().blocksRaycasts = true; //usun , wylacz jak grasz z kompem bo bedzie mozna ruszac kartami komp.
			}
		}

	}

	void OdblokblokMoje()
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

	void OdblokblokPrzec()
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

	public void PrzycsikKoniecRundydlamnie()
	{
		/*
		if (tura == Tura.PrzecTura) //wylacz jak bedzie si
		{	
			passPrzec = true;
			//Blokpoturze();
			OdblokblokPrzec();
			//tura = Tura.MojaTura; //tu jest problem
		}
		*/
		
		
		if (tura == Tura.MojaTura) //warunki
		{
			passMoj = true;
			//Blokpoturze(); 
			OdblokblokMoje();
			tura = Tura.PrzecTura;
		//	Aktualizujgre();
		}

		//if (passPrzec == true) tura = Tura.MojaTura; //problem rozwiazany


		if (passMoj == true && passPrzec == true)
		{
			for (int i = 0; i < KartywStole.Count; i++)
			{
				KartywStole[i].GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.Zniszczona;
				//KartywStole[i].GetComponent<Informacjeokarcie>().gameObject.SetActive(false);
				Cmentarz.Add(KartywStole[i].GetComponent<Informacjeokarcie>().gameObject);
				Destroy(KartywStole[i].GetComponent<Informacjeokarcie>().gameObject);
			}
			for (int i = 0; i < KartywStoleSrodkowym.Count; i++)
			{
				KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.Zniszczona;
				//KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().gameObject.SetActive(false);
				Cmentarz.Add(KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().gameObject);
				Destroy(KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().gameObject);
			}
			for (int i = 0; i < KartywStoleDolnym.Count; i++)
			{
				KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.Zniszczona;
				//KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().gameObject.SetActive(false);
				Cmentarz.Add(KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().gameObject);
				Destroy(KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().gameObject);
			}

			KartywStole.Clear();
			KartywStoleSrodkowym.Clear();
			KartywStoleDolnym.Clear();
			Cmentarz.Clear();


			if (tura == Tura.MojaTura)
			{
				OdblokblokMoje();
				tura = Tura.PrzecTura;
			}
			if (tura == Tura.PrzecTura)
			{
				OdblokblokPrzec();
				tura = Tura.MojaTura;
			}


			if (pktMoje > pktPrzec && blok == false) { wygranerundyMoje++; }
			if (pktPrzec > pktMoje && blok == false) { wygranerudnyPrzec++; }
			if (pktMoje == pktPrzec && blok == false)
			{
				wygranerudnyPrzec++;
				wygranerundyMoje++;
			}

			if (wygranerudnyPrzec > 3)
			{
				wygranerudnyPrzec = 3;
			//	blok = true;
			}
			if (wygranerundyMoje > 3)
			{
				wygranerundyMoje = 3;
			//	blok = true;
			}

			Runda++;

			if (Runda == 2)
			{
				for (int i = 0; i < 2; i++)
				{
					Kartaztali(Informacjeokarcie.Strona.Moja);
					Kartaztali(Informacjeokarcie.Strona.Przec);
				}
				Blokpoturze();
			}

			if (Runda == 3 && blok == false)
			{
				for (int i = 0; i < 1; i++)
				{
					Kartaztali(Informacjeokarcie.Strona.Moja);
					Kartaztali(Informacjeokarcie.Strona.Przec);
				}
				Blokpoturze();
			}

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

	public void PrzycsikKoniecRundydlakompa()
	{
		
		if (tura == Tura.PrzecTura) //wylacz jak bedzie si
		{	
			passPrzec = true;
			//Blokpoturze();
			OdblokblokPrzec();
			tura = Tura.MojaTura; //tu jest problem
		//	Aktualizujgre();
		}


		/*
		if (tura == Tura.MojaTura) //warunki
		{
			passMoj = true;
			//Blokpoturze(); 
			OdblokblokMoje();
			tura = Tura.PrzecTura;
			Aktualizujgre();
		}
		*/
		//if (passPrzec == true) tura = Tura.MojaTura; //problem rozwiazany


		if (passMoj == true && passPrzec == true)
		{
			for (int i = 0; i < KartywStole.Count; i++)
			{
				KartywStole[i].GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.Zniszczona;
				//KartywStole[i].GetComponent<Informacjeokarcie>().gameObject.SetActive(false);
				Cmentarz.Add(KartywStole[i].GetComponent<Informacjeokarcie>().gameObject);
				Destroy(KartywStole[i].GetComponent<Informacjeokarcie>().gameObject);
			}
			for (int i = 0; i < KartywStoleSrodkowym.Count; i++)
			{
				KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.Zniszczona;
				//KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().gameObject.SetActive(false);
				Cmentarz.Add(KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().gameObject);
				Destroy(KartywStoleSrodkowym[i].GetComponent<Informacjeokarcie>().gameObject);
			}
			for (int i = 0; i < KartywStoleDolnym.Count; i++)
			{
				KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.Zniszczona;
				//KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().gameObject.SetActive(false);
				Cmentarz.Add(KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().gameObject);
				Destroy(KartywStoleDolnym[i].GetComponent<Informacjeokarcie>().gameObject);
			}

			KartywStole.Clear();
			KartywStoleSrodkowym.Clear();
			KartywStoleDolnym.Clear();
			Cmentarz.Clear();


			if (tura == Tura.MojaTura)
			{
				OdblokblokMoje();
				tura = Tura.PrzecTura;
			}
			if (tura == Tura.PrzecTura)
			{
				OdblokblokPrzec();
				tura = Tura.MojaTura;
			}


			if (pktMoje > pktPrzec && blok == false) { wygranerundyMoje++; }
			if (pktPrzec > pktMoje && blok == false) { wygranerudnyPrzec++; }
			if (pktMoje == pktPrzec && blok == false)
			{
				wygranerudnyPrzec++;
				wygranerundyMoje++;
			}

			if (wygranerudnyPrzec > 3)
			{
				wygranerudnyPrzec = 3;
				//	blok = true;
			}
			if (wygranerundyMoje > 3)
			{
				wygranerundyMoje = 3;
				//	blok = true;
			}

			Runda++;

			if (Runda == 2)
			{
				for (int i = 0; i < 2; i++)
				{
					Kartaztali(Informacjeokarcie.Strona.Moja);
					Kartaztali(Informacjeokarcie.Strona.Przec);
				}
				Blokpoturze();
			}

			if (Runda == 3 && blok == false)
			{
				for (int i = 0; i < 1; i++)
				{
					Kartaztali(Informacjeokarcie.Strona.Moja);
					Kartaztali(Informacjeokarcie.Strona.Przec);
				}
				Blokpoturze();
			}

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

	public void NowaGradlaprzycisku()
	{
		OblsugaMenu.instancja5.menuGUI.enabled = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
