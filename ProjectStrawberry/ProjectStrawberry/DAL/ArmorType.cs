//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectStrawberry.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArmorType
    {
        public ArmorType()
        {
            this.Armors = new HashSet<Armor>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Armor> Armors { get; set; }
    }
}