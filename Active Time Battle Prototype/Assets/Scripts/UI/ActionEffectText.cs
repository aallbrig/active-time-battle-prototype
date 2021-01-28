using System.Collections;
using UnityEngine;

namespace UI
{
    public class ActionEffectText : MonoBehaviour
    {
        public float textLifetime = 3.0f;
        public float textSpeed = 100.0f;
        private Transform _transform;

        private IEnumerator ActionEffectTextCoroutine()
        {
            yield return new WaitForSeconds(textLifetime);
            Destroy(gameObject);
        }

        private void Update()
        {
            _transform.position += Vector3.up * (textSpeed * Time.deltaTime);
        }

        private void Start()
        {
            _transform = transform;
            StartCoroutine(ActionEffectTextCoroutine());
        }
    }
}
