using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Models;

namespace HRPortal.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _xmlFilePath;
        private readonly List<Employee> _data;

        public EmployeeRepository(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
            _data = ReadXml();
        }

        private List<Employee> ReadXml()
        {
            return (from a in XDocument.Load(_xmlFilePath).Root?.Elements("employee")
                select new Employee()
                {
                    EmployeeId = (int)a.Element("employeeid"),
                    FirstName = (string)a.Element("firstname"),
                    LastName = (string)a.Element("lastname"),
                    DateHired = (DateTime)a.Element("datehired")
                }).ToList();
        }

        private void WriteXml(List<Employee> employees)
        {
            if (File.Exists(_xmlFilePath))
                File.Delete(_xmlFilePath);

            using (XmlWriter writer = XmlWriter.Create(_xmlFilePath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("employees");
                foreach (Employee e in employees)
                {
                    writer.WriteStartElement("employee");
                    writer.WriteElementString("employeeid", e.EmployeeId.ToString());
                    writer.WriteElementString("lastname", e.LastName);
                    writer.WriteElementString("firstname", e.FirstName);
                    writer.WriteElementString("datehired", e.DateHired.ToString("MM/dd/yyyy"));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public Employee Add(Employee employeeToAdd)
        {
            employeeToAdd.EmployeeId = _data.Count == 0 ? 1 : _data.Max(e => e.EmployeeId) + 1;
            _data.Add(employeeToAdd);
            WriteXml(_data);
            return employeeToAdd;
        }

        public Employee Edit(int employeeId, Employee employeeToEdit)
        {
            employeeToEdit.EmployeeId = employeeId;
            _data.Remove(_data.Single(e => e.EmployeeId == employeeId));
            _data.Add(employeeToEdit);
            WriteXml(_data);
            return employeeToEdit;
        }

        public int Remove(int employeeId)
        {
            _data.Remove(_data.Single(e => e.EmployeeId == employeeId));
            WriteXml(_data);
            return employeeId;
        }

        public Employee Load(int employeeId)
        {
            return _data.Single(e => e.EmployeeId == employeeId);
        }

        public List<Employee> LoadAll()
        {
            return _data;
        }
    }
}