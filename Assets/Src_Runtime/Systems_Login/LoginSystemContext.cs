using System;

namespace GJ {

    public class LoginSystemContext {
        public SystemStatus status;
        public LoginSystemEvents events;

        // ==== External ====
        public AssetModule assetModule;
        public UICore uiCore;

        public LoginSystemContext() {
            events = new LoginSystemEvents();
        }

    }

}