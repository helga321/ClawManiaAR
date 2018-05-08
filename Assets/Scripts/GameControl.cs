using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public Claw claw;
	public Joystick joystick;
    public DragView dragView;
    public int contentPrice;

	public GameObject startButton;
	public GameObject backButton;
	public GameObject grabButton;
	public GameObject okButton;
	public GameObject youGotText;
	public GameObject celebration;
    public GameObject sellButton;
    public GameObject closeButton;
	public Transform rotateRoot;
    public Transform viewRoot;
    public Text textCoin;
    public Text textTicket;
	GameObject currentContent;

	public ContentSpawner contentSpawner;

	void Start() {
		joystick.claw = claw;
        dragView.dragRot = viewRoot;
		BackPress ();
        PrintCoin();
        PrintTicket();
	}

	public void BackPress() {
		startButton.SetActive (true);
		backButton.SetActive (false);
		grabButton.SetActive (false);
		okButton.SetActive (false);
		youGotText.SetActive (false);
		joystick.gameObject.SetActive (false);
        dragView.gameObject.SetActive(false);
		celebration.SetActive (false);
        sellButton.SetActive(false);
        closeButton.SetActive(false);
	}
	
	public void StartPress() {
		startButton.SetActive (false);
		backButton.SetActive (true);
		grabButton.SetActive (true);
		okButton.SetActive (false);
		youGotText.SetActive (false);
		joystick.gameObject.SetActive (true);
        dragView.gameObject.SetActive(false);
        sellButton.SetActive(false);
        closeButton.SetActive(false);

		contentSpawner.CheckContents ();
	}
	
	public void GrabPress() {
		claw.Grab ();
	}

    public void GetContent(GameObject content)
    {
        backButton.SetActive(false);
        grabButton.SetActive(false);
        okButton.SetActive(true);
        youGotText.SetActive(true);
        joystick.gameObject.SetActive(false);
        dragView.gameObject.SetActive(false);
        celebration.SetActive(true);
        sellButton.SetActive(false);
        closeButton.SetActive(false);
        content.GetComponentInChildren<Rigidbody>().isKinematic = true;
        content.GetComponentInChildren<Collider>().enabled = false;
        content.transform.SetParent(rotateRoot);
        content.transform.localPosition = Vector3.zero;
        content.transform.localEulerAngles = Vector3.zero;
        content.transform.localScale = Vector3.one;
        content.transform.GetChild(0).localPosition = Vector3.zero;
        content.transform.GetChild(0).localEulerAngles = new Vector3(-90f, 0f, 0f);

        currentContent = content;
    }

    public void ViewContent (GameObject content) {
        if (viewRoot.childCount > 0) {
            Destroy(viewRoot.GetChild(0).gameObject);
        }

        GameObject vContent = Instantiate(content, viewRoot.position, viewRoot.rotation);
        vContent.transform.SetParent(viewRoot);
        vContent.transform.localPosition = Vector3.zero;
        vContent.transform.localEulerAngles = Vector3.zero;
        vContent.transform.localScale = Vector3.one;

        backButton.SetActive(false);
        grabButton.SetActive(false);
        okButton.SetActive(false);
        youGotText.SetActive(false);
        joystick.gameObject.SetActive(false);
        dragView.gameObject.SetActive(true);
        celebration.SetActive(false);
        sellButton.SetActive(true);
        closeButton.SetActive(true);
    }

    public void CloseUI () {
        backButton.SetActive(true);
        grabButton.SetActive(true);
        okButton.SetActive(false);
        youGotText.SetActive(false);
        joystick.gameObject.SetActive(true);
        dragView.gameObject.SetActive(false);
        celebration.SetActive(false);
        sellButton.SetActive(false);
        closeButton.SetActive(false);
    }

	public void DestroyContent() {
        CloseUI();
		contentSpawner.DestroyContent (currentContent);
	}

    public void SellCollection () {
        GetTicket(contentPrice);
    }

    public void CloseContent () {
        CloseUI();
        Destroy(viewRoot.GetChild(0).gameObject);
    }

    public void GetCoin (int value) {
        int coin = PlayerPrefs.GetInt("CoinClawManiaAR", 0);

        coin += value; //Example
        PlayerPrefs.SetInt("CoinClawManiaAR", coin);
        Debug.Log("You got " + value + " Coin");
        Debug.Log("Your have " + coin + " Coin");
        PrintCoin();
    }

    public void GetTicket (int value) {
        int ticket = PlayerPrefs.GetInt("TicketClawManiaAR", 0);

        ticket += value; //Example
        PlayerPrefs.SetInt("TicketClawManiaAR", ticket);
        Debug.Log("You have sold one of your collection \n you got " + value + " Ticket");
        Debug.Log("Your have " + ticket + " Tickets");
        PrintTicket();
    }

    void PrintTicket () {
        int ticket = PlayerPrefs.GetInt("TicketClawManiaAR", 0);
        textTicket.text = ticket.ToString();
    }

    void PrintCoin () {
        int coin = PlayerPrefs.GetInt("CoinClawManiaAR", 0);
        textCoin.text = coin.ToString();
    }

	void Update() {
	}
}
