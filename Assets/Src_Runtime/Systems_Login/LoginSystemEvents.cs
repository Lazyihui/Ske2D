using System;

namespace GJ {

    public class LoginSystemEvents {
        public Action OnStartHandle;
        public void OnStart() => OnStartHandle.Invoke();

        public LoginSystemEvents() { }
    }
}