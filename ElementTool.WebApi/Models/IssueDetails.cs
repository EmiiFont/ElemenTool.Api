using ElementTool.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElemenTool.CacheLayer.Entities
{
    public class IssueDetails
    {

        public int Id { get; set; }
        public int IssueNumber { get; set; }
        public string Symbol { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StepstoReproduce { get; set; }
        public string Product   {get; set;}
        //public string Status    {get; set;}
        //public string Originalissue  {get; set;}
        //public string Versionfoundin  {get; set;}
        //public string Priority  {get; set;}
        //public string QAAssigned      {get; set;}
        //public string Submittedby     {get; set;}
        //public string DevAssigned     {get; set;}
        //public string Versionfixedin  {get; set;}
        //public string Remarks   {get; set;}
        //public string Customer  {get; set;}
        //public string CaseContact    {get; set;}
        //public string Productperson   {get; set;}
        //public string FixType   {get; set;}
        //public string CC  {get; set;}
        //public string TimeSpent {get; set;}
        //public string SLADeliveryDate      {get; set;}
        //public string PriorityListPosition  {get; set;}
        //public string ServicePack     {get; set;}
        //public string BuildNumber     {get; set;}
        //public string AssignedPriority      {get; set;}
        //public string DevTimeRem {get; set;}
        //public string QATimeRem  {get; set;}
        //public string DevCompDate     {get; set;}
        //public string DaysSinceIssueCreated {get; set;}
        //public string QACompDate      {get; set;}


        private List<IssueDetailsFields> _fields = new List<IssueDetailsFields>();
        public List<IssueDetailsFields> Fields
        {
            get
            {
                return _fields;
            }

            set
            {
                if (_fields == value)
                {
                    return;
                }
                _fields = value;
            }
        }

        private List<IssueHistory> _history = new List<IssueHistory>();

        public List<IssueHistory> History
        {
            get
            {
                return _history;
            }

            set
            {
                if (_history == value)
                {
                    return;
                }
                _history = value;
            }
        }

        private List<IssueRemarks> _remarks = new List<IssueRemarks>();

        public List<IssueRemarks> Remarks
        {
            get
            {
                return _remarks;
            }

            set
            {
                if (_remarks == value)
                {
                    return;
                }
                _remarks = value;
            }
        }
    }

    public class IssueDetailsFields
    {
        public string Value { get; set; }
        public  string  Caption { get; set; }
        public string SystemName { get; set; }
        public string[] OptionList { get; internal set; }
    }
}
