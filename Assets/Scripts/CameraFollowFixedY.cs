using UnityEngine;

public class CameraFollowFixedY : MonoBehaviour
{
    public Transform target; // Целевой объект (персонаж)
    public float smoothSpeed = 0.125f; // Скорость сглаживания
    public Vector3 offset; // Смещение камеры относительно целевого объекта
    public float fixedY; // Фиксированное значение по оси Y

    private void Start()
    {
        // Устанавливаем фиксированное значение Y на старте
        fixedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Определение желаемой позиции камеры
        Vector3 desiredPosition = target.position + offset;

        // Плавное перемещение камеры к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Сохранение фиксированного значения по оси Y
        smoothedPosition.y = fixedY;

        // Обновление позиции камеры
        transform.position = smoothedPosition;
    }
}
