using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OblsugaMenu : MonoBehaviour
{
	public GameObject Obiekty3diswiatlo;
	public Canvas MenuWyjsciowe; //ramka
	public Canvas MenuOpcji;
	public Canvas Gra;
	public Button start; //przycisk
	public Button opcje;
	public Button Pomocprzyciks;
	public Button wyjscie;

	public Canvas menuGUI;
	public Canvas Pomocramka;

	public static OblsugaMenu instancja5;

	void Awake()
	{
		instancja5 = this;
	}

	// Use this for initialization
	void Start()
	{
		menuGUI = (Canvas)GetComponent<Canvas>(); //pobranie
		MenuWyjsciowe = MenuWyjsciowe.GetComponent<Canvas>();
		MenuOpcji = MenuOpcji.GetComponent<Canvas>();
		Pomocramka = Pomocramka.GetComponent<Canvas>();
		Gra = Gra.GetComponent<Canvas>();
		start = start.GetComponent<Button>();
		opcje = opcje.GetComponent<Button>();
		wyjscie = wyjscie.GetComponent<Button>();
		Pomocprzyciks = Pomocprzyciks.GetComponent<Button>();
		MenuWyjsciowe.enabled = false;
		MenuOpcji.enabled = false;
		Gra.enabled = false;
		Pomocramka.enabled = false;
		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			menuGUI.enabled = true;//!menuGUI.enabled;

			if (menuGUI.enabled)
			{
				Time.timeScale = 0; //zatrzymanie czasu
				MenuWyjsciowe.enabled = false;
				MenuOpcji.enabled = false;
				start.enabled = true;
				wyjscie.enabled = true;
			}
			else
			{
				Time.timeScale = 1;
				menuGUI.enabled = false;
			}
		}
	}

	public void PrzyciskStart()
	{
		Pomocramka.enabled = false;
		Gra.enabled = true;
		menuGUI.enabled = false; //wylaczenie ramek z menu 
		Time.timeScale = 1;
	}

	public void PrzyciskOpcje()
	{
		Pomocramka.enabled = false;
		MenuOpcji.enabled = true;
		MenuWyjsciowe.enabled = false;
		start.enabled = true;
		//	wyjscie.enabled = true;
	}

	public void PrzyciskWyjscie()
	{
		Pomocramka.enabled = false;
		MenuWyjsciowe.enabled = true;
		start.enabled = true;
		wyjscie.enabled = true;
	}

	public void PrzyciskTAK()
	{
		Application.Quit();
	}

	public void PrzyciskNIE()
	{
		MenuWyjsciowe.enabled = false;
		start.enabled = true;
		wyjscie.enabled = true;
	}

	public void PrzyciskPowrot()
	{
		MenuOpcji.enabled = false;
		start.enabled = true;
		wyjscie.enabled = true;
	}

	public void PrzyciskPomoc()
	{
		menuGUI.enabled = false;
		Gra.enabled = false;
		Pomocramka.enabled = true;
		start.enabled = true;
		wyjscie.enabled = true;
	}

}
