using UnityEngine;

public class BirdBezierLoop : MonoBehaviour
{
    public Transform point0; // Начальная точка
    public Transform point1; // Управляющая точка 1
    public Transform point2; // Управляющая точка 2
    public Transform point3; // Конечная точка

    public float speed = 1.0f; // Скорость движения
    private float t = 0.0f; // Параметр времени для кривой
    private bool isReversing = false; // Флаг для обратного движения

    void Update()
    {
        // Движение вперёд или назад по кривой
        if (!isReversing)
        {
            t += Time.deltaTime * speed;
            if (t >= 1.0f)
            {
                t = 1.0f;
                isReversing = true; // Начинаем движение в обратном направлении
            }
        }
        else
        {
            t -= Time.deltaTime * speed;
            if (t <= 0.0f)
            {
                t = 0.0f;
                isReversing = false; // Начинаем движение вперёд
            }
        }

        // Вычисление положения на кривой Безье
        transform.position = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
    }

    // Функция вычисления точки на кривой Безье
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // (1-t)^3 * p0
        p += 3 * uu * t * p1; // 3*(1-t)^2*t * p1
        p += 3 * u * tt * p2; // 3*(1-t)*t^2 * p2
        p += ttt * p3; // t^3 * p3

        return p;
    }
}
