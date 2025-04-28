
using UnityEngine;

namespace Client.Scripts.LogicViews
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private GameObject _ballObject;
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(_ballObject.transform.position.x, transform.position.y, _ballObject.transform.position.z - 5), Time.deltaTime * _speed);
        }
    }
}
