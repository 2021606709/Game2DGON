using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BurnerBeetleAI : MonoBehaviour
{
    [SerializeField] private BossBurnerBeeStatsSO stats;
    private IBossState currentState;

    private Transform player;
    private Vector3 startPosition;
    private float stateTimer;
    private Rigidbody2D rb;

    public Rigidbody2D Rb => rb;
    public Transform Player => player;
    public BossBurnerBeeStatsSO Stats => stats;
    public Vector3 StartPosition => startPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        if(player == null || stats == null || rb == null)
        {
            Debug.LogError("BurnerBeetleAI: Missing references. Please check the inspector.");
            enabled = false;
            return;
        }

        SwitchState(new BossPatrolState(this));
        //Debug.Log("Initial State: " + currentState.GetType().Name);
    }

    void Update()
    {
        stateTimer += Time.deltaTime;
        currentState?.Update();
        //Debug.Log("Current State: " + currentState.GetType().Name + ", Elapsed Time: " + stateTimer);
    }

    void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void SwitchState(IBossState newState)
    {
        currentState?.Exit();
        currentState = newState;
        stateTimer = 0f;
        currentState?.Enter();
    }

    public float GetStateElapsedTime()
    {
        return stateTimer;
    }
    public void FlipToward(Vector3 target)
    {
        float direction = target.x - transform.position.x;

        if ((direction > 0 && transform.localScale.x < 0) ||
            (direction < 0 && transform.localScale.x > 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
