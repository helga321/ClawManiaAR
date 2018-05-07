using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	GameObject currentContent;

	public ContentSpawner contentSpawner;

	void Start() {
		joystick.claw = claw;
        dragView.dragRot = viewRoot;
		BackPress ();
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
        int ticket = PlayerPrefs.GetInt("TicketClawManiaAR", 0);

        ticket += contentPrice; //Example
        PlayerPrefs.SetInt("TicketClawManiaAR", ticket);
        Debug.Log("You have sold one of your collection \n you got " + contentPrice + " Ticket");
        Debug.Log("Your have " + ticket + " Tickets");
    }

    public void CloseContent () {
        CloseUI();
        Destroy(viewRoot.GetChild(0).gameObject);
    }

	void Update() {
	}
}
