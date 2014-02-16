using GCMP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GCMP.Models.Helper;

namespace GCMP.Models
{
    public class CategoryModel
    {
        private GCMPDBEntities db = null;

        //trungdd - constructor
        public CategoryModel()
        {
            db = new GCMPDBEntities();
        }


        //trungdd - edited: starus -> lay trong const class
        public List<Category> ListCategory()
        {
            var list = db.Categories.Where(ci => ci.Status.Equals(Const.CActive)).ToList();
            return list;
        }


        public Boolean AddCategory(string name, string status)
        {
            try
            {
                Category ct = new Category()
                {
                    CategoryName = name,
                    Status = status
                };
                db.Categories.Add(ct);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Category> FindCategory(int id)
        {
            var list = (from c in db.Categories where id.Equals(c.Id) select c).ToList();
            return list;
        }

        public Boolean CheckExistedCategory(string category)
        {
            if (db.Categories.Any(u => u.CategoryName == category))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Update(int id, string name, string status)
        {
            Category ct = new Category()
            {
                Id = id,
                CategoryName = name,
                Status = status
            };
            db.Entry(ct).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}