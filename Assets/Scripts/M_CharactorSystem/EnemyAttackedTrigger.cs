using System.Collections;
using System.Collections.Generic;
using M_Factory;
using UnityEngine;

namespace M_CharactorSystem
{
    public class EnemyAttackedTrigger : MonoBehaviour
    {
        private ICharactor _charactor;
        private int _id;

        private void Start()
        {
            _id = transform.parent.gameObject.GetInstanceID();
            _charactor = MainFactory.Instance.GetCharactorFactory().GetEnemy(_id);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Sword"))
            {
                var id = other.transform.parent.gameObject.GetInstanceID();
                _charactor.UnderAttack(MainFactory.Instance.GetCharactorFactory().GetPlayer(id));
            }
        }
    } 
} 
