using System;

namespace GJ {

    public class UserIDComponent {

        int role;
        int prop;

        public UserIDComponent() { }

        public UniqueID Role() => new UniqueID(EntityType.Role, ++role);
        public UniqueID Prop() => new UniqueID(EntityType.Prop, ++prop);
    }

}