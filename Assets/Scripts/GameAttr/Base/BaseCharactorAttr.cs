using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAttr.Base
{
    public class BaseCharactorAttr : BaseAttr
    {
        private readonly float _walkSpeed;
        private readonly float _runSpeed;
        private readonly float _jumpHeight;
        private readonly string _assetName;
        private readonly int _maxHp;

        public BaseCharactorAttr(float walkSpeed, float runSpeed, float jumpHeight, string assetName, int maxHp)
        {
            _walkSpeed = walkSpeed;
            _runSpeed = runSpeed;
            _jumpHeight = jumpHeight;
            _assetName = assetName;
            _maxHp = maxHp;
        }

        public override float GetWalkSpeed()
        {
            return _walkSpeed;
        }

        public override float GetRunSpeed()
        {
            return _runSpeed;
        }

        public override float GetJumpHeight()
        {
            return _jumpHeight;
        }

        public override string GetAssetName()
        {
            return _assetName;
        }

        public override int GetMaxHp()
        {
            return _maxHp;
        }
    }
} 
