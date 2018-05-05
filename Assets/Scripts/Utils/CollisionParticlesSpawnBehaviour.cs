using Core.ObjectPooling;
using UnityEngine;

namespace Utils
{
    public class CollisionParticlesSpawnBehaviour : MonoBehaviour
    {
        private Rigidbody2D _body2D;

        public GameObject collisionParticles;
        public float particleScaleMultiplier;

        private void Awake()
        {
            PoolManager.Instance.CreatePool(collisionParticles, 20);
            _body2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var parts = PoolManager.Instance.ReuseObject(collisionParticles, col.contacts[0].point, Quaternion.identity);
            parts.transform.localScale *= _body2D.velocity.magnitude * particleScaleMultiplier;
            parts.transform.LookAt((Vector2)gameObject.transform.position);
        }
    }

}
