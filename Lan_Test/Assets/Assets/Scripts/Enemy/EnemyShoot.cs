using UnityEngine;

// Клас, що реалізує логіку стрільби ворожих об'єктів
public class EnemyShoot : MonoBehaviour
{
    // Швидкість руху снаряду
    public float speed = 2f;

    // Завдане пошкодження об'єктом
    public float entityDamage = 1f;

    // Змінна для визначення напрямку стрільби
    public ToWeapon tw;

    // Компонент тіла снаряду
    private Rigidbody2D rb;

    // Тег стрілець
    public string shooterTag = "Enemy";

    // Ініціалізація
    void Start()
    {
        // Отримання компонента Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Знаходження об'єкта гравця
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Перевірка, чи гравець знайдений
        if (player != null)
        {
            // Визначення напрямку до гравця та нормалізація
            Vector2 playerPosition = player.transform.position;
            Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
            Vector2 directionOther = (direction * 1000).normalized;

            // Задання швидкості руху снаряду
            rb.velocity = directionOther * speed;

            // Визначення кута повороту снаряду
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // Виклик методу для автоматичного знищення снаряду через 4 секунди
        Invoke("DestroyTime", 4f);
    }

    // Обробка зіткнення снаряду
    private void OnCollisionStay2D(Collision2D other)
    {
        // Перевірка, чи об'єкт не має тегу стрільця
        if (other.gameObject.CompareTag(shooterTag))
        {
            return;
        }

        // Отримання тегу іншого об'єкта та його здоров'я
        string entityTag = other.gameObject.tag;
        Health health = other.gameObject.GetComponent<Health>();

        // Перевірка, чи існує компонент здоров'я та зменшення здоров'я об'єкта
        if (health != null)
        {
            health.Reduce((int)entityDamage, health.currentHealth);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Метод для автоматичного знищення снаряду через певний час
    void DestroyTime()
    {
        Destroy(gameObject);
    }
}
