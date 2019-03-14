namespace StudyOOP.Common
{
    public static class InstanceIDs
    {
        public static InstanceID EmployeeConverter => new InstanceID(nameof(EmployeeConverter), "StudyOOP.Convert.WXXX5555ToEmployeeTSVConverter");

        public static InstanceID ItemConverter => new InstanceID(nameof(ItemConverter), "StudyOOP.Convert.WXXX6666ToItemTSVConverter");
    }
}
