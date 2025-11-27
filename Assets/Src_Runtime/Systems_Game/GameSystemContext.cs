using System;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine.Video;


namespace GJ {

    public class GameSystemContext {

        public SystemStatus status;
        public GameSystemEvents events;

        // ==== External ====
        public UICore ui;
        public AssetModule assetModule;
        public InputModule inputModule;
        public RoleRepository roleRepository;
        public PropRepository propRepository;
        public UserEntity userEntity;
        public GameEntity gameEntity;

        // ==== Internal ====

        public GameSystemContext() {
            events = new GameSystemEvents();
        }

        public RoleEntity Get_Owner() {
            UniqueID ownerID = gameEntity.ownerID;
            roleRepository.TryGet(ownerID, out RoleEntity role);
            return role;
        }

    }
}