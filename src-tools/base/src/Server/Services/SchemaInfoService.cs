using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Sample.Project.Domain;

namespace Sample.Project.Services
{
    public partial class SchemaInfoService : EntityService<SchemaInfo>, ISchemaInfoService
    {
    }
}