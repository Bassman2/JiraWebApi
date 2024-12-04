namespace JiraWebApi;

/// <summary>
/// Fields meta information.
/// </summary>
//[Serializable]
public sealed class Fields // : ISerializable
{
    /// <summary>
    /// Initializes a new instance of the Fields class.
    /// </summary>
    private Fields()
    { }

    ///// <summary>
    ///// Initializes a new instance of the Fields class by serialization.
    ///// </summary>
    ///// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
    ///// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) for this serialization.</param>
    //private Fields(SerializationInfo info, StreamingContext context)
    //{
    //    //int i = 1;
    //    //Dictionary<string, FieldMeta> metaFields = new Dictionary<string, FieldMeta>();
    //    //foreach (SerializationEntry entry in info)
    //    //{
    //    //    JObject obj = entry.Value as JObject;
    //    //    FieldMeta fieldMeta = null;

    //    //    Trace.WriteLine(string.Format("entry {0} : {1} ", i++, entry.Name));
            
    //    //    fieldMeta = obj.ToObject<FieldMeta>();
    //    //    metaFields.Add(entry.Name, fieldMeta);
            
    //    //}
    //    //this.MetaFields = metaFields;
    //}

    ///// <summary>
    ///// Not supported.
    ///// </summary>
    ///// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
    ///// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) for this serialization.</param>
    //public void GetObjectData(SerializationInfo info, StreamingContext context)
    //{
    //    throw new NotSupportedException();
    //}

    /// <summary>
    /// Meta fields.
    /// </summary>
    public IDictionary<string, FieldMeta>? MetaFields { get; set; }
}
