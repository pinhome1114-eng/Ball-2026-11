using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private InputAction moveAction;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    
    public TextMeshProUGUI winText;
    public int targetScore = 4;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
    }
    
    private void Start()
    {
        UpdateScoreUI();
        winText.gameObject.SetActive(false);

    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);

        rb.AddForce(movement * speed);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            score++;
            
            UpdateScoreUI();
            CheckWinCondition();

            Debug.Log("Score: " + score);

            other.gameObject.SetActive(false);
        }
    }
    
    private void CheckWinCondition()
    {
        if (score >= targetScore)
        {
            winText.gameObject.SetActive(true);
        }
    }
    
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
