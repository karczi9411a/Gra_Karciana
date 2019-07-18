using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class PolozenieStol : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public static PolozenieStol instancja3;

	void Awake()
	{
		instancja3 = this;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//	Debug.Log("NaStole " + gameObject.name);
		if (eventData.pointerDrag == null) return;
		PrzeciaganieKart pk = eventData.pointerDrag.GetComponent<PrzeciaganieKart>();

		if (pk != null)
		{
			pk.miejscenakarteRodzic = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("PozaStolem");
		if (eventData.pointerDrag == null) return;
		PrzeciaganieKart pk = eventData.pointerDrag.GetComponent<PrzeciaganieKart>();

		if (pk != null && pk.miejscenakarteRodzic == this.transform)
		{
			pk.miejscenakarteRodzic = pk.powrotdorodzica;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		//	Debug.Log(eventData.pointerDrag.name + " Polozono " + gameObject.name);
		PrzeciaganieKart pk = eventData.pointerDrag.GetComponent<PrzeciaganieKart>(); //tyllko do warunku przeciagani
																					  //Debug.Log("Dane o obiekcie : " + info.zycie);

		///Dodanie do roznych stolow , list , spacingi do stolu
		if (pk.miejscenakarteRodzic.name == "Stolgorny")
		{
			Informacjeorozgrywce.instancja.KartywStole.Add(pk.gameObject);
			//Debug.Log(gameObject);
			gameObject.GetComponent<HorizontalLayoutGroup>().spacing -= 0.75f;
		}
		if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
		{
			Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Add(pk.gameObject);
			gameObject.GetComponent<HorizontalLayoutGroup>().spacing -= 0.75f;
		}
		if (pk.miejscenakarteRodzic.name == "Stoldolny")
		{
			Informacjeorozgrywce.instancja.KartywStoleDolnym.Add(pk.gameObject);
			gameObject.GetComponent<HorizontalLayoutGroup>().spacing -= 0.75f;
		}
		///

		///dodanie pkt glonwych , przez zycie karty i usunieci je z reki , chodzi o liste , przestawienie tury
		if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
		{
			Informacjeorozgrywce.instancja.pktMoje += pk.gameObject.GetComponent<Informacjeokarcie>().zycie;
			Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.PrzecTura;
			Informacjeorozgrywce.instancja.MojaRekaKart.Remove(pk.gameObject);
		}
	//	else
		//{
		//	Informacjeorozgrywce.instancja.pktPrzec += pk.gameObject.GetComponent<Informacjeokarcie>().zycie;
		//	Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.MojaTura;
		//	Informacjeorozgrywce.instancja.PrzecRekaKart.Remove(pk.gameObject);
	//	}
		///

		///PASSY
		if (Informacjeorozgrywce.instancja.passMoj == true) Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.PrzecTura;
		if (Informacjeorozgrywce.instancja.passPrzec == true) Informacjeorozgrywce.instancja.tura = Informacjeorozgrywce.Tura.MojaTura;
		///

		///Po polozeniu zmienienie statusu i wylaczenie zaslonki u przeciwnika
		pk.gameObject.GetComponent<Informacjeokarcie>().statuskarty = Informacjeokarcie.StatusKarty.NaStole;
		if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
		{
			pk.gameObject.GetComponent<Informacjeokarcie>().zaslonka.SetActive(false);
		}


		///ZDOLNOSCI

		///WILK
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "wilk" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "wilk (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "wilk (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(5);
			for (int i = 0; i < 2; i++)
			{
				if (pk.miejscenakarteRodzic.name == "Stolgorny")
				{
					Informacjeorozgrywce.instancja.KartywStole.Add(Instantiate(pk.gameObject, gameObject.transform));
				}
				if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
				{
					Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Add(Instantiate(pk.gameObject, gameObject.transform));
				}
				if (pk.miejscenakarteRodzic.name == "Stoldolny")
				{	
					Informacjeorozgrywce.instancja.KartywStoleDolnym.Add(Instantiate(pk.gameObject, gameObject.transform));
				}
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += pk.gameObject.GetComponent<Informacjeokarcie>().zycie;
			//	if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += pk.gameObject.GetComponent<Informacjeokarcie>().zycie;
			}
		}

		///ANIOL MILOSCI
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł miłości" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł miłości (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "anioł miłości (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(3);
			if (pk.miejscenakarteRodzic.name == "Stolgorny")
			{
				int dlugosc = Informacjeorozgrywce.instancja.KartywStole.Count - 1;
				//Debug.Log("wartosc dlugosc " + dlugosc);
				pk.gameObject.GetComponent<Informacjeokarcie>().zycie += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += dlugosc;
			}

			if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				int dlugosc = Informacjeorozgrywce.instancja.KartywStoleSrodkowym.Count - 1;
				//Debug.Log("wartosc dlugosc " + dlugosc);
				pk.gameObject.GetComponent<Informacjeokarcie>().zycie += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += dlugosc;
				if (pk.gameObject.GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += dlugosc;
			}

			if (pk.miejscenakarteRodzic.name == "Stoldolny")
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
			if (pk.miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						//	Debug.Log("Warunek 1");
						this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie += 1;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
					}

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount - 1)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
					}
				}
			}

			if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie += 1;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
					}

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount - 1)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
					}
				}
			}

			if (pk.miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie += 1;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
					}

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount - 1)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 1;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 1;
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
			if (pk.miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						//Debug.Log("Warunek 2 " + "liczba i: " + i + "ilosc kart:" + Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount);
						if (i == 0)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount - 1)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}		
					}
				}
			}
			if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						if (i == 0)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount - 1)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
					}
				}
			}
			if (pk.miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);

					if (obiekt.name == pk.name && Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount > 1) //dodajena dziecko prawe + na miejsce 0
					{
						if (i == 0)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
						}
						if (i > 0 && i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount - 1)
						{
							this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().zycie += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje += 2;
							if (this.gameObject.transform.GetChild(i + 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec += 2;
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
			if (pk.miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;

						this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
					}
				}
			}

			if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;

						this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
					}
				}
			}

			if (pk.miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					//	Debug.Log("Zliczam dzieci " + i);
					//	Debug.Log("obiekt " + obiekt + "ktory " + i );
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja) Informacjeorozgrywce.instancja.pktMoje -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec) Informacjeorozgrywce.instancja.pktPrzec -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;

						this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie;
					}
				}
			}
		}

		///POSŁANIEC
		if (pk.gameObject.GetComponent<Informacjeokarcie>().name == "posłaniec" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "posłaniec (1)" || pk.gameObject.GetComponent<Informacjeokarcie>().name == "posłaniec (2)")
		{
			Informacjeorozgrywce.instancja.Muzyczka(4);
			Transform obiekt;
			if (pk.miejscenakarteRodzic.name == "Stolgorny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluGornego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
						{
							int tmp = this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
						//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktMoje -= 5;
							if(tmp <0)	Informacjeorozgrywce.instancja.pktMoje += -tmp;
						}

						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
						{
							int tmp = this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
						//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktPrzec -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktPrzec += -tmp;
						}
					}
				}
			}

			if (pk.miejscenakarteRodzic.name == "Stolsrodkowy")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluSrodkowego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
						{
							int tmp = this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
						//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktMoje -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktMoje += -tmp;
						}

						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
						{
							int tmp = this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
						//	Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktPrzec -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktPrzec += -tmp;
						}
					}
				}
			}

			if (pk.miejscenakarteRodzic.name == "Stoldolny")
			{
				for (int i = 0; i < Informacjeorozgrywce.instancja.PozycjaStoluDolnego.childCount; i++)
				{
					obiekt = this.gameObject.transform.GetChild(i);
					if (obiekt.name == pk.name && i > 0) //dodaje na lewe dziecko
					{
						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Moja)
						{
							int tmp = this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
							//Debug.Log(tmp);
							Informacjeorozgrywce.instancja.pktMoje -= 5;
							if (tmp < 0) Informacjeorozgrywce.instancja.pktMoje += -tmp;
						}

						if (this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().strona == Informacjeokarcie.Strona.Przec)
						{
							int tmp = this.gameObject.transform.GetChild(i - 1).GetComponent<Informacjeokarcie>().zycie -= 5;
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
		///

		///Powrót jeśli miejsce = powrot
		if (pk != null)
		{
			//Debug.Log("POLOZENIE");
			pk.powrotdorodzica = this.transform;
		}
		///
	}
}

