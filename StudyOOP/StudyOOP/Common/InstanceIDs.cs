namespace StudyOOP.Common
{
    public static class InstanceIDs
    {
        public static InstanceID EmployeeConverter =>
            new InstanceID(nameof(EmployeeConverter));

        public static InstanceID ItemConverter =>
            new InstanceID(nameof(ItemConverter));
    }
}
