using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGenerator.Model
{
    public class Entity
    {
        public Entity()
        {
            this.Properties = new List<Property>();
            this.PrimaryKeys = new List<PrimaryKey>();
            this.ForeignKeys = new List<ForeignKey>();
        }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public IList<Property> Properties { get; set; }

        public IList<PrimaryKey> PrimaryKeys { get; set; }

        public IList<ForeignKey> ForeignKeys { get; set; }

        public IList<Property> Columns 
        { 
            get 
            {
                return (from property in Properties
                        where !(PrimaryKeys != null && PrimaryKeys.Any(x => x.ColumnName.Equals(property.Name))) &&
                              !(ForeignKeys != null && ForeignKeys.Any(x => x.ColumnName.Equals(property.Name))) &&
                              !(property.TypeOf != null)
                        select property).ToList();
            } 
        }

        public Property GetPropertyBy(ForeignKey foreignKey) 
        {
            var result = (  from prop in this.Properties
                            where prop.Name.Equals(foreignKey.PropertyName)
                            select prop);
            
            if (result != null && result.Count() > 0) return result.First();

            return null;
        }
    }
}
