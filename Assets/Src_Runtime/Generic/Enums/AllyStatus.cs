namespace GJ {

    public enum AllyStatus {
        None,
        Player,
        Computer,
        Neutral,
    }

    public static class AllyStatusExtension {

        public static bool IsEnemy(this AllyStatus status, AllyStatus other) {
            if (status == AllyStatus.Player && other == AllyStatus.Computer) {
                return true;
            }
            if (status == AllyStatus.Computer && other == AllyStatus.Player) {
                return true;
            }
            return false;
        }
    }

}