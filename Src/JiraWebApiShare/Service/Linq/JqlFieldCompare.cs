namespace JiraWebApi.Service.Linq;

[Flags]
internal enum JqlFieldCompare
{
    Comparable = 0x01,      // =, !=
    Sortable = 0x02,        // <, <=, >, >= 
    Contains = 0x04,        // ~, !~
    Include = 0x08,         // in, not in
    Check = 0x10,           // is, is not
    Was = 0x20,             // was, was not    
    WasInclude = 0x40,      // was in, was not in    
    Changed = 0x80          // changed
}
