using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ElemenTool.Api.com.elementool.www;

namespace ElemenTool.CacheLayer.Entities
{
    public class IssueDetails
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Indexed]
        public int IssueNumber { get; set; }
        public string Symbol { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StepstoReproduce { get; set; }
        public string Product   {get; set;}
        public string Status    {get; set;}
        public string Originalissue  {get; set;}
        public string Versionfoundin  {get; set;}
        public string Priority  {get; set;}
        public string QAAssigned      {get; set;}
        public string Submittedby     {get; set;}
        public string DevAssigned     {get; set;}
        public string Versionfixedin  {get; set;}
        public string Remarks   {get; set;}
        public string Customer  {get; set;}
        public string CaseContact    {get; set;}
        public string Productperson   {get; set;}
        public string FixType   {get; set;}
        public string CC  {get; set;}
        public string TimeSpent {get; set;}
        public string SLADeliveryDate      {get; set;}
        public string PriorityListPosition  {get; set;}
        public string ServicePack     {get; set;}
        public string BuildNumber     {get; set;}
        public string AssignedPriority      {get; set;}
        public string DevTimeRem {get; set;}
        public string QATimeRem  {get; set;}
        public string DevCompDate     {get; set;}
        public string DaysSinceIssueCreated {get; set;}
        public string QACompDate      {get; set;}

       
    }
}
