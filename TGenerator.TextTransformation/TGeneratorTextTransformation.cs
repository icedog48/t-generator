
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4Processor;
using TGenerator.Model;

namespace TGenerator.TextTransformation
{
    public class TGeneratorTextTransformation : T4ProcessorTextTransformation
    {
        public const string ENTITY_FILENAME = "_ENTITY_";
        public const string ENTITY_PARAMETER = "entity";
        public const string ENTITIES_PARAMETER = "entities";

        public Entity Entity 
        {
            get 
            {
                return (Entity)this.ProcessorSession[ENTITY_PARAMETER];
            }
        }

        public Entity Entities
        {
            get
            {
                return (Entity)this.ProcessorSession[ENTITIES_PARAMETER];
            }
        } 
    }
}
