using System.Collections;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f; // Скорость перемещения монстра
    [SerializeField] private float detectionRadius = 5.0f; // Радиус обнаружения игрока
    [SerializeField] private Transform player; // Ссылка на трансформ игрока
    [SerializeField] private float retreatDistance = 5.0f; // Расстояние для отступления при смерти игрока
    [SerializeField] private float retreatSpeed = 3.0f; // Скорость отступления

    private bool isPlayerNearby = false; // Флаг, указывающий, находится ли игрок рядом
    private bool isPlayerDead = false; // Флаг, указывающий, мертв ли игрок



    IEnumerator update() {
        while(true) {
            if (isPlayerDead)
            {
                RetreatFromPlayer();
            }
            else if (isPlayerNearby && player != null)
            {
                MoveTowardsPlayer();
            }
            yield return new WaitForSeconds(0.005f);
        }
    }

    private void MoveTowardsPlayer()
    {
        // Рассчитываем направление к игроку
        Vector3 direction = (player.position - transform.position).normalized;
        
        // Перемещаем монстра в сторону игрока
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void RetreatFromPlayer()
    {
        if (player != null)
        {
            // Рассчитываем направление от игрока
            Vector3 direction = (transform.position - player.position).normalized;
            
            // Перемещаем монстра в сторону от игрока
            transform.position += direction * retreatSpeed * Time.deltaTime;

            // Проверяем, достиг ли монстр достаточного расстояния от игрока
            if (Vector3.Distance(transform.position, player.position) >= retreatDistance)
            {
                // Возвращаемся в исходное состояние
                isPlayerDead = false;
                isPlayerNearby = false;
                player = null; // Сбрасываем ссылку на игрока
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isPlayerNearby = true;
            StartCoroutine(update());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            StopCoroutine(update());
        }
    }

    public void NotifyPlayerDeath()
    {
        // Метод для уведомления монстра о смерти игрока
        isPlayerDead = true;
    }
}
