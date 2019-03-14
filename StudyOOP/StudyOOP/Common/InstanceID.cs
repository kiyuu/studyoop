namespace StudyOOP.Common
{
    public class InstanceID : IDBase
    {
        public InstanceID(string id, string className)
            : base(id)
        {
            this.ClassName = className;
        }

        public string ClassName { get; }
    }
}
