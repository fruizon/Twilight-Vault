using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;             // Скорость передвижения персонажа
    public float jumpForce = 10f;        // Сила прыжка
    public LayerMask groundLayer;        // Слой, определяющий что является землей
    public Transform groundCheck;        // Точка для проверки соприкосновения с землей
    public float smoothing = 0.05f;      // Параметр сглаживания скорости

    public ParticleSystem dustParticle;  // Particle System для эффекта пыли при приземлении
    public ParticleSystem runParticle;   // Particle System для эффекта пыли при беге
    public float particlesPerSecond = 10f; // Количество частиц в секунду для бега

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool wasGrounded;            // Переменная для хранения предыдущего состояния приземления
    private float groundCheckRadius = 0.2f;
    private Vector2 velocity = Vector2.zero;
    private bool facingRight = true;     // Определяет, куда "смотрит" персонаж
    private bool isRunning;              // Переменная для проверки, идет ли движение

    private float timeSinceLastEmit;


    public bool isDie = false;
    private bool hasLandedAfterDeath = false; // Флаг для проверки, приземлился ли персонаж после смерти

    private float moveInput;             // Переменная для хранения значения движения

    [SerializeField] private CheckPointsController checkPointsController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();  // Инициализация компонента Animator
        timeSinceLastEmit = 0f;
        animator.SetBool("isDie", false);
        moveInput = 0f; // Изначально персонаж не движется

        if (PlayerPrefs.GetInt("isWathed") == 1) {
            if (PlayerPrefs.GetInt("last check point") >= 0) {
                gameObject.transform.position = checkPointsController.getPoint();
            }
            PlayerPrefs.SetInt("isWathed", 0);
        }
    }

    void FixedUpdate()
    {
        if (isDie)
        {
            // Логика смерти
            return;
        }

        // Проверка, находится ли персонаж на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        // Проверка наличия стены сбоку
        bool isTouchingWall = Physics2D.Raycast(transform.position, new Vector2(Mathf.Sign(moveInput), 0), 0.1f, groundLayer);

        // Целевая скорость для персонажа
        Vector2 targetVelocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Если персонаж касается стены, уменьшите горизонтальное движение
        if (isTouchingWall && !isGrounded)
        {
            targetVelocity.x = 0;
        }

        // Применение сглаживания скорости
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothing);

        // Управление анимацией скорости
        animator.SetFloat("Speed", Mathf.Abs(moveInput));  // Передача скорости в Animator

        // Поворот персонажа в сторону движения
        if (moveInput > 0 && !facingRight && !isTouchingWall)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight && !isTouchingWall)
        {
            Flip();
        }

        // Проверка на приземление
        if (isGrounded && !wasGrounded)
        {
            CreateDust();
        }

        // Управление эффектом бега
        if (isGrounded && Mathf.Abs(moveInput) > 0)
        {
            if (!isRunning)
            {
                StartRunningDust();
            }
            else
            {
                timeSinceLastEmit += Time.fixedDeltaTime;
                if (timeSinceLastEmit >= 1f / particlesPerSecond)
                {
                    runParticle.Emit(1); // Создает одну частицу за раз
                    timeSinceLastEmit = 0f;
                }
            }
        }
        else
        {
            if (isRunning)
            {
                StopRunningDust();
            }
        }

        // Обновляем значение wasGrounded
        wasGrounded = isGrounded;
    }

    // Метод для начала движения влево
    public void MoveLeft()
    {
        moveInput = Mathf.Max(moveInput - 1f, -1f);
    }

    // Метод для начала движения вправо
    public void MoveRight()
    {
        moveInput = Mathf.Min(moveInput + 1f, 1f);
    }

    // Метод для остановки движения
    public void StopMoving()
    {
        moveInput = 0f;
    }

    // Метод для прыжка
    public void Jump()
    {
        if (isGrounded) // Прыжок только если персонаж на земле
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Flip()
    {
        // Меняем направление движения персонажа
        facingRight = !facingRight;
        // Инвертируем масштаб по оси X, чтобы повернуть персонажа
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void CreateDust()
    {
        // Запуск эффекта пыли при приземлении
        dustParticle.Play();
    }

    void StartRunningDust()
    {
        // Запуск эффекта пыли при беге
        runParticle.Play();
        isRunning = true;
    }

    void StopRunningDust()
    {
        // Остановка эффекта пыли при беге
        runParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        isRunning = false;
    }

    void HandleDeath()
    {
        // Останавливаем движение и анимации при смерти
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1; // Включаем гравитацию
        animator.SetBool("isDie", true);
        StopRunningDust();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулся ли персонаж с объектом, у которого тег "Monster"
        if (collision.collider.CompareTag("Monster"))
        {
            MonsterAI monsterAI = collision.gameObject.GetComponent<MonsterAI>();
            if (monsterAI != null) {
                isDie = true;
                HandleDeath();
                monsterAI.NotifyPlayerDeath();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Визуализация groundCheck для удобства настройки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
