namespace StudyOOP.Common
{
    using StudyOOP.Convert;

    public static class InstanceIDs
    {
        public static InstanceID<FlatFileToTsvConverterBase> EmployeeConverter =>
            new InstanceID<FlatFileToTsvConverterBase>(nameof(EmployeeConverter));

        public static InstanceID<FlatFileToTsvConverterBase> ItemConverter =>
            new InstanceID<FlatFileToTsvConverterBase>(nameof(ItemConverter));
    }
}
