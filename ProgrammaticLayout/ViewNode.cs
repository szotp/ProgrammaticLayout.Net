using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using CoreGraphics;
using UIKit;

namespace ProgrammaticLayout
{
    /// <summary>
    /// Utility class to print the view hierarchy in XML format.
    /// </summary>
    public class ViewNode
    {
        public string Name;
        public CGRect Frame;
        public ViewNode[] Subviews;

        public ViewNode(UIView view)
        {
            Name = view.GetType().Name;
            Frame = view.Frame;
            Subviews = view.Subviews.Select(x => new ViewNode(x)).ToArray();
        }

        void WriteTo(XmlWriter writer)
        {
            writer.WriteStartElement(Name);
            writer.WriteAttributeString("Frame", $"{Frame.Left} {Frame.Top} {Frame.Width} {Frame.Height}");

            foreach (var item in Subviews)
            {
                item.WriteTo(writer);
            }

            writer.WriteEndElement();
        }

        public static void Dump(UIView view)
        {
            view.LayoutSubviews();
            var node = new ViewNode(view);
            node.Dump();
        }

        void Dump()
        {
            using (var stringWriter = new StringWriter())
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true
                };
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    WriteTo(xmlWriter);
                }


                var xml = stringWriter.ToString();
                Console.WriteLine(xml);
            }
        }
    }

}

