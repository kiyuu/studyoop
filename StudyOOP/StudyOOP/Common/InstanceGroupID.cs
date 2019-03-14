namespace StudyOOP.Common
{
    public class InstanceGroupID : IDBase
    {
        public InstanceGroupID(string id, InstanceID[] instanceIds)
            : base(id)
        {
            this.InstanceIds = instanceIds;
        }

        public InstanceID[] InstanceIds { get; }
    }
}
