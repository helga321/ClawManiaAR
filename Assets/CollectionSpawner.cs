using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSpawner : MonoBehaviour {
    public Transform collections;
    public GameObject[] contents;
    public GameControl gameControl;
    public Transform[] collectionsPos;

    //private CollectionClick collectionClick;

    private void Awake () {
        collectionsPos = collections.GetComponentsInChildren<Transform>();

        for (int i = 0; i < collectionsPos.Length - 1;i++) {
            int index = i;
            contents[i].SetActive(false);
            GameObject content = Instantiate(contents[i], collectionsPos[index+1].position, collectionsPos[index + 1].rotation);
            content.transform.localScale = collectionsPos[index + 1].localScale;
            content.GetComponentInChildren<Rigidbody>().useGravity = false;
            content.GetComponentInChildren<Collider>().isTrigger = true;
            content.transform.SetParent(collectionsPos[index+1]);

            content.transform.GetChild(0).gameObject.AddComponent<CollectionClick>();
            content.transform.GetChild(0).gameObject.GetComponent<CollectionClick>().gameControl = gameControl;
            content.SetActive(true);
        }
    }
}
