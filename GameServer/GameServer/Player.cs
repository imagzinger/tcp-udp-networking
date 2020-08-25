using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        private bool[] inputs;
        private float _inputDirection = 0f;

        private float maxSpeed = 2.5f;
        private float revSpeed = -1f;
        private float speed = 0f;
        private float acceleration = .05f;
        private float brake = .07f;
        private float drag = .01f;
        float _inputAngle = 0f;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new bool[6];
        }
        private void Drag()
        {
            if(speed > 0)
                speed -= drag;
            if (speed < 0)
                speed += drag;
        }
        private void Forward()
        {
            if (speed < maxSpeed)
            {
				if (speed < 0)
				{
                    speed += brake;
				}
                speed += acceleration;
            }

			if (inputs[3] || inputs[1])
			{
                //slow down when turning
	
                speed -= acceleration;
			}
		}
        private void Backward()
        {
            if (speed > revSpeed)
            {
                if (speed > 0)
				{
                    speed -= brake;
                }
                speed -= acceleration;
            }
            if (inputs[3] || inputs[1])
            {
                //if (speed > 5)
                //{
                //    speed -= acceleration;
                //}slow on turns but this is already braking so maybe ignore it?
            }
        }
        private void Coast()
        {
            //do nothing?
        }

        /// <summary>Processes player input and moves the player.</summary>
        public void Update()
        {
            if (inputs[0])//w is go forwards
            {
                Forward();
            }
            else if (inputs[2])//S is go backwards
            {
                Backward();
            }
            else// no input forward or backwards
            {
                Coast();
            }

            Drag();

            _inputDirection = speed;
            
            Move(_inputDirection, _inputAngle);
        }
        
        /// <summary>Calculates the player's desired movement direction and moves him.</summary>
        /// <param name="_inputDirection"></param>
        private void Move(float _inputDirection, float _inputAngle)
        {
            Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

            Vector3 _moveDirection = _forward * _inputDirection;
         
            position += _moveDirection * moveSpeed;
            rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, _inputAngle);

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        /*private void Move(Vector2 _inputDirection)
        {
            Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

            Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;
         
            position += _moveDirection * moveSpeed;
            // I will need something like this line in order to allow the A and D keys to TURN the car
            ///rotation += rotationAmount * rotation speed?
            //quaternions are strange so look up how this works...need new Quaternion each time?

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }*/

        /// <summary>Updates the player input with newly received input.</summary>
        /// <param name="_inputs">The new key inputs.</param>
        /// <param name="_rotation">The new rotation.</param>
        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
    }
}
