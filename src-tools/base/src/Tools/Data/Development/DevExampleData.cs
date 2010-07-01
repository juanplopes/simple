using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Simple;
using System.Linq.Expressions;
using Sample.Project.Domain;
using Simple.Generator.Data;

namespace Sample.Project.Tools.Data.Development
{
    public class DevExampleData : DevelopmentData<Book>
    {
        protected override void DefineItems()
        {
            NewBook("asd");
            NewBook("qwe");
            NewBook("123");
        }

        private void NewBook(string key)
        {
            MakeNew(key).IdentifiedBy(x =>
            {
                x.Title = key;
            })
            .AlsoWith(x =>
            {
                x.Author = "test";
            });
        }


        protected override Expression<Func<Book, bool>> FindPredicate(Book entity)
        {
            return x => x.Title == entity.Title;
        }
    }
}
