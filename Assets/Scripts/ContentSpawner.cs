using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpawner : MonoBehaviour {

	public GameObject[] contentPrefab;
	public Transform contentRoot;
	public List<GameObject> contentList;
	public int maxContent;

	void Start() {
		contentList = new List<GameObject> ();
	}

	public void CheckContents() {
		StopAllCoroutines ();
		StartCoroutine (SpawnContents());
	}

	IEnumerator SpawnContents() {
		while (contentList.Count < maxContent) {
			int rand = Random.Range(0,contentPrefab.Length);
			GameObject g = Instantiate(contentPrefab[rand],contentRoot);
			contentList.Add(g);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void DestroyContent(GameObject target) {
		contentList.Remove (target);
		Destroy (target);
	}
}
