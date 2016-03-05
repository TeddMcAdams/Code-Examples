using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Models;

namespace HRPortal.Data.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly string _filePath;
        private readonly List<Policy> _data;

        public PolicyRepository(string xmlFilePath)
        {
            _filePath = xmlFilePath;
            _data = ReadXml();
        }

        private List<Policy> ReadXml()
        {
            return (from p in XDocument.Load(_filePath).Root?.Elements("policy")
                    select new Policy()
                    {
                        PolicyId = (int)p.Element("policyId"),
                        CategoryId = (int)p.Element("categoryId"),
                        Title = (string)p.Element("title"),
                        Content = (string)p.Element("content")
                    }).ToList();
        }

        private void WriteXml(List<Policy> policies)
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            using (XmlWriter writer = XmlWriter.Create(_filePath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("policies");

                foreach (Policy p in policies)
                {
                    writer.WriteStartElement("policy");
                    writer.WriteElementString("policyId", p.PolicyId.ToString());
                    writer.WriteElementString("categoryId", p.CategoryId.ToString());
                    writer.WriteElementString("title", p.Title);
                    writer.WriteElementString("content", p.Content);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public List<Policy> LoadAll()
        {
            return _data;
        }

        public Policy Load(int policyId)
        {
            return _data.Single(p => p.PolicyId == policyId);
        }

        public Policy Add(Policy policyToAdd)
        {
            policyToAdd.PolicyId = _data.Count == 0 ? 1 : _data.Max(p => p.PolicyId) + 1;
            _data.Add(policyToAdd);
            WriteXml(_data);
            return policyToAdd;
        }

        public Policy Edit(int policyId, Policy policyToEdit)
        {
            policyToEdit.PolicyId = policyId;
            _data.Remove(_data.Single(p => p.PolicyId == policyId));
            _data.Add(policyToEdit);
            WriteXml(_data);
            return policyToEdit;
        }

        public int Remove(int policyId)
        {
            _data.Remove(_data.Single(p => p.PolicyId == policyId));
            WriteXml(_data);
            return policyId;
        }
    }
}