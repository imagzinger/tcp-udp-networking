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
        private float angleSpeed = 120f;
        private float speed = 2f;
        float _inputAngle = 0f;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new bool[6];
        }

        /// <summary>Processes player input and moves the player.</summary>
        public void Update()
        {
            if (inputs[0])//W
            {
                speed += 1f;

                //if (inputs[3])//D
                //    _inputAngle += angleSpeed;
                //if (inputs[1])//A
                //    _inputAngle -= angleSpeed;
            }
            else if (inputs[2])//S
            {
                speed -= 4f;

                //if (inputs[3])//D
                //    _inputAngle -= angleSpeed;
                //if (inputs[1])//A
                //    _inputAngle += angleSpeed;
            }
            else
            {
                if (speed > 0)
                    speed -= 1f;
                if (speed < 0)
                    speed += 1f;
            }
            //_inputDirection /= 2;
            _inputDirection = MathF.Atan(speed/100);
            
            Move(_inputDirection, _inputAngle);
        }
        /* 
           public void Update()
           {
           Vector2 _inputDirection = Vector2.Zero;
           if (inputs[0])//W
           {
               _inputDirection.Y += 1;
           }
           if (inputs[1])//A
           {
               //rotate left
               _inputDirection.X += 1;
           }
           if (inputs[2])//S
           {
               _inputDirection.Y -= 1;
           }
           if (inputs[3])//D
           {
               //rotate right
               _inputDirection.X -= 1;
           }

           if (inputs[4])//SPACE
           { 
               //jump noises
               //add a force?
           }

           Move(_inputDirection);
           }
           */
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
