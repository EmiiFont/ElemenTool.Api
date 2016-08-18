namespace ElemenTool.CacheLayer.Infrastructure
{
    internal class IssueSystemFields
    {
        public IssueSystemFields()
        {
        }

        public string AssignedTo { get; internal set; }
        public string Description { get; internal set; }
        public string LastUpdated { get; internal set; }
        public string Priority { get; internal set; }
        public string Product { get; internal set; }
        public string Severity { get; internal set; }
        public string Status { get; internal set; }
        public string SubmittedBy { get; internal set; }
        public string SubmittedIn { get; internal set; }
        public string Title { get; internal set; }
        public string Version { get; internal set; }
    }
}