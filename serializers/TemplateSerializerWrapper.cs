namespace PgnAnalyzer.Serializer; //<-- The custom serializer must belong to the Serializer namespace.

/*
    Template for creating a custom serializer.
    The serializer must have the name [Fileformat]SerializerWrapper, with file type capitalized.
        ex. txt -> TxtSerializerWrapper
            xml -> XmlSerializerWrapper
*/

public class TemplateSerializerWrapper : ISerializerWrapper //<-- Serializer must implement the ISerializerWrapper interface.
{
    public void Serialize(string filename, object obj)
    {
        //Your complete serializer logic, from reflection to creating the file.
        //See the other serializers for more examples.
    }
}