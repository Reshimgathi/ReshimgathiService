//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReshimgathiServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favourite
    {
        public System.Guid Id { get; set; }
        public System.Guid UserProfileId { get; set; }
        public string FavouriteProfileId { get; set; }
        public bool IsStillFavourite { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    
        public virtual UserProfileDetail UserProfileDetail { get; set; }
    }
}
