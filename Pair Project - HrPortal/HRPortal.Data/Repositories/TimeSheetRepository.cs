using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Models;

namespace HRPortal.Data.Repositories
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly string _filepath;
        private readonly List<TimeSheet> _data;

        public TimeSheetRepository(string xmlFilePath)
        {
            _filepath = xmlFilePath;
            _data = ReadXml();
        }

        private List<TimeSheet> ReadXml()
        {
            return (from a in XDocument.Load(_filepath).Root?.Elements("timesheet")
                select new TimeSheet()
                {
                    TimeSheetId = (int)a.Element("timesheetid"),
                    EmployeeId = (int)a.Element("employeeid"),
                    Date = (DateTime)a.Element("date"),
                    HoursWorked = (decimal)a.Element("hoursworked")
                }).ToList();
        }

        private void WriteXml(List<TimeSheet> timesheets)
        {
            if (File.Exists(_filepath))
                File.Delete(_filepath);

            using (XmlWriter writer = XmlWriter.Create(_filepath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("timesheets");

                foreach (TimeSheet t in timesheets)
                {
                    writer.WriteStartElement("timesheet");
                    writer.WriteElementString("timesheetid", t.TimeSheetId.ToString());
                    writer.WriteElementString("employeeid", t.EmployeeId.ToString());
                    writer.WriteElementString("date", t.Date.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("hoursworked", t.HoursWorked.ToString(CultureInfo.InvariantCulture));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public TimeSheet Add(TimeSheet timesheetToAdd)
        {
            timesheetToAdd.TimeSheetId = _data.Count == 0 ? 1 : _data.Max(t => t.TimeSheetId) + 1;
            _data.Add(timesheetToAdd);
            WriteXml(_data);
            return timesheetToAdd;
        }

        public TimeSheet Edit(int timesheetId, TimeSheet timeSheetToEdit)
        {
            timeSheetToEdit.TimeSheetId = timesheetId;
            _data.Remove(_data.Single(ts => ts.TimeSheetId == timesheetId));
            _data.Add(timeSheetToEdit);
            WriteXml(_data);
            return timeSheetToEdit;
        }

        public int Remove(int timeSheetId)
        {
            _data.Remove(_data.Single(ts => ts.TimeSheetId == timeSheetId));
            WriteXml(_data);
            return timeSheetId;
        }


        public TimeSheet Load(int timesheetId)
        {
            return _data.Single(t => t.TimeSheetId == timesheetId);
        }

        public List<TimeSheet> LoadAll()
        {
            return _data;
        }

    }
}