using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace ErwMvcExtensions.ActionResults
{
    public class XmlResult : ActionResult
    {
        private object serializableObject;
        private XmlSerializer serializer;
        private string xmlContent;

        public XmlResult(object serializableObject)
            : base()
        {
            this.serializableObject = serializableObject;
            this.serializer = new XmlSerializer(this.serializableObject.GetType());
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.xmlContent == null)
            {
                this.SerializeObject();
            }

            context.HttpContext.Response.ContentType = "application/xml";
            context.HttpContext.Response.ContentEncoding = Encoding.UTF8;
            context.HttpContext.Response.Write(this.xmlContent);
        }

        private void SerializeObject()
        {
            using (var writer = new StringWriter())
            {
                this.serializer.Serialize(writer, this.serializableObject);
                this.xmlContent = writer.ToString();
            }
        }
    }
}
