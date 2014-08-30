﻿using System.Xml.Linq;
using Shared;
using Shared.Interfaces;

namespace XmlPrinter
{
    public class XmlPrinter : IPrinter
    {
        public string PathToFile { get; set; }

        public void Print(Person person, Analysis analysis, Template template)
        {
            var xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("person",
                    new XElement("FirstName", person.FirstName),
                    new XElement("LastName", person.LastName),
                    new XElement("DateOfBirth", person.DateOfBirth),
                    new XElement("Address", person.Address),
                    new XElement("Age", person.Age),
                    new XElement("PolicyNumber", person.FirstName),
                    new XElement("analysis",
                        new XElement("Title", analysis.TemplateTitle),
                        new XElement("Date", analysis.Date))));

            for (int i = 0; i < analysis.Data.Count; ++i)
            {
                xdoc.Element("person").Element("analysis").Add(new XElement(template.Data[i], analysis.Data[i]));
            }

            xdoc.Save(PathToFile + ".xml");
        }
    }
}