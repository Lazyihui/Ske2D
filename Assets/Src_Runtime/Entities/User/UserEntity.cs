using System;
using UnityEngine;

namespace GJ {

    public class UserEntity {

        public UserIDComponent userIDComponent;

        public UserEntity() {
            userIDComponent = new UserIDComponent();
        }

    }
}