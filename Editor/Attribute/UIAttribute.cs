using System;

namespace UIFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UIAttribute:Attribute
    {
        public string Uxml { set; get; }
        
        public  string Uss { get; set; }

        public UIAttribute(string uxml, string uss)
        {
            this.Uxml = uxml;
            this.Uss = uss;
        }

        public UIAttribute()
        {
        }
    }
}