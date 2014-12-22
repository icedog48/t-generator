using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4Processor;
using TGenerator.Model;
using TGenerator.TextTransformation;

namespace TGenerator.App
{
    public class App
    {
        private const string TEMPLATES_PATH = "Templates";

        private string _NamespaceFileName;

        private string _GeneratedOutputPath;

        private TemplateProcessor _Processor;

        public App(string filename, string generatedOutputPath)
        {
            this._NamespaceFileName = filename;
            this._GeneratedOutputPath = generatedOutputPath;

            this._Processor = TemplateProcessorFactory.DefaultFactory().CreateTemplateProcessor();
        }

        public void Execute() 
        {
            var entities = JsonConvert.DeserializeObject<Entity[]>(File.ReadAllText(this._NamespaceFileName));

            var templates = Directory.EnumerateFiles(TEMPLATES_PATH,"*.tt", SearchOption.AllDirectories);

            foreach (var template in templates)
                foreach (var entity in entities)
                    ProcessTemplate(template, entity);
        }

        private void ProcessTemplate(string template, Entity entity)
        {
            var processedString = _Processor.ProcessTemplate(template, new Dictionary<string, object> 
            {  
                { TGeneratorTextTransformation.ENTITY_PARAMETER, entity }
            });

            var outputFilename = GetGeneratedFilename(entity, template);

            if (!Directory.Exists(Path.GetDirectoryName(outputFilename))) Directory.CreateDirectory(Path.GetDirectoryName(outputFilename));

            File.WriteAllText(outputFilename, processedString);
        }

        private string GetGeneratedFilename(Entity entity, string templateFilename)
        {
            var templateName = Path.GetFileNameWithoutExtension(templateFilename);
            var templateDirectory = Path.GetDirectoryName(templateFilename);

            var outputPath = this._GeneratedOutputPath + templateDirectory.Replace(TEMPLATES_PATH, string.Empty);
            var outputFilename = templateName.Replace(TGeneratorTextTransformation.ENTITY_FILENAME, entity.Name) + _Processor.FileExtension;

            return Path.Combine(outputPath, outputFilename);
        }
    }
}
