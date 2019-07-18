using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MyslKomputer : MonoBehaviour
{
	public GameObject obiekt1;
	public GameObject obiekt2;
	public GameObject obiekt3;
	int losowe;
	public GameObject wylaczgra;

	public static MyslKomputer instancja;
	public int i = 0;
	void Update() 
	{

		if (Informacjeorozgrywce.instancja.passMoj == true && Informacjeorozgrywce.instancja.pktPrzec > Informacjeorozgrywce.instancja.pktMoje)
		{
				Informacjeorozgrywce.instancja.PrzycsikKoniecRundydlakompa();
		}


		//Informacjeorozgrywce.instancja.Aktualizujgre();
		if (Informacjeorozgrywce.instancja.RamkaWyniku.enabled == false)
		{
			if (Informacjeorozgrywce.instancja.tura == Informacjeorozgrywce.Tura.PrzecTura)
			{
				i += 1;
				//zrob waita bo nie slychac jak klade
				if (i >= 200)
				{
					Dropdlakompa();
					i = 0;
				}
				
			}

			Informacjeorozgrywce.instancja.Aktualizujgre();
		}
		else
		{
			wylaczgra.SetActive(false);
			//enabled = false;
		}
	}
		

/*	public void RuchyKompa()
	{
		if (Informacjeorozgrywce.instancja.RamkaWyniku.enabled == false)
		{
			if (Informacjeorozgrywce.instancja.tura == Informacjeorozgrywce.Tura.PrzecTura)
			{
				Dropdlakompa();
			}

			if (Informacjeorozgrywce.instancja.passMoj == true && Informacjeorozgrywce.instancja.pktPrzec > 1) Informacjeorozgrywce.instancja.PrzycsikKoniecRundy();
			Informacjeorozgrywce.instancja.Aktualizujgre();
		}
	}
*/

	public void PatrzcoMamJA()
	{
		for (int i = 0; i < Informacjeorozgrywce.instancja.MojaRekaKart.Count; i++)
		{
			//Debug.Log(Informacjeorozgrywce.instancja.MojaRekaKart[i]);
		}
	}

	public void Zobacz_co_w_rece()
	{
		for (int i = 0; i < Informacjeorozgrywce.instancja.PrzecRekaKart.Count; i++)
		{
			//Debug.Log(Informacjeorozgrywce.instancja.PrzecRekaKart[i]);
		}
	}

	void Awake()
	{
		instancja = this;
	}

	public void Dropdlakompa()
	{
		//losowa karta
		losowe = Random.Range(0, Informacjeorozgrywce.instancja.PrzecRekaKart.Count);
		//Debug.Log(losowe);
		GameObject pk = Informacjeorozgrywce.instancja.PrzecRekaKart[losowe]; //losowa karta

		//tu zrob do losowej pozycji
		int z = Random.Range(0, 3);
		switch (z)
		{
			case 0:
				pk.transform.SetParent(Informacjeorozgrywce.instancja.PozycjaStoluGornego);
				pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic = Informacjeorozgrywce.instancja.PozycjaStoluGornego;
				pk.GetComponent<PrzeciaganieKart>().powrotdorodzica = Informacjeorozgrywce.instancja.PozycjaStoluGornego;
				pk.GetComponent<PrzeciaganieKart>().karta = Informacjeorozgrywce.instancja.PozycjaStoluGornego;
				break;
			case 1:
				pk.transform.SetParent(Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego);
				pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic = Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego;
				pk.GetComponent<PrzeciaganieKart>().powrotdorodzica = Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego;
				pk.GetComponent<PrzeciaganieKart>().karta = Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego;
				break;
			case 2:
				pk.transform.SetParent(Informacjeorozgrywce.instancja.PozycjaStoluDolnego);
				pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic = Informacjeorozgrywce.instancja.PozycjaStoluDolnego;
				pk.GetComponent<PrzeciaganieKart>().powrotdorodzica = Informacjeorozgrywce.instancja.PozycjaStoluDolnego;
				pk.GetComponent<PrzeciaganieKart>().karta = Informacjeorozgrywce.instancja.PozycjaStoluDolnego;
				break;
		}
	
		///
		///
		///

		///Dodanie do roznych stolow , list , spacingi do stolu
		if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
		{
			Informacjeorozgrywce.instancja.KartywStole.Add(pk.gameObject);
			//Debug.Log(gameObject);
			obiekt1.GetComponent<HorizontalLayoutGroup>().spacing -= 0.75f;
		}
		if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
		{
			Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Add(pk.gameObject);
			obiekt2.GetComponent<HorizontalLayoutGroup>().spacing -= 0.75f;
		}
		if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
		{
			Informacjeorozgrywce.instancja.KartywStoleDolnym.Add(pk.gameObject);
			obiekt3.GetComponent<HorizontalLayoutGroup>().spacing -= 0.75f;
		}
		///

		///dodanie pkt glonwych , przez zycie karty i usunieci je z reki , chodzi o liste , przestawienie tury
		if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
		{
			Informacjeorozgrywce.instancja.pktPrzec += pk.gameObject.GetComponent<Informacjeokarcie>().zycie;
			Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.MojaTura;
			Informacjeorozgrywce.instancja.PrzecRekaKart.Remove(pk.gameObject);
		}
		///

		///PASSY
		if (Informacjeorozgrywce.instancja.passMoj == true) Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.PrzecTura;
		if (Informacjeorozgrywce.instancja.passPrzec == true) Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.MojaTura;
		///

		///Po polozeniu zmienienie statusu i wylaczenie zaslonki u przeciwnika
		pk.gameObject.GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.NaStole;
		if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
		{
			pk.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
			pk.gameObject.GetComponent<Informacjeokarcie>().zaslonka.SetActive(false);
		}


		///ZDOLNOSCI

		///WILK dziala
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "wilk" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "wilk (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "wilk (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(5);
			for (int i = 0; i < 2; i++)
			{
				if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
				{
					Informacjeorozgrywce.instancja.KartywStole.Add(Instantiate(pk.gameObject, pk.transform.parent));
				}
				if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
				{
					Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Add(Instantiate(pk.gameObject, pk.transform.parent));
				}
				if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
				{
					Informacjeorozgrywce.instancja.KartywStoleDolnym.Add(Instantiate(pk.gameObject, pk.transform.parent));
				}
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += pk.gameObject.GetComponent<Informacjeokarcie>().zycie;
			}
		}

		///ANIOL MILOSCI dziala
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł miłości" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł miłości (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł miłości (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(3);
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
			{
				int dlugosc = Informacjeorozgrywce.instancja.KartywStole.Count - 1;
				//Debug.Log("wartosc dlugosc " + dlugosc);
				pk.gameObject.GetComponent<Informacjeokarcie>().zycie += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += dlugosc;
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				int dlugosc = Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Count - 1;
				//Debug.Log("wartosc dlugosc " + dlugosc);
				pk.gameObject.GetComponent<Informacjeokarcie>().zycie += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += dlugosc;
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
			{
				int dlugosc = Informacjeorozgrywce.instancja.KartywStoleDolnym.Count - 1;
				//Debug.Log("wartosc dlugosc " + dlugosc);
				pk.gameObject.GetComponent<Informacjeokarcie>().zycie += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += dlugosc;
			}
		}

		/// NIETOPERZ
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "nietoperz" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "nietoperz (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "nietoperz (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(1);
			Transform obiekt;
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
			{
				//Debug.Log("pk " + pk);

				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					//Debug.Log("obiekt " + obiekt + "ktory " + i);
					//if (obiekt.name == pk.name) Destroy(obiekt);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						//	Debug.Log("Warunek 1");
						pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie += 1;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
					}

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount - 1)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
					}

				}
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie += 1;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
					}

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount - 1)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
					}
				}
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie += 1;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
					}

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount - 1)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
					}
				}
			}
		}

		///ANIOL
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(0);
			Transform obiekt;
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount - 1)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
					}
				}
			}
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						if (i == 0)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount - 1)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
					}
				}
			}
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						if (i == 0)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount - 1)
						{
							pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (pk.transform.parent.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
					}
				}
			}
		}

		///ŚMIERĆ
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "śmierć" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "śmierć (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "śmierć (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(2);
			Transform obiekt;
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;

						pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
					}
				}
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;

						pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
					}
				}
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;

						pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
					}
				}
			}
		}

		///POSŁANIEC
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "posłaniec" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "posłaniec (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "posłaniec (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(4);
			Transform obiekt;
			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
						{
							int tmp = pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktMoje -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktMoje += -tmp;
						}

						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
						{
							int tmp = pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktPrzec -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktPrzec += -tmp;
						}
					}
				}
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
						{
							int tmp = pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktMoje -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktMoje += -tmp;
						}

						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
						{
							int tmp = pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktPrzec -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktPrzec += -tmp;
						}
					}
				}
			}

			if (pk.GetComponent<PrzeciaganieKart>().miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = pk.transform.parent.GetChild(i);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
						{
							int tmp = pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktMoje -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktMoje += -tmp;
						}

						if (pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
						{
							int tmp = pk.transform.parent.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktPrzec -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktPrzec += -tmp;
						}
					}
				}
			}
		}
		///wyswietlenie , blok + tura numer
		Informacjeorozgrywce.instancja.Aktualizujgre();
		Informacjeorozgrywce.instancja.Blokpoturze();
		Informacjeorozgrywce.instancja.turaNumer++;
	}

}
