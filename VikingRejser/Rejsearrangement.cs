//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VikingRejser
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rejsearrangement
    {
        public int id { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int price { get; set; }
        public Nullable<int> maxPeople { get; set; }
        public string transporter { get; set; }
        public string description { get; set; }
        public Nullable<int> signedUp { get; set; }
    }
}