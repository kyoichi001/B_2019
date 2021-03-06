﻿using UnityEngine;

namespace KiliWare.SampleVRMApp
{
    public class TransformControllerRegister : MonoBehaviour
    {
        [SerializeField]
        protected float _rotationSpeed = 3;
        [SerializeField]
        protected float _moveSpeed = 3;

        void Awake()
        {
            var manager = GetComponent<VRMLoadManager>();
            manager.OnModelLoaded += RegisterController;
        }

        protected void RegisterController(GameObject loadedModel)
        {
            if (loadedModel == null)
            {
                Debug.LogError("The loaded model is null");
                return;
            }

            if (loadedModel.GetComponent<TransformController>() != null)
            {
                return;
            }

            loadedModel.AddComponent<Animation>();
            var controller = loadedModel.AddComponent<TransformController>();
            controller.SetSpeed(rotation: _rotationSpeed, move: _moveSpeed);
        }
    }
}