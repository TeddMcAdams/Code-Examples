using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HRPortal.Contracts.Repository;
using HRPortal.Models;

namespace HRPortal.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _xmlFilePath;
        private readonly List<Category> _data;

        public CategoryRepository(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
            _data = ReadXml();
        }

        private List<Category> ReadXml()
        {
            return (from c in XDocument.Load(_xmlFilePath).Root?.Elements("category")
                    select new Category
                    {
                        CategoryId = (int)c.Element("categoryId"),
                        Title = (string)c.Element("title"),
                    }).ToList();
        }

        private void WriteXml(List<Category> categories)
        {
            if (File.Exists(_xmlFilePath))
                File.Delete(_xmlFilePath);

            using (XmlWriter writer = XmlWriter.Create(_xmlFilePath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("categories");

                foreach (Category c in categories)
                {
                    writer.WriteStartElement("category");
                    writer.WriteElementString("categoryId", c.CategoryId.ToString());
                    writer.WriteElementString("title", c.Title);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public List<Category> LoadAll()
        {
            return _data;
        } 

        public Category Load(int categoryId)
        {
            return _data.Single(c => c.CategoryId == categoryId);
        }

        public Category Add(Category categoryToAdd)
        {
            categoryToAdd.CategoryId = _data.Count == 0 ? 1 : _data.Max(c => c.CategoryId) + 1;
            _data.Add(categoryToAdd);
            WriteXml(_data);
            return categoryToAdd;
        }

        public Category Edit(int categoryId, Category categoryToEdit)
        {
            categoryToEdit.CategoryId = categoryId;
            if (categoryId == 0)
                return categoryToEdit;
            _data.Remove(_data.Single(c => c.CategoryId == categoryId));
            _data.Add(categoryToEdit);
            WriteXml(_data);
            return categoryToEdit;
        }

        public int Remove(int categoryId)
        {
            if (categoryId == 0)
                return categoryId;
            
            _data.Remove(_data.Single(c => c.CategoryId == categoryId));
            WriteXml(_data);
            return categoryId;
        }
    }
}