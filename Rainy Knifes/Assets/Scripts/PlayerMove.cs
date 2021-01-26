using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sr;

    private float speed = 3f;

    private float min_X = -2.7f;
    private float max_X = 2.7f;

    public Text timer_Text;
    private int timer;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}

    void Start() {
        Time.timeScale = 1f;
        StartCoroutine(CountTime());
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        PlayerBounds();
	}

    void PlayerBounds() {

        Vector3 temp = transform.position;

        if(temp.x > max_X) {

            temp.x = max_X;

        } else if(temp.x < min_X) {

            temp.x = min_X;
        }

        transform.position = temp;

    }

	void Move() {

        float h = Input.GetAxisRaw("Horizontal");

        Vector3 temp = transform.position;

        if(h > 0) {
            // going to the right side

            temp.x += speed * Time.deltaTime;
            sr.flipX = false;

            anim.SetBool("Walk", true);

        } else if(h < 0) {

            temp.x -= speed * Time.deltaTime;
            sr.flipX = true;

            anim.SetBool("Walk", true);

        } else if(h == 0) {

            anim.SetBool("Walk", false);
        }

        transform.position = temp;

	}

    IEnumerator RestartGame() {
        yield return new WaitForSecondsRealtime(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    IEnumerator CountTime() {

        yield return new WaitForSeconds(1f);
        timer++;

        timer_Text.text = "Timer: " + timer;

        StartCoroutine(CountTime());
    }

	void OnTriggerEnter2D(Collider2D target) {
		
        if(target.tag == "Knife") {
            Time.timeScale = 0f;

            StartCoroutine(RestartGame());
        }

	}

} // class































