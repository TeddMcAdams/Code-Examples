using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Models;

namespace HRPortal.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly List<Application> _data; 
        private readonly string _xmlFilePath;

        public ApplicationRepository(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
            _data = ReadXml();
        }

        private List<Application> ReadXml()
        {
            return (from a in XDocument.Load(_xmlFilePath).Root?.Elements("application")
                    select new Application()
                    {
                        ApplicationId = (int)a.Element("applicationId"),
                        FirstName = (string)a.Element("firstname"),
                        LastName = (string)a.Element("lastname"),
                        Email = (string)a.Element("email"),
                        Phone = (string)a.Element("phone"),
                        LinkedIn = (string)a.Element("linkedin"),
                        WhyInterested = (string)a.Element("whyus")
                    }).ToList();
        }

        private void WriteXml(List<Application> applications)
        {
            if (File.Exists(_xmlFilePath))
                File.Delete(_xmlFilePath);

            using (XmlWriter writer = XmlWriter.Create(_xmlFilePath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("applications");

                foreach (Application a in applications)
                {
                    writer.WriteStartElement("application");
                    writer.WriteElementString("applicationId", a.ApplicationId.ToString());
                    writer.WriteElementString("firstname", a.FirstName);
                    writer.WriteElementString("lastname", a.LastName);
                    writer.WriteElementString("email", a.Email);
                    writer.WriteElementString("phone", a.Phone);
                    writer.WriteElementString("linkedin", a.LinkedIn);
                    writer.WriteElementString("whyus", a.WhyInterested);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public Application Add(Application applicationToAdd)
        {
            applicationToAdd.ApplicationId = _data.Count == 0 ? 1 : _data.Max(a => a.ApplicationId) + 1;
            _data.Add(applicationToAdd);
            WriteXml(_data);
            return applicationToAdd;
        }

        public Application Edit(int applicationId, Application applicationToEdit)
        {
            applicationToEdit.ApplicationId = applicationId;
            _data.Remove(_data.Single(a => a.ApplicationId == applicationId));
            _data.Add(applicationToEdit);
            WriteXml(_data);
            return applicationToEdit;
        }

        public int Remove(int applicationId)
        {
            _data.Remove(_data.Single(a => a.ApplicationId == applicationId));
            WriteXml(_data);
            return applicationId;
        }

        public Application Load(int applicationId)
        {
            return _data.Single(a => a.ApplicationId == applicationId);
        }

        public List<Application> LoadAll()
        {
            return _data;
        }
    }
}