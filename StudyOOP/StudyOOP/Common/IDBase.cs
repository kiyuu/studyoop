namespace StudyOOP.Common
{
    public abstract class IDBase
    {
        protected IDBase(string id)
        {
            this.Id = id;
        }

        public string Id { get; }

        public override string ToString() => this.Id;

        public override bool Equals(object obj) => obj != null && this.Id == obj.ToString();

        public override int GetHashCode() => this.Id.GetHashCode();
    }
}
