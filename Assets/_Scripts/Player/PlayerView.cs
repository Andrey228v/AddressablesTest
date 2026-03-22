using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        private string _playerName = "test";

        private void Start()
        {
            Debug.Log("PlayerInit");
        }

        public void SetName(string newName)
        {
            _playerName = newName;
        }
    }
}
