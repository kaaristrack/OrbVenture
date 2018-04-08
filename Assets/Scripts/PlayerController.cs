using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed = 100;
    public Rigidbody rb;
	private int count;
	// count is a variable that keeps track of the score!
	public Text Count;
	public Text Win;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		count = 0;
		SetText ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);

	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "item")
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetText();
        }
        if (other.gameObject.tag == "hazard")
        {
            other.gameObject.SetActive(false);
            Vector3 jump = new Vector3(0.0f, 10.0f, 0.0f);
            rb.velocity = jump;
			Win.text = "You suck!";
            StartCoroutine(NewGame());
		}
	}

	void SetText() {
		Count.text = "Count: " + count.ToString();
		Win.text = "";
		if (count >= 3) {
			Win.text = "You win!";
            StartCoroutine(NewGame());
		}
	}

	IEnumerator NewGame () {
		yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

